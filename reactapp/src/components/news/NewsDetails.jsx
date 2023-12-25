/* eslint-disable react-hooks/exhaustive-deps */
import React, { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom';
import { getNews } from '../../apis/api_function';
import { Box, Typography } from '@mui/material';
import MarkdownComponent from "./MarkdownComponent";
import { ClockIcon } from "@heroicons/react/24/outline";

const NewsDetails = () => {
  const { newsId } = useParams();
  const [currentNews, setCurrentNews] = useState(null);

  const getNewsDetails = () => {
    try {
      getNews(newsId)
        .then((res) => {
          setCurrentNews(res.data);
        });
    } catch (error) {
      console.log(error);
    }
  };

  useEffect(() => {
    getNewsDetails();
  }, [newsId]);

  return (
    <>
      {currentNews && (
        <Box className="p-4">
          <img
            className="w-full"
            src={currentNews.NewsThumbnail}
            alt="newsThumbnail" />
          
          <Typography
            variant="h5"
            className="py-4">
              <b>{currentNews.NewsTitle}</b>
          </Typography>

          <Box className="flex items-center pb-4">
            <ClockIcon className="h-6 w-6 text-gray-500 mr-1" />
            <Typography
              variant="h6" className="text-gray-500">
              {new Date(currentNews.NewsDate).toLocaleDateString("vi-VN", {
                day: "numeric",
                month: "long",
                year: "numeric",
              })}
            </Typography>
          </Box>

          <Box className="markdown prose space-y-4 lg:prose-xl">
            <MarkdownComponent markdown={currentNews.NewsContent} />
          </Box>
        </Box>
      )}
    </>
  )
}

export default NewsDetails;