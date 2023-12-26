import * as React from "react";
import { styled } from "@mui/material/styles";
import Card from "@mui/material/Card";
import CardHeader from "@mui/material/CardHeader";
import CardContent from "@mui/material/CardContent";
import CardActions from "@mui/material/CardActions";
import Collapse from "@mui/material/Collapse";
import IconButton from "@mui/material/IconButton";
import Typography from "@mui/material/Typography";
import ExpandMoreIcon from "@mui/icons-material/ExpandMore";
import { Box } from "@mui/material";

const ExpandMore = styled((props) => {

  const { expand, ...other } = props;
  return <IconButton {...other} />;

})(({ theme, expand }) => ({

  transform: !expand ? "rotate(0deg)" : "rotate(180deg)",
  marginLeft: "auto",
  transition: theme.transitions.create("transform", {
    duration: theme.transitions.duration.shortest,
  })

}));

const TitleCard = ({ title, titleTypeName }) => {
  const [expanded, setExpanded] = React.useState(false);

  const handleExpandClick = () => {
    setExpanded(!expanded);
  };

  return (
    <Card sx={{ maxWidth: "95%", mb: 2 }}>
      <Box className="flex">
        <img src={title.TitleImage} alt="titleImg" className="w-20 h-20 object-cover m-4" />
        <CardHeader
            title={title.TitleName}
            subheader={titleTypeName} />
      </Box>

      <CardActions disableSpacing>
        <ExpandMore
          expand={expanded}
          onClick={handleExpandClick}
          aria-expanded={expanded}
          aria-label="Show more"
        >
          <ExpandMoreIcon />
        </ExpandMore>
      </CardActions>

      <Collapse in={expanded} timeout="auto" unmountOnExit>
        <CardContent>
          <Typography variant="h6">
            <b>Nội dung:</b>
          </Typography>
          <Typography paragraph>
            {title.TitleExplanation}
          </Typography>
        </CardContent>
      </Collapse>
    </Card>
  );
}

export default TitleCard;