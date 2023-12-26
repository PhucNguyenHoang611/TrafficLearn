/* eslint-disable react-hooks/exhaustive-deps */

import React, { useEffect, useState } from "react";
import SignCard from "./SignCard";
import LoadingItem from "../loading/LoadingItem";

import { getAllTitles } from "@/apis/api_function"
import { Box } from "@mui/material";

const TitleList = ({ 
  licenses,
  // searchValue,
  selectedLicense }) => {

  const [titles, setTitles] = useState([]);
  const [titlesTemp, setTitlesTemp] = useState([]);
  const [checkLoading, setCheckLoading] = useState(false);

  const getTitles = () => {
    try {
      getAllTitles()
        .then((res) => {
          const result = res.data;

          setTitles(result);
          setTitlesTemp(result);
          setCheckLoading(true);
        });
    } catch (error) {
      console.log(error);
    }
  };

  const filterBySignType = (titles, signType) => {
    if (signType == "all")
      return titles;
    else
      return titles.filter((item) => item.SignTypeId === signType);
  };

  const filterBySearch = (titles, input) => {
    if (input == "")
      return titles;
    else
      return titles.filter((item) => item.SignName.toLowerCase().includes(input.toLowerCase()));
  };

  // useEffect(() => {
  //   const filteredTitlesByType = filterBySignType(titlesTemp, selectedLicense);
  //   const filteredResult = filterBySearch(filteredTitlesByType, searchValue);

  //   setTitles(filteredResult);
  // }, [searchValue, selectedLicense]);
  
  useEffect(() => {
    if (titles.length == 0) {
      getTitles();
    }
  }, []);

  return (
    <div>
      {/* <article className="mx-2 my-2 pb-10 text-md font-semibold">
        Có {titles.length} kết quả được tìm thấy
      </article> */}
      {(titles.length > 0) && (
        <div className="ml-4">
          {titles.map((item1, index) => {
            const matchingItem = licenses.find((item2) => item1.SignTypeId === item2.Id);
            return (
              <SignCard
                key={index}
                sign={item1}
                signTypeName={matchingItem ? matchingItem.SignType : ""} />
            );
          })}
        </div>
      )}

      {(titles.length == 0 && !checkLoading) && (
        <>
          <LoadingItem />
          <LoadingItem />
          <LoadingItem />
        </>
      )}

      {(titles.length == 0 && checkLoading) && (
        <Box className="ml-4">
          Không có kết quả phù hợp
        </Box>
      )}
    </div>
  );
};

export default TitleList;