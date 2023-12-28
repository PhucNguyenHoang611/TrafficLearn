import * as React from "react";
import { useNavigate } from "react-router-dom";
import Card from "@mui/material/Card";
import CardHeader from "@mui/material/CardHeader";
import CardContent from "@mui/material/CardContent";
import CardActions from "@mui/material/CardActions";
import Typography from "@mui/material/Typography";
import { Button, Box, Modal } from "@mui/material";
import { useState } from "react";

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

const ExaminationCard = ({ auth, examination, licenseName, numberOfQuestions, timeRemaining }) => {
  const navigate = useNavigate();
  const [openModal, setOpenModal] = useState(false);

  const handleCloseModal = () => {
    setOpenModal(false);
  };

  const startExam = () => {
    if (auth.token) {
      if (localStorage.getItem("examId") && localStorage.getItem("examId") !== examination.Id)
        localStorage.removeItem("timeRemaining");

      navigate(`/exam/${examination.Id}`, {
        state: {
          license: licenseName
        }
      });
    }
    else
      navigate("/login");
  };

  return (
    <Card sx={{ maxWidth: "95%", mb: 2 }}>
      <CardHeader
        title={examination.ExaminationName}
        subheader={`Hạng ${licenseName}`} />
      
      <CardContent>
        <Typography
          variant="body2"
          color="text.secondary">
            {`Bài thi giấy phép lái xe hạng ${licenseName}`}
        </Typography>
      </CardContent>

      <CardActions disableSpacing>
        <Box className="w-full flex justify-end items-center">
          <Button
            className="bg-blue-500"
            onClick={() => setOpenModal(true)}>Thi thử</Button>
        </Box>
        <Modal
          open={openModal}
          onClose={handleCloseModal}
          aria-labelledby="modal-modal-title"
          aria-describedby="modal-modal-description"
          >
          <Box sx={style}>
            <Typography id="modal-modal-title" variant="h6" component="h2">
              Xác nhận làm bài thi thử
            </Typography>
            <Typography id="modal-modal-description" sx={{ mt: 2 }}>
              Bài thi có tổng cộng {numberOfQuestions} câu hỏi.
            </Typography>
            <Typography id="modal-modal-description">
              Thời gian dành cho bạn là {timeRemaining / 60} phút.
            </Typography>
            <Box className="flex w-full justify-end items-center mt-4">
              <Button onClick={handleCloseModal}>Hủy</Button>
              <Button onClick={startExam}>Bắt đầu</Button>
            </Box>
          </Box>
        </Modal>
      </CardActions>
    </Card>
  );
}

export default ExaminationCard;