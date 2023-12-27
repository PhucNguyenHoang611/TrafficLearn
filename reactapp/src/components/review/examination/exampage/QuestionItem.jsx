import * as React from "react";
import Card from "@mui/material/Card";
import CardHeader from "@mui/material/CardHeader";
import CardContent from "@mui/material/CardContent";
import { Box, FormControl, FormControlLabel, FormLabel, RadioGroup, Radio } from "@mui/material";

import { storeQuestionsData } from '../../../../redux/reducers/exam_reducers';
import { useDispatch } from "react-redux";

const QuestionItem = ({ question, questions, setQuestions }) => {
  const dispatch = useDispatch();

  const handleChangeAnswer = (event) => {
    const updateQuestions = questions.map((item) =>
      item.Question.Id == question.Question.Id
      ? { ...item, selectedAnswerId: event.target.value }
      : item
    );

    setQuestions(updateQuestions);
    dispatch(storeQuestionsData(updateQuestions));
  };

  return (
    <Card sx={{ maxWidth: "95%", mb: 2 }}>
      {question.Question.Important && (
        <p className="mt-2 ml-2 text-red-500">(Câu hỏi điểm liệt)</p>
      )}

      <CardHeader
          title={`Câu ${question.questionNumber}:`}
          subheader={question.Question.QuestionContent} />
      
      <CardContent>
        {(question.Question.QuestionMedia != "") && (
          <Box className="flex justify-center items-center">
            <img
              alt="questionMedia"
              src={question.Question.QuestionMedia}
              className="w-96 object-cover" />
          </Box>
        )}

        <FormControl sx={{ mt: 2 }}>
          <FormLabel id="controlled-radio-buttons-group">Đáp án</FormLabel>
          <RadioGroup
            aria-labelledby="controlled-radio-buttons-group"
            name="controlled-radio-buttons-group"
            value={question.selectedAnswerId}
            onChange={handleChangeAnswer}>
            {question.Answers.map((item, index) => (
              <FormControlLabel key={index} value={item.AnswerId} control={<Radio />} label={item.Content} />
            ))}
          </RadioGroup>
        </FormControl>
      </CardContent>
    </Card>
  );
}

export default QuestionItem;