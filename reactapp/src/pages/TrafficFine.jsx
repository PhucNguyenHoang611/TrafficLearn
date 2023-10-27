import React from "react";
import { NavLink, Outlet } from "react-router-dom";
import FineList from "../components/trafficfine/FineList";
import FineMenu from "../components/trafficfine/FineMenu";

const TrafficFine = () => {
  return (
    <section className="flex flex-row h-screen ">
      <FineMenu></FineMenu>
      <div className="flex-grow">
        <FineList />
      </div>
    </section>
  );
};

export default TrafficFine;
