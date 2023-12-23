import React from "react";

// const fines = [
//   { title: "Not following traffic sign", penalty: "100 - 200$" },
//   { title: "Speeding", penalty: "50 - 100$" },
//   { title: "Driving under the influence", penalty: "500 - 1000$" },
//   { title: "Using phone while driving", penalty: "50 - 100$" },
// ];

const FineTable = ({ fines }) => {
  return (
    <table className="w-full border-collapse">
      <thead>
        <tr>
          <th className="text-left font-bold px-4 py-2 bg-gray-100 border-b border-gray-200">
            Nội dung vi phạm
          </th>
          <th className="text-left font-bold px-4 py-2 bg-gray-100 border-b border-gray-200">
            Mức phạt
          </th>
        </tr>
      </thead>
      <tbody>
        {fines.map((fine, index) => (
          <tr
            key={index}
            className={`${
              index % 2 === 0 ? "bg-gray-100" : "bg-white"
            } border-b border-gray-200`}
          >
            <td className="px-4 py-2">{fine.title}</td>
            <td className="px-4 py-2">{fine.penalty}</td>
          </tr>
        ))}
      </tbody>
    </table>
  );
};

export default FineTable;
