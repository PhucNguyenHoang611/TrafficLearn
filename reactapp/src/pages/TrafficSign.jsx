import React, { useState } from "react";
import SignList from "@/components/trafficsign/SignList";
import SignMenu from "@/components/trafficsign/SignMenu";

const TrafficSign = () => {
  const [searchValue, setSearchValue] = useState("");
  const [signTypes, setSignTypes] = useState([]);
  const [selectedSignType, setSelectedSignType] = useState("all");

  return (
    <div>
      <section className="flex flex-col sm:flex-row h-screen">
        <SignMenu
          signTypes={signTypes}
          setSignTypes={setSignTypes}
          searchValue={searchValue}
          setSearchValue={setSearchValue}
          selectedSignType={selectedSignType}
          setSelectedSignType={setSelectedSignType} />
        <div className="flex-grow overflow-y-auto">
          <SignList
            signTypes={signTypes}
            searchValue={searchValue}
            selectedSignType={selectedSignType} />
        </div>
      </section>
    </div>
  );
};

export default TrafficSign;
