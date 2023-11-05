import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Outlet } from "react-router-dom";
import { toast, ToastContainer } from "react-toastify";

const RootPage = () => {
  const dispatch = useDispatch();
  const notify = useSelector((state) => state.notify);
  useEffect(() => {
    document.title = "Trang chủ";

    dispatch({
      type: "UN_NOTIFY",
    });
  }, [dispatch]);

  useEffect(
    () => {
      if (notify) {
        const { type, message } = notify;
        switch (type) {
          case "success":
            toast.success(message);
            break;
          case "error":
            toast.error(message);
            break;
          case "warning":
            toast.warning(message);
            break;
          default:
            break;
        }
      }
    },
    // eslint-disable-next-line react-hooks/exhaustive-deps
    [notify]
  );

  return (
    <>
      <Outlet />
      <ToastContainer
        position="top-right"
        autoClose={5000}
        hideProgressBar={false}
        newestOnTop
        closeOnClick
        rtl={false}
        pauseOnFocusLoss
        draggable
        pauseOnHover
      />
    </>
  );
};

export default RootPage;
