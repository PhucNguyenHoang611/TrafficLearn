/* eslint-disable react-hooks/exhaustive-deps */
import React, { useEffect, useState } from 'react'
import { getAllExaminations } from '../../../apis/api_function';
import ExaminationCard from './ExaminationCard';
import LoadingItem from "../../loading/LoadingItem";
import { Box } from '@mui/material';
import { licenseInfo } from "../../../constants/constants";
import { useSelector } from 'react-redux';

const ExaminationList = ({ selectedLicense, licensesList }) => {
  const [examinations, setExaminations] = useState([]);
  const [examinationsTemp, setExaminationsTemp] = useState([]);
  const [checkLoading, setCheckLoading] = useState(false);
  const auth = useSelector((state) => state.auth);

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

  const getNumberOfQuestions = (licenseName) => {
    const license = licenseInfo.find((license) => license.licenseName === licenseName);
    return license.numberOfQuestions;
  }

  const getTimeRemaining = (licenseName) => {
    const license = licenseInfo.find((license) => license.licenseName === licenseName);
    return license.time;
  }

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
                auth={auth}
                examination={item1}
                licenseName={matchingItem ? matchingItem.LicenseName : ""}
                numberOfQuestions={matchingItem ? getNumberOfQuestions(matchingItem.LicenseName) : 0}
                timeRemaining={matchingItem ? getTimeRemaining(matchingItem.LicenseName) : 0} />
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