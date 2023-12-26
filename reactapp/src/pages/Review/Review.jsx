import React, { useState } from "react";
import TitleList from "@/components/review/TitleList";
import LicenseMenu from "@/components/review/LicenseMenu";

const Review = () => {
  const [selectedLicense, setSelectedLicense] = useState("");

  return (
    <div>
      <section className="flex flex-col sm:flex-row h-screen">
        <LicenseMenu
          selectedLicense={selectedLicense}
          setSelectedLicense={setSelectedLicense} />
          
        <div className="flex-grow overflow-y-auto">
          <TitleList
            selectedLicense={selectedLicense} />
        </div>
      </section>
    </div>
  );
};

export default Review;