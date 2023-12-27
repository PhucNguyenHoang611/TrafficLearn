/* eslint-disable react-hooks/exhaustive-deps */
import { Box, Typography } from '@mui/material';
import React, { useState, useEffect } from 'react'
import { useSelector } from 'react-redux';
import { useLocation, useParams } from 'react-router-dom';
import { validateAnswer } from '../../../apis/api_function';
import { licenseInfo } from '../../../constants/constants';

const ExamResult = () => {
  const { examId } = useParams();
  const location  = useLocation();
  const [result, setResult] = useState(null);
  const [questionsData, setQuestionsData] = useState([]);
  const auth = useSelector((state) => state.auth);

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
      
      if (results.includes(-1) || score < getNumberOfTrueAnswers(questions[0]))
        setResult({ score, failed: true });
      else
        setResult({ score, failed: false });
    } catch (error) {
      console.log(error);
    }
  };

  const storeResult = () => {
    try {

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

  return (
    <Box width="100%" height="100%" display="flex" justifyContent="center" alignItems="center">
      {result && (
        <Box className="flex flex-col justify-center items-center">
          <Typography variant="h4">
            Kết quả bài làm
          </Typography>
          <Typography variant="h6">
            {result.score} / {questionsData.length}
          </Typography>
          <Typography variant="h6">
            {result.failed ? "Failed" : "Passed"}
          </Typography>
        </Box>
      )}
    </Box>
  )
}

export default ExamResult;