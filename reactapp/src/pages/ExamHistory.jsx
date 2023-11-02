import React from "react";
import ExamTable from "@/components/examhistory/ExamTable";

const ExamHistory = () => {
  return (
    <section>
      <h2 className="text-center font-bold text-xl my-8 pt-4">Kết quả thi</h2>
      <div className="max-w-screen flex justify-center">
        <ExamTable></ExamTable>
      </div>
    </section>
  );
};

export default ExamHistory;
