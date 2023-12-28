import * as React from "react";
import Card from "@mui/material/Card";
import CardHeader from "@mui/material/CardHeader";
import CardContent from "@mui/material/CardContent";
import { CheckIcon } from "@heroicons/react/24/outline";
import { Box, FormControl, FormControlLabel, FormLabel, RadioGroup, Radio, Typography } from "@mui/material";

const ReviewQuestionItem = ({ index, question }) => {

  return (
    <Card sx={{ maxWidth: "95%", mb: 2 }}>
      {question.Question.Important && (
        <p className="mt-2 ml-2 text-red-500">(Câu hỏi điểm liệt)</p>
      )}

      <CardHeader
          title={`Câu ${index}:`}
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
            value={question.trueAnswer}>
            {question.Answers.map((item, index) => (
              <Box className="flex items-center" key={index}>
                <FormControlLabel key={index} value={item.AnswerId} control={<Radio />} label={item.Content} />
                {(question.trueAnswer === item.AnswerId) && <CheckIcon className="h-6 w-6 text-green-700" />}
              </Box>
            ))}
          </RadioGroup>
        </FormControl>

        <Box className="mt-4">
          <Typography variant="h6" sx={{ fontWeight: 600, mb: 2 }}>
            Giải thích
          </Typography>
          <Typography>
            {question.Question.Explanation}
          </Typography>
        </Box>
      </CardContent>
    </Card>
  );
}

export default ReviewQuestionItem;