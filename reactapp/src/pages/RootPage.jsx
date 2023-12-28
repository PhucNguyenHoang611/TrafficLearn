import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Outlet, useNavigate } from "react-router-dom";
import { toast, ToastContainer } from "react-toastify";
import { ggLogin } from "../redux/reducers/auth_reducers";
import { googleLoginSuccess } from "../apis/api_function";

const RootPage = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const notify = useSelector((state) => state.notify);
  const provider = useSelector((state) => state.auth.provider);
  useEffect(() => {
    // document.title = "Trang chủ";

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
            dispatch({ type: "UN_NOTIFY" });
            break;
          case "error":
            toast.error(message);
            dispatch({ type: "UN_NOTIFY" });
            break;
          case "warning":
            toast.warning(message);
            dispatch({ type: "UN_NOTIFY" });
            break;
          default:
            break;
        }
      }
    },
    // eslint-disable-next-line react-hooks/exhaustive-deps
    [notify]
  );

  useEffect(() => {
    const handleLoginWithGoogle = async () => {
      try {
        const result = await googleLoginSuccess();
        console.log("rs", result);

        const ggUser = result.data;

        const userLogin = {
          id: ggUser.userId,
          email: ggUser.userEmail,
          firstName: ggUser.userFirstName,
          lastName: ggUser.userLastName,
        };

        dispatch(ggLogin(userLogin));
        navigate("/");
      } catch (error) {
        if (notify.type !== "error") {
          dispatch({
            type: "NOTIFY",
            payload: {
              type: "error",
              message: "Đăng nhập thất bại, vui lòng thử lại!",
            },
          });
        }
        console.log("error", error);
        // dispatch({ type: "LOGOUT" });
      }
    };
    if (provider === "google") handleLoginWithGoogle();
  }, [provider, dispatch, navigate]);

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
