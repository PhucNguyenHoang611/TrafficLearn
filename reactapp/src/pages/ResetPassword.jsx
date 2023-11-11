import React, { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { useForm } from "react-hook-form";
// import { login } from "../apis/api_function";
import { useDispatch, useSelector } from "react-redux";
import { Visibility, VisibilityOff, Send } from "@mui/icons-material";
import ThumbUpAltIcon from "@mui/icons-material/ThumbUpAlt";
import { toast, ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { resetPassword } from "../apis/api_function";
import "../themes/buttonstyles/submitButton.css";

const ResetPassword = () => {
  const dispatch = useDispatch();
  const verify = useSelector((state) => state.verify);
  const navigate = useNavigate();
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm();
  const [showPassword, setShowPassword] = useState(false);
  const [isNotify, setIsNotify] = useState(false);
  const [loading, setLoading] = useState(false);

  const handleShowPassword = () => {
    setShowPassword((prev) => !prev);
  };

  const onSubmit = async (data) => {
    // if (loading) {
    //   return;
    // }
    // setLoading(true);
    // if (!data.password || !data.rwpassword) {
    //   return;
    // }
    if (data.password !== data.rwpassword) {
      toast.error("Mật khẩu không khớp !");
      return;
    }

    try {
      const res = await resetPassword(verify.email, data.password, verify.TOTP);
      if (res.status === 200) {
        toast.success("Đổi mật khẩu thành công !");
        navigate("/login");
      }
    } catch (error) {
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
            Nhập mật khẩu mới
          </div>
          <div className="mt-10">
            <form onSubmit={handleSubmit(onSubmit)}>
              <div className="flex flex-col mt-4 mb-2">
                <label
                  htmlFor="password"
                  className="mb-1 text-xs sm:text-sm tracking-wide text-gray-600"
                >
                  Mật khẩu:
                </label>
                <div className="relative">
                  <div className="inline-flex items-center justify-center absolute left-0 top-0 h-full w-10 text-gray-400">
                    <span>
                      <svg
                        className="h-6 w-6"
                        fill="none"
                        strokeLinecap="round"
                        strokeLinejoin="round"
                        strokeWidth="2"
                        viewBox="0 0 24 24"
                        stroke="currentColor"
                      >
                        <path d="M12 15v2m-6 4h12a2 2 0 002-2v-6a2 2 0 00-2-2H6a2 2 0 00-2 2v6a2 2 0 002 2zm10-10V7a4 4 0 00-8 0v4h8z" />
                      </svg>
                    </span>
                  </div>

                  <input
                    id="password"
                    type={showPassword ? "text" : "password"}
                    name="password"
                    className="text-sm sm:text-base placeholder-gray-500 pl-10 pr-4 rounded-lg border border-gray-400 w-full py-2 focus:outline-none focus:border-blue-400"
                    placeholder="Mật khẩu"
                    {...register("password", { required: true })}
                  />

                  <div className="inline-flex items-center justify-center absolute right-0 top-0 h-full w-10 text-gray-400">
                    <span>
                      {showPassword ? (
                        <Visibility
                          onClick={handleShowPassword}
                          className="h-6 w-6"
                        />
                      ) : (
                        <VisibilityOff
                          onClick={handleShowPassword}
                          className="h-6 w-6"
                        />
                      )}
                    </span>
                  </div>
                </div>
                {errors.password?.type === "required" && (
                  <p role="alert" className="text-red-500">
                    Hãy nhập mật khẩu
                  </p>
                )}
              </div>
              <div className="flex flex-col mt-4 mb-2">
                <label
                  htmlFor="password"
                  className="mb-1 text-xs sm:text-sm tracking-wide text-gray-600"
                >
                  Nhập lại mật khẩu:
                </label>
                <div className="relative">
                  <div className="inline-flex items-center justify-center absolute left-0 top-0 h-full w-10 text-gray-400">
                    <span>
                      <svg
                        className="h-6 w-6"
                        fill="none"
                        strokeLinecap="round"
                        strokeLinejoin="round"
                        strokeWidth="2"
                        viewBox="0 0 24 24"
                        stroke="currentColor"
                      >
                        <path d="M12 15v2m-6 4h12a2 2 0 002-2v-6a2 2 0 00-2-2H6a2 2 0 00-2 2v6a2 2 0 002 2zm10-10V7a4 4 0 00-8 0v4h8z" />
                      </svg>
                    </span>
                  </div>

                  <input
                    id="rwpassword"
                    type={showPassword ? "text" : "password"}
                    name="rwpassword"
                    className="text-sm sm:text-base placeholder-gray-500 pl-10 pr-4 rounded-lg border border-gray-400 w-full py-2 focus:outline-none focus:border-blue-400"
                    placeholder="Mật khẩu"
                    {...register("rwpassword", { required: true })}
                  />

                  <div className="inline-flex items-center justify-center absolute right-0 top-0 h-full w-10 text-gray-400">
                    <span>
                      {showPassword ? (
                        <Visibility
                          onClick={handleShowPassword}
                          className="h-6 w-6"
                        />
                      ) : (
                        <VisibilityOff
                          onClick={handleShowPassword}
                          className="h-6 w-6"
                        />
                      )}
                    </span>
                  </div>
                </div>
                {errors.rwpassword?.type === "required" && (
                  <p role="alert" className="text-red-500">
                    Hãy nhập lại mật khẩu
                  </p>
                )}
              </div>
              <div className="flex w-full mt-8">
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
              <span className="ml-2">Về trang đăng nhập?</span>
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

export default ResetPassword;
