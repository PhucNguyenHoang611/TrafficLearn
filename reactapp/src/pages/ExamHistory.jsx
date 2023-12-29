/* eslint-disable react-hooks/exhaustive-deps */
import React, { useEffect, useState } from "react";
import { useSelector } from 'react-redux';
import ExamTable from "@/components/examhistory/ExamTable";
import { getAllExaminationResults, getExamination, getLicense } from "../apis/api_function";
import LoadingSpinner from "../components/loading/LoadingSpinner";

const ExamHistory = () => {
  const [examResults , setExamResults] = useState([]);
  const auth = useSelector((state) => state.auth);

  const getResults = async () => {
    try {
      const response = await getAllExaminationResults(auth.id, auth.token);
      const results = response.data;

      const getExamInfoPromises = results.map(async (item) => {
        const exam = await getExamination(item.ExaminationId);
        const license = await getLicense(exam.data.LicenseId);

        return {
          // examinationId: exam.data.Id,
          examinationName: exam.data.ExaminationName,
          license: license.data.LicenseName,
          examinationDate: new Date(item.ExaminationDate).toLocaleDateString("vi-VN", {
            day: "numeric",
            month: "long",
            year: "numeric",
          }),
          score: item.Score,
          isPassed: item.IsPassed ? "Đạt" : "Chưa đạt"
        };
      });

      const result = await Promise.all(getExamInfoPromises);
      setExamResults(result);
    } catch (error) {
      console.log(error);
    }
  };

  useEffect(() => {
    document.title = "Lịch sử bài thi";
    if (examResults.length == 0) {
      getResults();
    }
  }, [])

  return (
    <section>
      <h2 className="text-center font-bold text-3xl my-8 pt-4">Lịch sử làm bài</h2>
      <div className="max-w-screen flex justify-center mb-8">
        {examResults.length == 0 ? <LoadingSpinner /> : <ExamTable results={examResults}></ExamTable>}
      </div>
    </section>
  );
};

export default ExamHistory;
