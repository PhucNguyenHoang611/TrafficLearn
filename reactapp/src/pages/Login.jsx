import React, { useState, useEffect } from "react";
import { Link, useNavigate } from "react-router-dom";
import { useForm } from "react-hook-form";
import { login, checkValid, sendVerifyEmail } from "../apis/api_function";
import { useDispatch } from "react-redux";
import { Visibility, VisibilityOff } from "@mui/icons-material";
import { toast, ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import LoadingOnPage from "@/utils/LoadingOnPage";
import GoogleLogin from "../components/login/GoogleLogin";

const Login = () => {
  const dispatch = useDispatch();
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
    if (!data.email || !data.password) {
      return;
    } else if (loading) {
      return;
    }

    setLoading(true);
    // try {
    //   const check = await checkValid(data.email);
    //   if (check.status === 200) {
    //     if (check.data === false) {
    //       dispatch({
    //         type: "NOTIFY",
    //         payload: {
    //           type: "error",
    //           message: "Email chưa được xác thực !",
    //         },
    //       });
    //       // email verify
    //       dispatch({ type: "SET_EMAIL", payload: data });
    //       const send = await sendVerifyEmail(data.email);
    //       if (send.status === 200) {
    //         navigate("/verify");
    //         setLoading(false);
    //         return;
    //       }

    //       setLoading(false);
    //       return;
    //     }
    //   }
    // } catch (error) {
    //   setLoading(false);
    //   console.log(error);
    // }

    try {
      const res = await login(data.email, data.password);
      if (res.status === 200) {
        dispatch({ type: "LOGIN", payload: res.data });
        localStorage.setItem("auth", JSON.stringify(res.data));
        // dispatch({
        //   type: "NOTIFY",
        //   payload: { type: "success", message: "Đăng nhập thành công !" },
        // });
        setLoading(false);
        navigate("/");
      }
    } catch (error) {
      setLoading(false);
      if (error.response.data.error === "Email doesn't exist !") {
        toast.error("Email không tồn tại !");
      }
      if (error.response.data.error === "Incorrect password !") {
        toast.error("Mật khẩu không đúng !");
      }
      if (error.response.data.error === "Please verify your email !") {
        toast.error("Email chưa được xác thực !");

        dispatch({
          type: "NOTIFY",
          payload: {
            type: "error",
            message: "Email chưa được xác thực !",
          },
        });
        // email verify
        dispatch({ type: "SET_EMAIL", payload: data });
        const send = await sendVerifyEmail(data.email);
        if (send.status === 200) {
          navigate("/verify");
          setLoading(false);
          return;
        }

        setLoading(false);
        return;
      }
    }
  };

  // useEffect(() => {
  //   document.title = "Đăng nhập";
  // }, []);

  return (
    <div>
      <link
        rel="stylesheet"
        href="https://kit-pro.fontawesome.com/releases/v5.15.1/css/pro.min.css"
      />

      <div className="min-h-screen flex flex-col items-center justify-center bg-gray-100 dark:bg-den">
        <div className="flex flex-col bg-white shadow-md px-4 sm:px-6 md:px-8 lg:px-10 py-8 rounded-md w-full max-w-md">
          <div className="font-medium self-center text-xl sm:text-2xl text-gray-800">
            Chào mừng bạn trở lại!
          </div>
          <GoogleLogin />
          <div className="relative mt-10 h-px bg-gray-300">
            <div className="absolute left-0 top-0 flex justify-center w-full -mt-2">
              <span className="bg-white px-4 text-xs text-gray-500 uppercase">
                Hoặc đăng nhập với Email
              </span>
            </div>
          </div>
          <div className="mt-10">
            <form onSubmit={handleSubmit(onSubmit)}>
              <div className="flex flex-col mb-2">
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

              <div className="flex items-center mb-6">
                <div className="flex ml-auto">
                  <label
                    onClick={() => navigate("/forgotpassword")}
                    className="inline-flex text-xs sm:text-sm text-den hover:text-blue-700 cursor-pointer"
                  >
                    Quên mật khẩu?
                  </label>
                </div>
              </div>

              <div className="flex w-full">
                <button
                  type="submit"
                  className="flex items-center justify-center focus:outline-none text-white text-sm sm:text-base bg-den hover:bg-indigo-700 rounded py-2 w-full transition duration-150 ease-in"
                >
                  <span className="mr-2 uppercase">Đăng nhập</span>
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
                      <path d="M13 9l3 3m0 0l-3 3m3-3H8m13 0a9 9 0 11-18 0 9 9 0 0118 0z" />
                    </svg>
                  </span>
                </button>
              </div>
            </form>
          </div>
          <div className="flex justify-center items-center mt-6">
            <button
              onClick={() => navigate("/signup")}
              target="_blank"
              className="inline-flex items-center font-bold text-den hover:text-blue-700 text-xs text-center cursor-pointer"
            >
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
                  <path d="M18 9v3m0 0v3m0-3h3m-3 0h-3m-2-5a4 4 0 11-8 0 4 4 0 018 0zM3 20a6 6 0 0112 0v1H3v-1z" />
                </svg>
              </span>
              <span className="ml-2">Bạn chưa có tài khoản?</span>
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
      <LoadingOnPage open={loading} setOpen={setLoading} />
    </div>
  );
};

export default Login;
