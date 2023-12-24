/* eslint-disable react-hooks/exhaustive-deps */
import React, { useEffect, useState } from "react";
import FineTable from "./FineTable";
import FineCard from "./FineCard";

import { useParams } from "react-router-dom";

import { getAllTrafficFines } from "@/apis/api_function"

const FineList = ({ 
  fineTypes,
  searchValue,
  setSearchValue,
  selectedFineType,
  setSelectedFineType }) => {

  const { vehicleType } = useParams();
  const [fines, setFines] = useState([]);
  const [finesTemp, setFinesTemp] = useState([]);
  const [allFines, setAllFines] = useState([]);

  const getFines = () => {
    try {
      getAllTrafficFines()
        .then((res) => {
          const result = res.data;
          const filteredResult = filterByVehicleType(result, vehicleType);

          setAllFines(result);
          setFines(filteredResult);
          setFinesTemp(filteredResult);
        });
    } catch (error) {
      console.log(error);
    }
  };

  const filterByVehicleType = (fines, vehicleType) => {
    return fines.filter((item) => item.VehicleType === vehicleType);
  };

  const filterByFineType = (fines, fineType) => {
    if (fineType == "all")
      return fines;
    else
      return fines.filter((item) => item.FineTypeId === fineType);
  };

  const filterBySearch = (fines, input) => {
    if (input == "")
      return fines;
    else
      return fines.filter((item) => item.FineName.toLowerCase().includes(input.toLowerCase()));
  };
  
  useEffect(() => {
    if (allFines.length > 0) {
      const filteredFines = filterByVehicleType(allFines, vehicleType);
      setFines(filteredFines);
      setFinesTemp(filteredFines);

      setSearchValue("");
      setSelectedFineType("all");
    }
  }, [vehicleType]);

  useEffect(() => {
    const filteredFinesByType = filterByFineType(finesTemp, selectedFineType);
    const filteredResult = filterBySearch(filteredFinesByType, searchValue);

    setFines(filteredResult);
  }, [searchValue, selectedFineType]);
  
  useEffect(() => {
    if (allFines.length == 0) {
      getFines();
    }
  }, []);

  return (
    <div>
      <article className="mx-2 my-2 pb-10 text-md font-semibold">
        Có {fines.length} kết quả được tìm thấy
      </article>
      {(fines.length > 0) && (
        <div className="ml-4">
          {/* <FineTable
            fines={fines}
            fineTypes={fineTypes} /> */}

          {fines.map((item1, index) => {
            const matchingItem = fineTypes.find((item2) => item1.FineTypeId === item2.Id);
            return (
              <FineCard
                key={index}
                fine={item1}
                fineTypeName={matchingItem ? matchingItem.FineType : ""} />
            );
          })}
        </div>
      )}
    </div>
  );
};

export default FineList;
