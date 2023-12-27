import React, { useState } from 'react'
import { Box, Button, Grid, Modal, Typography } from '@mui/material';

const style = {
  position: "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: 400,
  bgcolor: "white",
  borderRadius: 3,
  boxShadow: 24,
  p: 4,
};

const QuestionMenu = ({
  minutes,
  seconds,
  questions,
  setScrollQuestion,
  setTimeRemaining }) => {

  const [openModal, setOpenModal] = useState(false);

  const handleCloseModal = () => {
    setOpenModal(false);
  };

  const handleChangeQuestion = (questionNumber) => {
    setScrollQuestion(questionNumber);
  };

  const handleCompleteExam = () => {
    setOpenModal(false);
    setTimeRemaining(0);
  };

  return (
    <nav className="z-10 shadow-md w-full sm:w-56 flex-shrink-0">
      <div className="flex flex-col h-full">
        <div className="flex items-center justify-center h-16">
          <p className="mr-2 font-semibold">Thời gian:</p>
          <p
            className="text-2xl font-bold">
              {`${minutes}:${seconds < 10 ? '0' : ''}${seconds}`}
          </p>
        </div>
        <div className="flex flex-col flex-grow m-4 overflow-y-auto">
          <Grid container spacing={2}>
            <Grid container item spacing={1}>
              {questions.map((item, index) => (
                <Grid
                  key={index}
                  onClick={() => handleChangeQuestion(item.questionNumber)}
                  item xs={3}>
                  <Box className={`
                    ${item.selectedAnswerId != "" ? "bg-blue-200" : ""} 
                    text-center text-gray-700 cursor-pointer border-2 border-gray-400 rounded-lg py-2`}>
                    {item.questionNumber}
                  </Box>
                </Grid> 
              ))}
            </Grid>
          </Grid>
        </div>

        <div className="flex flex-col flex-grow p-2">
          <Button
            onClick={() => setOpenModal(true)}
            sx={{
              backgroundColor: "#1786b4",
              color: "white",
              fontWeight: "bold",
              fontSize: "1.2rem"
            }} >
              Nộp bài
          </Button>
          <Modal
            open={openModal}
            onClose={handleCloseModal}
            aria-labelledby="modal-modal-title"
            aria-describedby="modal-modal-description"
            >
            <Box sx={style}>
              <Typography id="modal-modal-title" variant="h6" component="h2">
                Kết thúc bài thi
              </Typography>
              <Typography id="modal-modal-description" sx={{ mt: 2 }}>
                Bạn có chắc chắn muốn nộp bài ?
              </Typography>
              <Box className="flex w-full justify-end items-center mt-4">
                <Button onClick={handleCloseModal}>Hủy</Button>
                <Button onClick={handleCompleteExam}>Xác nhận</Button>
              </Box>
            </Box>
          </Modal>
        </div>
      </div>
    </nav>
  )
}

export default QuestionMenu;