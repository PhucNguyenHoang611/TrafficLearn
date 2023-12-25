import { Box, Typography } from '@mui/material';
import React from 'react'
import { useNavigate } from 'react-router-dom';
import { ClockIcon } from "@heroicons/react/24/outline";

const NewsItem = ({ news, width, height }) => {
  const navigate = useNavigate();
  
  return (
    <Box
      className="mx-4 my-8 flex gap-8 cursor-pointer"
      onClick={() => navigate(news.Id)}>
        <img
          className="w-1/5 object-cover min-h-22 min-w-40 max-w-40 max-h-22"
          alt="newsThumbnail"
          src={news.NewsThumbnail}
          style={{ width: (width * 4), height: (height * 4) }} />

      <Box className="w-4/5 flex flex-col gap-2">
        <Typography
          variant="subtitle1"
          sx={{ overflow: "hidden", textOverflow: "ellipsis", whiteSpace: "nowrap" }}>
          <b>{news.NewsTitle}</b>
        </Typography>

        <Box className="flex items-center">
          <ClockIcon className="h-6 w-6 text-gray-500 mr-1" />
          <Typography
            variant="body2" className="text-gray-500">
            {new Date(news.NewsDate).toLocaleDateString("vi-VN", {
              day: "numeric",
              month: "long",
              year: "numeric",
            })}
          </Typography>
        </Box>


        <Typography
          variant="body2"
          className="text-justify">
          {news.NewsClarify}
        </Typography>
      </Box>
    </Box>
  )
}

export default NewsItem;