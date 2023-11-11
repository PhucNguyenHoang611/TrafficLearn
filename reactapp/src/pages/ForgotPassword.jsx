import React, { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { useForm } from "react-hook-form";
// import { login } from "../apis/api_function";
import { useDispatch } from "react-redux";
import { Visibility, VisibilityOff, Send } from "@mui/icons-material";
import ThumbUpAltIcon from "@mui/icons-material/ThumbUpAlt";
import { toast, ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { forgetPassword } from "../apis/api_function";

const ForgotPassword = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm();
  const [loading, setLoading] = useState(false);
  const [isNotify, setIsNotify] = useState(false);

  const onSubmit = async (data) => {
    if (loading) {
      return;
    }
    setLoading(true);
    try {
      const res = await forgetPassword(data.email);
      if (res.status === 200) {
        dispatch({
          type: "SET_EMAIL",
          payload: { email: data.email, TOTP: "" },
        });
        dispatch({ type: "CURRENT", payload: { current: "verifyPassword" } });
        toast.success("Vui lòng kiểm tra email của bạn !");
        setLoading(false);
        navigate("/verify");
      }
    } catch (error) {
      setLoading(false);
      if (error.response.data.error === "Email doesn't exist !") {
        toast.error("Email không tồn tại !");
      }
      if (error.response.data.error === "Incorrect password !") {
        toast.error("Mật khẩu không đúng !");
      }
    }
  };

  return (
    <div>
      <link
        rel="stylesheet"
        href="https://kit-pro.fontawesome.com/releases/v5.15.1/css/pro.min.css"
      />

      <div className="min-h-screen flex flex-col items-center justify-center bg-gray-100 dark:bg-den">
        <div className="flex flex-col bg-white shadow-md px-4 sm:px-6 md:px-8 lg:px-10 py-8 rounded-md w-full max-w-md">
          <div className="font-medium self-center text-xl sm:text-2xl text-gray-800">
            Quên mật khẩu?
          </div>
          <div className="mt-10">
            <form onSubmit={handleSubmit(onSubmit)}>
              <div className="flex flex-col mb-6">
                <label
                  htmlFor="email"
                  className="mb-1 text-xs sm:text-sm tracking-wide text-gray-600"
                >
                  Địa chỉ E-Mail:
                </label>
                <div className="relative">
                  <div className="inline-flex items-center justify-center absolute left-0 top-0 h-full w-10 text-gray-400">
                    <svg
                      className="h-6 w-6"
                      fill="none"
                      strokeLinecap="round"
                      strokeLinejoin="round"
                      strokeWidth="2"
                      viewBox="0 0 24 24"
                      stroke="currentColor"
                    >
                      <path d="M16 12a4 4 0 10-8 0 4 4 0 008 0zm0 0v1.5a2.5 2.5 0 005 0V12a9 9 0 10-9 9m4.5-1.206a8.959 8.959 0 01-4.5 1.207" />
                    </svg>
                  </div>

                  <input
                    id="email"
                    type="email"
                    name="email"
                    className="text-sm sm:text-base placeholder-gray-500 pl-10 pr-4 rounded-lg border border-gray-400 w-full py-2 focus:outline-none focus:border-blue-400"
                    placeholder="Địa chỉ E-Mail"
                    aria-invalid={errors.email ? "true" : "false"}
                    {...register("email", { required: true })}
                  />
                </div>
                {errors.email?.type === "required" && (
                  <p role="alert" className="text-red-500">
                    Hãy nhập email
                  </p>
                )}
              </div>

              <div className="flex w-full">
                <button
                  type="submit"
                  className={`${
                    loading ? "button__loader" : ""
                  } flex items-center justify-center focus:outline-none text-white text-sm sm:text-base bg-den hover:bg-indigo-700 rounded py-2 w-full transition duration-150 ease-in`}
                  disabled={loading}
                >
                  {loading ? (
                    <span class="button__text">Loading...</span>
                  ) : (
                    <>
                      <span className="mr-2 uppercase z-10">Gửi</span>
                      <span className="mb-1">
                        <Send />
                      </span>
                    </>
                  )}
                </button>
              </div>
            </form>
          </div>
          <div className="flex justify-center items-center mt-6">
            <button
              onClick={() => navigate("/login")}
              target="_blank"
              className="inline-flex items-center font-bold text-den hover:text-blue-700 text-xs text-center cursor-pointer"
              disabled={loading}
            >
              <span>
                <ThumbUpAltIcon />
              </span>
              <span className="ml-2">Tự dưng nhớ lại mật khẩu rồi?</span>
            </button>
          </div>
        </div>
      </div>
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
    </div>
  );
};

export default ForgotPassword;
