/* eslint-disable react-hooks/exhaustive-deps */
import { Box, Button, Typography } from '@mui/material';
import React, { useState, useEffect } from 'react'
import { useSelector } from 'react-redux';
import { useLocation, useNavigate, useParams } from 'react-router-dom';
import { createExaminationResults, validateAnswer } from '../../../apis/api_function';
import { licenseInfo } from '../../../constants/constants';

const ExamResult = () => {
  const { examId } = useParams();
  const location  = useLocation();
  const [result, setResult] = useState(null);
  const [flag, setFlag] = useState(false);
  const [questionsData, setQuestionsData] = useState([]);
  const auth = useSelector((state) => state.auth);
  const navigate = useNavigate();

  const getNumberOfTrueAnswers = (question) => {
    const license = licenseInfo.find((license) => license.licenseName === question.License.LicenseName);
    return license.numberOfTrueAnswers;
  }

  const calculateResult = async (questions) => {
    try {
      const validatePromises = questions
        .filter((question) => question.selectedAnswerId != "")
        .map(async (question) => {
          const response = await validateAnswer(question.Question.Id, question.selectedAnswerId, auth.token);
          const isCorrect = response.data.result ? 1 : 0;
          
          if (question.Question.Important && isCorrect == 0) return -1;
  
          return isCorrect;
        });

      const results = await Promise.all(validatePromises);
      const score = results.filter((res) => res == 1).length;
      const important = results.filter((res) => res == -1).length;
      const incomplete = questions.length - results.length;
      
      if (results.includes(-1) || score < getNumberOfTrueAnswers(questions[0]))
        setResult({ score, incomplete, important, passed: false });
      else
        setResult({ score, incomplete, important, passed: true });
    } catch (error) {
      console.log(error);
    }
  };

  const storeResult = async () => {
    setFlag(true);
    try {
      await createExaminationResults(
        auth.id,
        examId,
        new Date(),
        result.score,
        result.passed,
        auth.token);
    } catch (error) {
      console.log(error);
    }
  }

  useEffect(() => {
    if (location.state.questionsData) {
      setQuestionsData(location.state.questionsData);
      calculateResult(location.state.questionsData);
    }
  }, [location]);

  useEffect(() => {
    if (result && !flag)
      storeResult();
  }, [result]);

  return (
    <Box width="100%" height="100%" display="flex" justifyContent="center" alignItems="center">
      {result && (
        <Box className="flex flex-col justify-center items-center p-24">
          <Typography variant="h4" sx={{ fontWeight: 600, mb: 8 }}>
            Kết quả bài thi
          </Typography>
          <Typography variant="h6">
            Số câu đúng: {result.score} / {questionsData.length}
          </Typography>
          <Typography variant="h6">
            Số câu chưa làm: {result.incomplete}
          </Typography>
          <Typography variant="h6">
            Số câu điểm liệt sai: {result.important}
          </Typography>
          <Typography variant="h6">
            Kết quả: {result.passed ? "Đạt" : "Chưa đạt"}
          </Typography>

          <Button sx={{ mt: 4, fontSize: 20, bgcolor: "#1786b4", color: "white", fontWeight: 600 }} onClick={() => navigate("/")}>
            Trở về trang chủ
          </Button>
        </Box>
      )}
    </Box>
  )
}

export default ExamResult;