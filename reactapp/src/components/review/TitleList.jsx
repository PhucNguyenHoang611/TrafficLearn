/* eslint-disable react-hooks/exhaustive-deps */

import React, { useEffect, useState } from "react";
import LoadingItem from "../loading/LoadingItem";

import {
  getLicenseTitlesByLicenseId,
  getAllQuestionsByLicenseId,
  getAllImportantQuestionsByLicenseId,
  getQuestionsByLicenseTitleId } from "@/apis/api_function"

import { Box } from "@mui/material";
import TitleCard from "./TitleCard";

const TitleList = ({ selectedLicense }) => {

  const [startScreen, setStartScreen] = useState(true);
  const [isLoading, setIsLoading] = useState(false);

  const [licenseTitles, setLicenseTitles] = useState([]);

  const [allQuestions, setAllQuestions] = useState(null);
  const [allImportantQuestions, setAllImportantQuestions] = useState(null);

  const getLicenseTitles = async () => {
    setIsLoading(true);

    try {
      const response = await getLicenseTitlesByLicenseId(selectedLicense);
      const licenseTitlesResult = response.data;

      const getQuestionsPromises = licenseTitlesResult.map(async (item) => {
        const questions = await getQuestionsByLicenseTitleId(item.LicenseTitle.Id);
        return { licenseTitle: item, questions: questions.data };
      });
      
      const result = await Promise.all(getQuestionsPromises);
      setLicenseTitles(result);

      await getAllQuestions(selectedLicense);
      await getAllImportantQuestions(selectedLicense);
    } catch (error) {
      console.log(error);
    }

    setIsLoading(false);
  };

  const getAllQuestions = (id) => {
    try {
      getAllQuestionsByLicenseId(id)
        .then((res) => {
          setAllQuestions(res.data);
        });
    } catch (error) {
      console.log(error);
    }
  };

  const getAllImportantQuestions = (id) => {
    try {
      getAllImportantQuestionsByLicenseId(id)
        .then((res) => {
          setAllImportantQuestions(res.data)
        });
    } catch (error) {
      console.log(error);
    }
  };

  useEffect(() => {
    if (selectedLicense != "") {
      setStartScreen(false);
      getLicenseTitles();
    }
  }, [selectedLicense]);

  return (
    <div>
      {(licenseTitles.length > 0 && allQuestions && allImportantQuestions) && (
        <div className="ml-4">
          <TitleCard
            key={0}
            titleName="Toàn bộ câu hỏi trong bộ đề"
            total={allQuestions.total}
            numberOfImportantQuestions={allQuestions.numberOfImportantQuestions}
            questionsList={allQuestions} />

          {licenseTitles.map((item, index) => {
            return (
              <TitleCard
                key={index + 1}
                titleName={item.licenseTitle.Title.TitleName}
                total={item.questions.total}
                numberOfImportantQuestions={item.questions.numberOfImportantQuestions}
                questionsList={item.questions} />
            );
          })}

          <TitleCard
            key={licenseTitles.length + 1}
            titleName="Các câu điểm liệt trong bộ đề"
            total={allImportantQuestions.total}
            numberOfImportantQuestions={allImportantQuestions.total}
            questionsList={allImportantQuestions} />
        </div>
      )}

      {isLoading && (
        <>
          <LoadingItem />
          <LoadingItem />
          <LoadingItem />
        </>
      )}

      {startScreen && (
        <Box className="ml-4">
          Chọn hạng giấy phép lái xe để bắt đầu
        </Box>
      )}

      {(licenseTitles.length == 0 && !startScreen && !isLoading) && (
        <Box className="ml-4">
          Không có dữ liệu
        </Box>
      )}
    </div>
  );
};

export default TitleList;