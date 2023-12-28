/* eslint-disable react-hooks/exhaustive-deps */
import React, { useEffect, useState } from 'react'
import { useLocation, useNavigate } from 'react-router-dom';
import ReviewQuestionMenu from '../../components/review/reviewdetails/ReviewQuestionMenu';
import { validateAnswer } from '../../apis/api_function';
import { useSelector } from 'react-redux';
import ReviewQuestionList from '../../components/review/reviewdetails/ReviewQuestionList';

const ReviewDetails = () => {
  const location = useLocation();
  const navigate = useNavigate();
  const [questions, setQuestions] = useState([]);
  const [flag, setFlag] = useState(false);
  const [scrollQuestion, setScrollQuestion] = useState(0);
  const auth = useSelector((state) => state.auth);

  const getAnswer = async (questionId, answers) => {
    try {
      const promises = answers.map(async (answer) => {
        const check = await validateAnswer(questionId, answer.AnswerId, auth.token);
        return check.data.result ? answer.AnswerId : null;
      });

      return Promise.all(promises);
    } catch (error) {
      console.log(error);
    }
  };

  const getTrueAnswers = async (questionsList) => {
    const result = await Promise.all(questionsList.map(async (question) => {
      const trueAns = await getAnswer(question.Question.Id, question.Answers);
      const final = trueAns.filter((ans) => ans);
        
      return { ...question, trueAnswer: final[0] };
    }));

    setQuestions(result);
    setFlag(true);
  };

  useEffect(() => {
    if (!flag && location.state && location.state.questionsList && location.state.check) {
      getTrueAnswers(location.state.questionsList);
    }
    else
      navigate("/review");
  }, [location]);
  
  return (
    <div>
      <section className="flex flex-col sm:flex-row h-screen">
        {questions.length > 0 && (
          <>
            <ReviewQuestionMenu
              questions={questions}
              setScrollQuestion={setScrollQuestion} />

            <div className="flex-grow overflow-y-auto">
              <ReviewQuestionList
                questions={questions}
                scrollQuestion={scrollQuestion} />
            </div>
          </>
        )}
      </section>
    </div>
  )
}

export default ReviewDetails;