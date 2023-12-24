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
import MoreVertIcon from "@mui/icons-material/MoreVert";

// import Avatar from "@mui/material/Avatar";
// import CardMedia from "@mui/material/CardMedia";
// import { red } from "@mui/material/colors";
// import FavoriteIcon from "@mui/icons-material/Favorite";
// import ShareIcon from "@mui/icons-material/Share";

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

const FineCard = ({ fine, fineTypeName }) => {
  const [expanded, setExpanded] = React.useState(false);

  const handleExpandClick = () => {
    setExpanded(!expanded);
  };

  return (
    <Card sx={{ maxWidth: "95%", mb: 2 }}>
      <CardHeader
        title={fine.FineName}
        subheader={fineTypeName} />

      <CardContent>
        <Typography
          variant="body2"
          color="text.secondary"
          dangerouslySetInnerHTML={{ __html: fine.FineContent.split("; ").join("<br>") }}>
        </Typography>
      </CardContent>

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
            <b>Hành vi:</b>
          </Typography>
          <Typography paragraph>
            {fine.FineBehavior}
          </Typography>

          {(fine.FineAdditional != "") && (
            <>
              <Typography variant="h6">
                <b>Hình thức phạt bổ sung:</b>
              </Typography>
              <Typography
                paragraph
                dangerouslySetInnerHTML={{ __html: fine.FineAdditional.split("; ").join("<br>") }}>
              </Typography>
            </>
          )}

          {(fine.FineNote != "") && (
            <>
              <Typography variant="h6">
                <b>Ghi chú:</b>
              </Typography>
              <Typography paragraph>
                {fine.FineNote}
              </Typography>
            </>
          )}

          {/* {(fine.FineUnifiedBehavior != "") && (
            <>
              <Typography variant="h6">
                <b>Hành vi hợp nhất:</b>
              </Typography>
              <Typography
                paragraph
                dangerouslySetInnerHTML={{ __html: fine.FineUnifiedBehavior.split("; ").join("<br>") }}>
              </Typography>
            </>
          )} */}
        </CardContent>
      </Collapse>
    </Card>
  );
}

export default FineCard;