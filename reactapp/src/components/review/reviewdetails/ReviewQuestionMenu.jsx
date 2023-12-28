import React from 'react'
import { Box, Button, Grid } from '@mui/material';
import { useNavigate } from 'react-router-dom';

const ReviewQuestionMenu = ({
  questions,
  setScrollQuestion }) => {

  const navigate = useNavigate();

  const handleChangeQuestion = (questionNumber) => {
    setScrollQuestion(questionNumber);
  };

  return (
    <nav className="z-10 shadow-md w-full sm:w-56 flex-shrink-0">
      <div className="flex flex-col h-full">
        <div className="flex flex-col flex-grow m-4 overflow-y-auto">
          <Grid container spacing={2}>
            <Grid container item spacing={1}>
              {questions.map((item, index) => (
                <Grid
                  key={index}
                  onClick={() => handleChangeQuestion(index + 1)}
                  item xs={3}>
                  <Box className={`
                    text-center text-gray-700 cursor-pointer border-2 border-gray-400 rounded-lg py-2`}>
                    {index + 1}
                  </Box>
                </Grid> 
              ))}
            </Grid>
          </Grid>
        </div>

        <div className="flex flex-col flex-grow p-2">
          <Button
            onClick={() => navigate(-1)}
            sx={{
              backgroundColor: "#1786b4",
              color: "white",
              fontWeight: "bold",
              fontSize: "1.2rem"
            }} >
              Trở lại
          </Button>
        </div>
      </div>
    </nav>
  )
}

export default ReviewQuestionMenu;