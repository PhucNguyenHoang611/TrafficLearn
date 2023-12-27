/* eslint-disable react-hooks/exhaustive-deps */

import React, { useEffect, useState } from "react";
import { Box } from "@mui/material";
import LoadingSpinner from "../loading/LoadingSpinner";

import { getAllLicenses } from "@/apis/api_function"

const LicenseMenu = ({
  selectedLicense,
  setSelectedLicense,
  setLicensesList }) => {

  const [licenses, setLicenses] = useState([]);

  const handleChangeLicense = (id) => {
    setSelectedLicense(id);
  }

  const getLicenses = () => {
    try {
      getAllLicenses()
        .then((res) => {
          setLicenses(res.data);
          setLicensesList(res.data);
        });
    } catch (error) {
      console.log(error);
    }
  };

  useEffect(() => {
    if (licenses.length == 0) {
      getLicenses()
    }
  }, []);

  return (
    <nav className="z-10 shadow-md w-full sm:w-56 flex-shrink-0">
      <div className="flex flex-col h-full">
        <div className="flex items-center justify-center h-16">
          <h1 className="text-den font-bold text-xl">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              fill="none"
              viewBox="0 0 24 24"
              strokeWidth={1.5}
              stroke="currentColor"
              className="w-6 h-6"
            >
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                d="M12 21v-8.25M15.75 21v-8.25M8.25 21v-8.25M3 9l9-6 9 6m-1.5 12V10.332A48.36 48.36 0 0012 9.75c-2.551 0-5.056.2-7.5.582V21M3 21h18M12 6.75h.008v.008H12V6.75z"
              />
            </svg>
          </h1>
        </div>
        <div className="flex flex-col flex-grow">
          <p className="ml-2 mt-2">Loại GPLX:</p>
          <hr className="bg-gray-500 border-1 border-gray-300 mx-2" />

          {/* License filter: Small screen */}
          <select
            onChange={(e) => handleChangeLicense(e.target.value)}
            value={selectedLicense}
            className="sm:hidden w-full my-2 px-3 py-2 rounded-md border border-gray-300 focus:outline-none focus:ring-2 focus:ring-blue-400 focus:border-transparent"
          >
            {/* <option key={-1} value="all">Tất cả</option> */}
            {(licenses.length > 0) && licenses.map((item, index) => (
                <option key={index} value={item.Id}>
                  {(item.LicenseName !== "Tình huống mô phỏng")
                  ? `Hạng ${item.LicenseName}`
                  : "120 tình huống mô phỏng giao thông"}
                </option>
            ))}
          </select>

          {/* License filter: Large screen */}
          <div className="hidden sm:block">
            {(licenses.length > 0) && licenses.map((item, index) => (
              <Box
                key={index}
                onClick={() => handleChangeLicense(item.Id)}
                className={(selectedLicense == item.Id)
                  ? "bg-gray-900 text-white px-3 py-2 rounded-md text-sm font-medium cursor-pointer"
                  : "text-gray-800 no-underline hover:bg-gray-700 hover:text-white px-3 py-2 rounded-md text-sm font-medium cursor-pointer"}
              >
                {(item.LicenseName !== "Tình huống mô phỏng")
                  ? `Hạng ${item.LicenseName}`
                  : "120 tình huống mô phỏng giao thông"}
              </Box>
            ))}

            {(licenses.length == 0) && <LoadingSpinner />}
          </div>
        </div>
      </div>
    </nav>
  );
};

export default LicenseMenu;