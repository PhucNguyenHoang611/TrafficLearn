import React, { useState } from "react";
import DatePicker from "react-datepicker";

import "react-datepicker/dist/react-datepicker.css";

// CSS Modules, react-datepicker-cssmodules.css
// import 'react-datepicker/dist/react-datepicker-cssmodules.css';

const CustomDatePicker = ({ date, onDateChange }) => {
  return (
    <DatePicker
      selected={date}
      onChange={onDateChange}
      className="block text-sm sm:text-base placeholder-gray-500 w-full px-4 py-2 pr-4 text-gray-700 bg-white border border-gray-400 rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500"
    />
  );
};

export default CustomDatePicker;
