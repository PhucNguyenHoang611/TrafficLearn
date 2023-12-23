/* eslint-disable react-hooks/exhaustive-deps */
import React, { useEffect, useState } from "react";
import FineTable from "./FineTable";
import FineCard from "./FineCard";

import { useParams } from "react-router-dom";

import { getAllTrafficFines } from "@/apis/api_function"

const FineList = () => {
  const { vehicleType } = useParams();
  const [fines, setFines] = useState([]);
  const [allFines, setAllFines] = useState([]);

  const getFines = () => {
    try {
      getAllTrafficFines()
        .then((res) => {
          const result = res.data;
          const filteredResult = filterByVehicleType(result, vehicleType);

          setAllFines(result);
          setFines(filteredResult);
        });
    } catch (error) {
      console.log(error);
    }
  };

  const filterByVehicleType = (fines, vehicleType) => {
    return fines.filter((item) => item.VehicleType === vehicleType);
  };

  useEffect(() => {
    if (allFines.length == 0) {
      getFines();
    }
  }, []);

  useEffect(() => {
    if (allFines.length > 0) {
      const filteredFines = filterByVehicleType(allFines, vehicleType);
      setFines(filteredFines);
    }
  }, [vehicleType]);

  return (
    <>
      <article className="mx-2 my-2 pb-10 text-md font-semibold">
        Có {fines.length} kết quả được tìm thấy
      </article>
      {(fines.length > 0) && (
        <>
          <div className="ml-4">
            <FineTable
              fines={fines} />
          </div>
        </>
      )}
    </>
  );
};

export default FineList;
