/* eslint-disable react-hooks/exhaustive-deps */
import React, { useEffect, useState } from 'react'
import { getAllNews } from '../../apis/api_function';
import { Box, Typography } from '@mui/material';
import NewsItem from './NewsItem';
import LoadingItem from '../loading/LoadingItem';


const NewsList = () => {
  const [news, setNews] = useState([]);
  const [newsTemp, setNewsTemp] = useState([]);
  const [searchValue, setSearchValue] = useState("");
  const [checkLoading, setCheckLoading] = useState(false);

  const getNews = () => {
    try {
      getAllNews()
        .then((res) => {
          const result = res.data;
          const filteredResult = result.filter((item) => item.IsHidden == false);
          const sortedResult = filteredResult
          .sort((a, b) => new Date(b.NewsDate) - new Date(a.NewsDate))

          setNews(sortedResult);
          setNewsTemp(sortedResult);
          setCheckLoading(true);
        });
    } catch (error) {
      console.log(error);
    }
  };

  const handleSearchChange = (e) => {
    setSearchValue(e.target.value);
  }

  const filterBySearch = (news, input) => {
    if (input == "")
      return news;
    else
      return news.filter((item) => item.NewsTitle.toLowerCase().includes(input.toLowerCase()));
  };

  useEffect(() => {
    if (news.length == 0) {
      getNews();
    }
  }, []);

  useEffect(() => {
    const filteredResult = filterBySearch(newsTemp, searchValue);
    setNews(filteredResult);
  }, [searchValue]);

  return (
    <Box>
      <Box className="flex max-sm:flex-col sm:justify-between items-center">
        <Typography variant="h5">
          <b>Tin tức giao thông</b>
        </Typography>

        <Box>
          <div className="p-2 relative">
            <input
              type="text"
              placeholder="Tìm kiếm tin tức"
              value={searchValue}
              onChange={handleSearchChange}
              className="w-full px-3 py-2 rounded-md border border-gray-300 focus:outline-none focus:ring-2 focus:ring-blue-400 focus:border-transparent"
            />
            <span className="absolute inset-y-0 right-0 flex items-center pr-3">
              <svg
                className="w-5 h-5 text-gray-400"
                fill="none"
                stroke="currentColor"
                viewBox="0 0 24 24"
                xmlns="http://www.w3.org/2000/svg"
              >
                <path
                  strokeLinecap="round"
                  strokeLinejoin="round"
                  strokeWidth="2"
                  d="M21 21l-4.35-4.35"
                />
                <path
                  strokeLinecap="round"
                  strokeLinejoin="round"
                  strokeWidth="2"
                  d="M15.5 10.5a5 5 0 11-10 0 5 5 0 0110 0z"
                />
              </svg>
            </span>
          </div>
        </Box>
      </Box>

      <Box>
      {(news.length > 0) && news.map((item, index) => (
        <NewsItem
          key={index}
          news={item}
          width={40}
          height={22} />
      ))}

      {(news.length == 0 && !checkLoading) && <LoadingItem />}

      {(news.length == 0 && checkLoading) && (
        <Box className="m-4">
          Không có kết quả phù hợp
        </Box>
      )}
      </Box>
    </Box>
  )
}

export default NewsList;