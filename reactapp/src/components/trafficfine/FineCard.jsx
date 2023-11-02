import * as React from "react";
import { styled } from "@mui/material/styles";
import Card from "@mui/material/Card";
import CardHeader from "@mui/material/CardHeader";
import CardMedia from "@mui/material/CardMedia";
import CardContent from "@mui/material/CardContent";
import CardActions from "@mui/material/CardActions";
import Collapse from "@mui/material/Collapse";
import Avatar from "@mui/material/Avatar";
import IconButton from "@mui/material/IconButton";
import Typography from "@mui/material/Typography";
import { red } from "@mui/material/colors";
import FavoriteIcon from "@mui/icons-material/Favorite";
import ShareIcon from "@mui/icons-material/Share";
import ExpandMoreIcon from "@mui/icons-material/ExpandMore";
import MoreVertIcon from "@mui/icons-material/MoreVert";

const ExpandMore = styled((props) => {
  const { expand, ...other } = props;
  return <IconButton {...other} />;
})(({ theme, expand }) => ({
  transform: !expand ? "rotate(0deg)" : "rotate(180deg)",
  marginLeft: "auto",
  transition: theme.transitions.create("transform", {
    duration: theme.transitions.duration.shortest,
  }),
}));

export default function RecipeReviewCard() {
  const [expanded, setExpanded] = React.useState(false);

  const handleExpandClick = () => {
    setExpanded(!expanded);
  };

  return (
    <Card sx={{ maxWidth: "90%" }}>
      <CardHeader
        action={
          <IconButton aria-label="settings">
            <MoreVertIcon />
          </IconButton>
        }
        title="Không có báo hiệu xin vượt trước khi vượt"
        subheader="Chuyển hướng, nhường đường"
      />
      <CardContent>
        <Typography variant="body2" color="text.secondary">
          Phạt tiền từ 100.000 đồng đến 200.000 đồng đối với người điều khiển
        </Typography>
      </CardContent>
      <CardActions disableSpacing>
        <IconButton aria-label="add to favorites">
          <FavoriteIcon />
        </IconButton>
        <IconButton aria-label="share">
          <ShareIcon />
        </IconButton>
        <ExpandMore
          expand={expanded}
          onClick={handleExpandClick}
          aria-expanded={expanded}
          aria-label="show more"
        >
          <ExpandMoreIcon />
        </ExpandMore>
      </CardActions>
      <Collapse in={expanded} timeout="auto" unmountOnExit>
        <CardContent>
          <Typography paragraph>Hành vi:</Typography>
          <Typography paragraph>
            Lorem ipsum dolor sit amet consectetur, adipisicing elit.
            Exercitationem possimus voluptatum id, amet aut, voluptate animi
            deleniti expedita beatae magni dolore! Beatae dolorem perspiciatis
            accusamus.
          </Typography>
          <Typography paragraph>
            <h3>Không có báo hiệu xin vượt trước khi vượt</h3>
            <p>
              Phạt tiền từ 100.000 đồng đến 200.000 đồng đối với người điều
              khiển
            </p>
          </Typography>
          <Typography paragraph>Căn cứ pháp lý:</Typography>
          <a href="chiu">Điểm e khoản 2 điều 6</a>
          <p> </p>
          <Typography paragraph>Hình thức phạt bổ sung:</Typography>
        </CardContent>
      </Collapse>
    </Card>
  );
}
