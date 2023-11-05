import React, { useEffect } from "react";
import { useDispatch } from "react-redux";
import { Outlet } from "react-router-dom";

const RootPage = () => {
  const dispatch = useDispatch();
  useEffect(() => {
    document.title = "Trang chủ";
  }, []);

  return (
    <>
      <Outlet />
    </>
  );
};

export default RootPage;
