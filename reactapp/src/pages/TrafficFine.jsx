import React from "react";
import { NavLink, Outlet } from "react-router-dom";
import FineList from "@/components/trafficfine/FineList";
import FineMenu from "@/components/trafficfine/FineMenu";
import FineVehicle from "@/components/trafficfine/FineVehicle";

const TrafficFine = () => {
  return (
    <div>
      <section className="sm:hidden flex justify-center">
        <FineVehicle />
      </section>
      <section className="sm:flex flex-row h-screen ">
        <FineMenu></FineMenu>
        <div className="flex-grow">
          <FineList />
        </div>
      </section>
    </div>
  );
};

export default TrafficFine;
