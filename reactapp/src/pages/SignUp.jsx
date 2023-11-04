import React, { useState, useEffect } from "react";
import { Link, useNavigate } from "react-router-dom";
import { useForm } from "react-hook-form";
import { registerUser } from "../apis/api_function";
import { useDispatch } from "react-redux";
import { Visibility, VisibilityOff } from "@mui/icons-material";
import VpnKeyIcon from "@mui/icons-material/VpnKey";
import SubscriptionsIcon from "@mui/icons-material/Subscriptions";
import { toast, ToastContainer } from "react-toastify";
import CustomDatetime from "@/utils/CustomDatetime";
import "react-toastify/dist/ReactToastify.css";

const SignUp = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm();
  const [showPassword, setShowPassword] = useState(false);
  const [isNotify, setIsNotify] = useState(false);
  const [date, setDate] = useState(new Date());

  function onDateChange(date) {
    setDate(date);
  }

  const handleShowPassword = () => {
    setShowPassword((prev) => !prev);
  };

  const onSubmit = async (data) => {
    try {
      if (!data.email || !data.password) {
        return;
      }
      const birthday = date.toISOString();
      const res = await registerUser(
        data.email,
        data.password,
        data.firstname,
        data.lastname,
        birthday,
        data.gender,
        data.phone
      );
      if (res.status === 200) {
        // toast.success("Đăng ký thành công !");
        navigate("/login");
      }
    } catch (error) {
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
            Đăng ký tài khoản mới!
          </div>
          <div className="mt-10">
            <form onSubmit={handleSubmit(onSubmit)}>
              {/* mail */}
              <div className="flex flex-col mb-6">
                <label
                  htmlFor="email"
                  className="mb-1 text-xs sm:text-sm tracking-wide text-gray-600"
                >
                  Địa chỉ E-Mail:
                </label>
                <div className="relative">
                  <input
                    id="email"
                    type="email"
                    name="email"
                    className="text-sm sm:text-base placeholder-gray-500 pl-4 pr-4 rounded-lg border border-gray-400 w-full py-2 focus:outline-none focus:border-blue-400"
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
              {/* last name */}
              <div className="flex justify-between gap-12">
                <div className="flex flex-col mb-6">
                  <label
                    htmlFor="lastname"
                    className="mb-1 text-xs sm:text-sm tracking-wide text-gray-600"
                  >
                    Họ:
                  </label>
                  <div className="relative">
                    <input
                      id="lastname"
                      type="text"
                      name="lastname"
                      className="text-sm sm:text-base placeholder-gray-500 pl-4 pr-4 rounded-lg border border-gray-400 w-full py-2 focus:outline-none focus:border-blue-400"
                      placeholder="Họ"
                      aria-invalid={errors.lastname ? "true" : "false"}
                      {...register("lastname", { required: true })}
                    />
                  </div>
                  {errors.lastname?.type === "required" && (
                    <p role="alert" className="text-red-500">
                      Hãy nhập họ
                    </p>
                  )}
                </div>
                {/* first name */}
                <div className="flex flex-col mb-6">
                  <label
                    htmlFor="firstname"
                    className="mb-1 text-xs sm:text-sm tracking-wide text-gray-600"
                  >
                    Tên:
                  </label>
                  <div className="relative">
                    <input
                      id="firstname"
                      type="text"
                      name="firstname"
                      className="text-sm sm:text-base placeholder-gray-500 pl-4 pr-4 rounded-lg border border-gray-400 w-full py-2 focus:outline-none focus:border-blue-400"
                      placeholder="Tên"
                      aria-invalid={errors.firstname ? "true" : "false"}
                      {...register("firstname", { required: true })}
                    />
                  </div>
                  {errors.firstname?.type === "required" && (
                    <p role="alert" className="text-red-500">
                      Hãy nhập tên
                    </p>
                  )}
                </div>
              </div>
              <div className="flex justify-between gap-4">
                {/* birthday */}
                <div className="flex flex-col mb-6">
                  <label
                    htmlFor="lastname"
                    className="mb-1 text-xs sm:text-sm tracking-wide text-gray-600"
                  >
                    Ngày sinh:
                  </label>
                  <div className="relative">
                    <CustomDatetime date={date} onDateChange={onDateChange} />
                  </div>
                </div>
                {/* gender */}
                <div className="flex flex-col mb-6">
                  <label
                    htmlFor="gender"
                    className="mb-1 text-xs sm:text-sm tracking-wide text-gray-600"
                  >
                    Giới tính:
                  </label>
                  <div className="relative">
                    <select
                      id="gender"
                      name="gender"
                      className="text-sm sm:text-base placeholder-gray-500 pl-4 pr-4 rounded-lg border border-gray-400 min-w-fit w-full px-6 py-[10px] focus:outline-none focus:border-blue-400"
                      placeholder="Giới tính"
                      defaultValue="Male"
                      {...register("gender", { required: true })}
                    >
                      <option value="Male">Nam</option>
                      <option value="Female">Nữ</option>
                      <option value="Other">Khác</option>
                    </select>
                  </div>
                </div>
              </div>
              {/* phone*/}
              <div className="flex flex-col mb-6">
                <label
                  htmlFor="phone"
                  className="mb-1 text-xs sm:text-sm tracking-wide text-gray-600"
                >
                  Số điện thoại:
                </label>
                <div className="relative">
                  <input
                    id="phone"
                    type="number"
                    name="phone"
                    className="text-sm sm:text-base placeholder-gray-500 pl-4 pr-4 rounded-lg border border-gray-400 w-full py-2 focus:outline-none focus:border-blue-400"
                    placeholder="Số điện thoại"
                    aria-invalid={errors.phone ? "true" : "false"}
                    {...register("phone", { required: true })}
                  />
                </div>
                {errors.phone?.type === "required" && (
                  <p role="alert" className="text-red-500">
                    Hãy nhập số điện thoại
                  </p>
                )}
              </div>
              <div className="flex flex-col mb-6">
                <label
                  htmlFor="password"
                  className="mb-1 text-xs sm:text-sm tracking-wide text-gray-600"
                >
                  Mật khẩu:
                </label>
                <div className="relative">
                  <input
                    id="password"
                    type={showPassword ? "text" : "password"}
                    name="password"
                    className="text-sm sm:text-base placeholder-gray-500 pl-4 pr-4 rounded-lg border border-gray-400 w-full py-2 focus:outline-none focus:border-blue-400"
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
              {/* rewrite mật khẩu */}
              <div className="flex flex-col mb-2">
                <label
                  htmlFor="rwpassword"
                  className="mb-1 text-xs sm:text-sm tracking-wide text-gray-600"
                >
                  Nhập lại mật khẩu:
                </label>
                <div className="relative">
                  <input
                    id="rwpassword"
                    type={showPassword ? "text" : "password"}
                    name="rwpassword"
                    className="text-sm sm:text-base placeholder-gray-500 pl-4 pr-4 rounded-lg border border-gray-400 w-full py-2 focus:outline-none focus:border-blue-400"
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
                    Nhập lại mật khẩu
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
                  className="flex items-center justify-center focus:outline-none text-white text-sm sm:text-base bg-den hover:bg-blue-700 rounded py-2 w-full transition duration-150 ease-in"
                >
                  <span className="mr-2 uppercase">Đăng ký</span>
                  <span className="mb-1">
                    <SubscriptionsIcon />
                  </span>
                </button>
              </div>
            </form>
          </div>
          <div className="flex justify-center items-center mt-6">
            <button
              onClick={() => navigate("/login")}
              target="_blank"
              className="inline-flex items-center font-bold text-den hover:text-blue-700 text-xs text-center"
            >
              <span>
                <VpnKeyIcon />
              </span>
              <span className="ml-2">Đã có tài khoản? Quay lại đăng nhập</span>
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

export default SignUp;
