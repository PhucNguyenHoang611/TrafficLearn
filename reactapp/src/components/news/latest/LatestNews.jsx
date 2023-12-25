/* eslint-disable react-hooks/exhaustive-deps */
import { Box } from '@mui/material';
import React, { useEffect, useState } from 'react'
import { getAllNews } from '../../../apis/api_function';
import LoadingSpinner from '../../loading/LoadingSpinner';
import LatestNewsItem from './LatestNewsItem';

const LatestNews = () => {
  const [latestNews, setLatestNews] = useState([]);

  const getLatestNews = () => {
    try {
      getAllNews()
        .then((res) => {
          const result = res.data;
          const filteredResult = result.filter((item) => item.IsHidden == false);
          const sortedResult = filteredResult
          .sort((a, b) => new Date(b.NewsDate) - new Date(a.NewsDate))
          .slice(0, 5);

          setLatestNews(sortedResult);
        });
    } catch (error) {
      console.log(error);
    }
  };

  useEffect(() => {
    if (latestNews.length == 0) {
      getLatestNews();
    }
  }, []);

  return (
    <Box className="border border-gray-300">
      {latestNews.length > 0 && latestNews.map((item, index) => (
        <LatestNewsItem
          key={index}
          news={item}
          width={20}
          height={12} />
      ))}

      {(latestNews.length == 0) && <LoadingSpinner />}
    </Box>
  )
}

export default LatestNews;