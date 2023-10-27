import React from "react";
import FineTable from "./FineTable";

const FineList = () => {
  return (
    <>
      <article className="mx-2 my-2 pb-10 text-md font-semibold">
        Có 100000 kết quả được tìm thấy
      </article>
      <div className="ml-2">
        <FineTable />
      </div>
    </>
  );
};

export default FineList;
