/* eslint-disable react-hooks/exhaustive-deps */

import React, { useEffect, useState } from "react";
import SignCard from "./SignCard";
import LoadingItem from "../loading/LoadingItem";

import { getAllTrafficSigns } from "@/apis/api_function"
import { Box } from "@mui/material";

const SignList = ({ 
  signTypes,
  searchValue,
  selectedSignType }) => {

  const [signs, setSigns] = useState([]);
  const [signsTemp, setSignsTemp] = useState([]);
  const [checkLoading, setCheckLoading] = useState(false);

  const getSigns = () => {
    try {
      getAllTrafficSigns()
        .then((res) => {
          const result = res.data;

          setSigns(result);
          setSignsTemp(result);
          setCheckLoading(true);
        });
    } catch (error) {
      console.log(error);
    }
  };

  const filterBySignType = (signs, signType) => {
    if (signType == "all")
      return signs;
    else
      return signs.filter((item) => item.SignTypeId === signType);
  };

  const filterBySearch = (signs, input) => {
    if (input == "")
      return signs;
    else
      return signs.filter((item) => item.SignName.toLowerCase().includes(input.toLowerCase()));
  };

  useEffect(() => {
    const filteredSignsByType = filterBySignType(signsTemp, selectedSignType);
    const filteredResult = filterBySearch(filteredSignsByType, searchValue);

    setSigns(filteredResult);
  }, [searchValue, selectedSignType]);
  
  useEffect(() => {
    if (signs.length == 0) {
      getSigns();
    }
  }, []);

  return (
    <div>
      <article className="mx-2 my-2 pb-10 text-md font-semibold">
        Có {signs.length} kết quả được tìm thấy
      </article>
      {(signs.length > 0) && (
        <div className="ml-4">
          {signs.map((item1, index) => {
            const matchingItem = signTypes.find((item2) => item1.SignTypeId === item2.Id);
            return (
              <SignCard
                key={index}
                sign={item1}
                signTypeName={matchingItem ? matchingItem.SignType : ""} />
            );
          })}
        </div>
      )}

      {(signs.length == 0 && !checkLoading) && (
        <>
          <LoadingItem />
          <LoadingItem />
          <LoadingItem />
        </>
      )}

      {(signs.length == 0 && checkLoading) && (
        <Box className="ml-4">
          Không có kết quả phù hợp
        </Box>
      )}
    </div>
  );
};

export default SignList;