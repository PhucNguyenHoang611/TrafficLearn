/* eslint-disable react-hooks/exhaustive-deps */
import React, { useEffect, useState } from 'react'
import { useLocation, useNavigate, useParams } from 'react-router-dom';
import { getAllExaminationQuestions } from '@/apis/api_function';
import QuestionMenu from "@/components/review/examination/exampage/QuestionMenu";
import QuestionList from "@/components/review/examination/exampage/QuestionList";
import { useDispatch, useSelector } from 'react-redux';
import { storeExamData, storeQuestionsData } from '../../../redux/reducers/exam_reducers';
import { licenseInfo } from '../../../constants/constants';

const ExamPage = () => {
  const { examId } = useParams();
  const location = useLocation();
  const navigate = useNavigate();
  const dispatch = useDispatch();

  const [questions, setQuestions] = useState([]);
  const [examData, setExamData] = useState(null);
  const [scrollQuestion, setScrollQuestion] = useState(0);
  const exam = useSelector((state) => state.exam);

  const getLicenseTime = () => {
    const license = licenseInfo.find((license) => license.licenseName === location.state.license);
    return license.time;
  };

  const initialTimeRemaining = localStorage.getItem("timeRemaining")
    ? parseInt(localStorage.getItem("timeRemaining"))
    : getLicenseTime();
  const [timeRemaining, setTimeRemaining] = useState(initialTimeRemaining);

  const minutes = Math.floor(timeRemaining / 60);
  const seconds = timeRemaining % 60;
  
  const getQuestions = () => {
    try {
      localStorage.setItem("examId", examId.toString());
      getAllExaminationQuestions(examId)
        .then((res) => {
          setExamData(res.data);
          dispatch(storeExamData({
            examId,
            examData: res.data
          }));

          const questionArray = res.data.data.map((question, index) => {
            return { questionNumber: index + 1, ...question, selectedAnswerId: "" };
          });
          setQuestions(questionArray);
          dispatch(storeQuestionsData(questionArray));
        });
    } catch (error) {
      console.log(error);
    }
  };

  useEffect(() => {
    if (exam.examData && exam.questionsData.length > 0 && exam.examId === examId) {
      setExamData(exam.examData);
      setQuestions(exam.questionsData);
    } else {
      getQuestions();
    }
  }, [examId]);

  useEffect(() => {
    // Exit early if we reach 0
    if (timeRemaining <= 0) {
      localStorage.removeItem("timeRemaining");
      localStorage.removeItem("examId");

      dispatch(storeExamData({
        examId: "",
        examData: null
      }));
      dispatch(storeQuestionsData([]));

      navigate(`/result/${examId}`, {
        state: {
          questionsData: questions
        }
      });
      return;
    }

    // Set up the timer
    const timerId = setInterval(() => {
      setTimeRemaining((prevTime) => {
        const newTime = prevTime - 1;
        localStorage.setItem("timeRemaining", JSON.stringify(newTime));
        return newTime;
      });
    }, 1000);

    // Clean up the interval on component unmount
    return () => clearInterval(timerId);
  }, [timeRemaining]);

  return (  
    <div>
      <section className="flex flex-col sm:flex-row h-screen">
        <QuestionMenu
          // examId={examId}
          minutes={minutes}
          seconds={seconds}
          questions={questions}
          setScrollQuestion={setScrollQuestion}
          setTimeRemaining={setTimeRemaining} />

        <div className="flex-grow overflow-y-auto">
          <QuestionList
            questions={questions}
            setQuestions={setQuestions}
            scrollQuestion={scrollQuestion} />
        </div>
      </section>
    </div>
  )
}

export default ExamPage;