import React, { useState } from 'react';
import LicenseMenu from "@/components/review/LicenseMenu";
import ExaminationList from "@/components/review/examination/ExaminationList";

const Examination = () => {
  const [selectedLicense, setSelectedLicense] = useState("all");
  const [licensesList, setLicensesList] = useState([]);

  React.useEffect(() => {
    document.title = "Thi thử";
  }, []);

  return (
    <div>
      <section className="flex flex-col sm:flex-row h-screen">
        <LicenseMenu
          selectedLicense={selectedLicense}
          setSelectedLicense={setSelectedLicense}
          setLicensesList={setLicensesList} />
          
        <div className="flex-grow overflow-y-auto">
          <ExaminationList
            licensesList={licensesList}
            selectedLicense={selectedLicense} />
        </div>
      </section>
    </div>
  );
}

export default Examination;