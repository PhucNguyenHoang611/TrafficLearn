import * as React from "react";
import { useNavigate } from "react-router-dom";
import Card from "@mui/material/Card";
import CardHeader from "@mui/material/CardHeader";
import CardContent from "@mui/material/CardContent";
import Typography from "@mui/material/Typography";

const TitleCard = ({
  titleName,
  total,
  numberOfImportantQuestions,
  questionsList }) => {

  const navigate = useNavigate();

  return (
    <Card sx={{ maxWidth: "95%", mb: 2, cursor: "pointer" }}>
      <CardHeader
          title={titleName} />
      
      <CardContent>
        <Typography
          variant="body2"
          color="text.secondary">
            {titleName == "Các câu điểm liệt trong bộ đề"
              ? `Gồm ${total} câu hỏi.`
              : `Gồm ${total} câu hỏi. Có ${numberOfImportantQuestions} câu điểm liệt`}
        </Typography>
      </CardContent>
    </Card>
  );
}

export default TitleCard;