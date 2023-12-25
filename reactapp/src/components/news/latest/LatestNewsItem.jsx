import { Box, Typography } from '@mui/material';
import React from 'react'
import { useNavigate } from 'react-router-dom';

const LatestNewsItem = ({ news, width, height }) => {
  const navigate = useNavigate();

  return (
    <Box
      className="mx-4 my-8 flex gap-4 cursor-pointer"
      onClick={() => navigate(news.Id)}>
        <img
          className="w-1/4 object-cover min-h-12 min-w-20 max-w-20 max-h-12"
          alt="newsThumbnail"
          src={news.NewsThumbnail}
          style={{ width: (width * 4), height: (height * 4) }} />

      <Box className="w-3/4 flex flex-col gap-2">
        <Typography
          variant="subtitle1"
          sx={{ overflow: "hidden", textOverflow: "ellipsis", whiteSpace: "nowrap" }}>
          <b>{news.NewsTitle}</b>
        </Typography>

        <Typography
          variant="body2"
          className="text-justify">
          {news.NewsClarify}
        </Typography>
      </Box>
    </Box>
  )
}

export default LatestNewsItem;