/* eslint-disable react-hooks/exhaustive-deps */
import React, { useEffect, useState } from 'react'
import { getAllExaminations } from '../../../apis/api_function';
import ExaminationCard from './ExaminationCard';
import LoadingItem from "../../loading/LoadingItem";
import { Box } from '@mui/material';

const ExaminationList = ({ selectedLicense, licensesList }) => {
  const [examinations, setExaminations] = useState([]);
  const [examinationsTemp, setExaminationsTemp] = useState([]);
  const [checkLoading, setCheckLoading] = useState(false);

  const getExaminations = () => {
    try {
      getAllExaminations()
        .then((res) => {
          setExaminations(res.data);
          setExaminationsTemp(res.data);
          setCheckLoading(true);
        });
    } catch (error) {
      console.log(error);
    }
  };

  const filterByLicense = (exams, license) => {
    if (license == "all")
      return exams;
    else
      return exams.filter((item) => item.LicenseId === license);
  };

  useEffect(() => {
    const filteredResult = filterByLicense(examinationsTemp, selectedLicense);
    setExaminations(filteredResult);
  }, [selectedLicense]);

  useEffect(() => {
    if (examinations.length == 0) {
      getExaminations();
    }
  }, []);
  
  return (
    <div>
      {(examinations.length > 0) && (
        <div className="ml-4">
          {examinations.map((item1, index) => {
            const matchingItem = licensesList.find((item2) => item1.LicenseId === item2.Id);
            return (
              <ExaminationCard
                key={index}
                examination={item1}
                licenseName={matchingItem ? matchingItem.LicenseName : ""} />
            );
          })}
        </div>
      )}

      {(examinations.length == 0 && !checkLoading) && (
        <>
          <LoadingItem />
          <LoadingItem />
          <LoadingItem />
        </>
      )}

      {(examinations.length == 0 && checkLoading) && (
        <Box className="ml-4">
          Không có kết quả phù hợp
        </Box>
      )}
    </div>
  )
}

export default ExaminationList;