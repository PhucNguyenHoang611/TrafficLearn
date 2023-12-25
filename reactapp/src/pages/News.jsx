import { Box, Typography } from '@mui/material';
import React from 'react'
import { Outlet } from 'react-router-dom';
import LatestNews from '../components/news/latest/LatestNews';

const News = () => {
  return (
    <Box className="flex gap-4 justify-center max-[1300px]:flex-col-reverse mb-4">
      <Box className="min-[1300px]:w-4/12 overflow-hidden">
        <p className="mb-2">
          <b>TIN TỨC MỚI NHẤT</b>
        </p>

        <LatestNews />
      </Box>
      <Box className="w-full min-[1300px]:w-8/12">
        <Outlet />
      </Box>
    </Box>
  )
}

export default News;