import React, { useState } from "react";
import TitleList from "@/components/review/TitleList";
import LicenseMenu from "@/components/review/LicenseMenu";

const Review = () => {
  // const [searchValue, setSearchValue] = useState("");
  // const [licenses, setLicenses] = useState([]);
  const [selectedLicense, setSelectedLicense] = useState("");

  return (
    <div>
      <section className="flex flex-col sm:flex-row h-screen">
        <LicenseMenu
          // licenses={licenses}
          // setLicenses={setLicenses}
          // searchValue={searchValue}
          // setSearchValue={setSearchValue}
          selectedLicense={selectedLicense}
          setSelectedLicense={setSelectedLicense} />
        <div className="flex-grow overflow-y-auto">
          <TitleList
            // licenses={licenses}
            // searchValue={searchValue}
            selectedLicense={selectedLicense} />
        </div>
      </section>
    </div>
  );
};

export default Review;