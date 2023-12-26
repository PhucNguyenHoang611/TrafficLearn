/* eslint-disable react-hooks/exhaustive-deps */

import React, { useEffect, useState } from "react";
import SignCard from "./SignCard";
import LoadingItem from "../loading/LoadingItem";

import { getLicenseTitlesByLicenseId } from "@/apis/api_function"
import { Box } from "@mui/material";

const TitleList = ({ 
  // licenses,
  // searchValue,
  selectedLicense }) => {

  const [licenseTitles, setLicenseTitles] = useState([]);
  const [licenseTitlesTemp, setLicenseTitlesTemp] = useState([]);
  // const [checkLoading, setCheckLoading] = useState(false);

  const getLicenseTitles = () => {
    try {
      getLicenseTitlesByLicenseId(selectedLicense)
        .then((res) => {
          const result = res.data;

          setLicenseTitles(result);
          setLicenseTitlesTemp(result);
          // setCheckLoading(true);
        });
    } catch (error) {
      console.log(error);
    }
  };

  const filterByLicense = (licenseTitles, signType) => {
    if (signType == "all")
      return licenseTitles;
    else
      return licenseTitles.filter((item) => item.SignTypeId === signType);
  };

  const filterBySearch = (licenseTitles, input) => {
    if (input == "")
      return licenseTitles;
    else
      return licenseTitles.filter((item) => item.SignName.toLowerCase().includes(input.toLowerCase()));
  };

  // useEffect(() => {
  //   const filteredLicenseTitlesByType = filterBySignType(licenseTitlesTemp, selectedLicense);
  //   const filteredResult = filterBySearch(filteredLicenseTitlesByType, searchValue);

  //   setLicenseTitles(filteredResult);
  // }, [searchValue, selectedLicense]);
  
  useEffect(() => {
    if (licenseTitles.length == 0) {
      getLicenseTitles();
    }
  }, []);

  return (
    <div>
      {/* <article className="mx-2 my-2 pb-10 text-md font-semibold">
        Có {licenseTitles.length} kết quả được tìm thấy
      </article> */}

      {/* {(licenseTitles.length > 0) && (
        <div className="ml-4">
          {licenseTitles.map((item1, index) => {
            const matchingItem = licenses.find((item2) => item1.SignTypeId === item2.Id);
            return (
              <SignCard
                key={index}
                sign={item1}
                signTypeName={matchingItem ? matchingItem.SignType : ""} />
            );
          })}
        </div>
      )} */}

      {(licenseTitles.length == 0) && (
        <>
          <LoadingItem />
          <LoadingItem />
          <LoadingItem />
        </>
      )}
    </div>
  );
};

export default TitleList;