import React from "react";

const FineTable = ({ fines, fineTypes }) => {
  return (
    <table className="w-full border-collapse">
      <thead>
        <tr>
          <th className="text-left font-bold px-4 py-2 bg-gray-100 border-b border-gray-200">
            Nội dung vi phạm
          </th>
          <th className="text-left font-bold px-4 py-2 bg-gray-100 border-b border-gray-200">
            Loại vi phạm
          </th>
        </tr>
      </thead>
      <tbody>
        {fines.map((item1, index) => {
          const matchingItem2 = fineTypes.find((item2) => item1.FineTypeId === item2.Id);
          return (
            <tr
              key={index}
              className={`${
                index % 2 === 0 ? "bg-white" : "bg-gray-100"
              } border-b border-gray-200 cursor-pointer hover:bg-gray-200`}
              onClick={() => console.log("Get fine details")}
            >
              <td className="truncate px-4 py-2 w-2/3 max-w-0">
                {item1.FineName}
              </td>
              <td className="truncate px-4 py-2 w-1/3 max-w-0">
                {matchingItem2 ? matchingItem2.FineType : ""}
              </td>
            </tr>
          );
        })}
      </tbody>
    </table>
  );
};

export default FineTable;
