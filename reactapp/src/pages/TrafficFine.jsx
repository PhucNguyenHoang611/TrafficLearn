import React, { useState } from "react";
import FineList from "@/components/trafficfine/FineList";
import FineMenu from "@/components/trafficfine/FineMenu";
import FineVehicle from "@/components/trafficfine/FineVehicle";

const TrafficFine = () => {
  const [searchValue, setSearchValue] = useState("");
  const [fineTypes, setFineTypes] = useState([]);
  const [selectedFineType, setSelectedFineType] = useState("all");

  React.useEffect(() => {
    document.title = "Tra cứu mức phạt";
  }, []);

  return (
    <div>
      <section className="sm:hidden flex justify-center">
        <FineVehicle />
      </section>
      <section className="flex flex-col sm:flex-row h-screen">
        <FineMenu
          fineTypes={fineTypes}
          setFineTypes={setFineTypes}
          searchValue={searchValue}
          setSearchValue={setSearchValue}
          selectedFineType={selectedFineType}
          setSelectedFineType={setSelectedFineType} />
        <div className="flex-grow overflow-y-auto">
          <FineList
            fineTypes={fineTypes}
            searchValue={searchValue}
            setSearchValue={setSearchValue}
            selectedFineType={selectedFineType}
            setSelectedFineType={setSelectedFineType} />
        </div>
      </section>
    </div>
  );
};

export default TrafficFine;
