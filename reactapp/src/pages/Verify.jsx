import React, { useState } from "react";
import { Box, Button, LinearProgress, Modal, Typography } from "@mui/material";
import { useDispatch, useSelector } from "react-redux";
import { MuiOtpInput } from "mui-one-time-password-input";
import { useNavigate } from "react-router-dom";
import { styleVerify } from "../themes/Styles";
import { matchIsNumeric } from "../utils/function";
import { verifyEmail, sendVerifyEmail } from "../apis/api_function";

const Verify = () => {
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const email = useSelector((state) => state.verify.email);
  const auth = useSelector((state) => state.auth);
  const [otp, setOTP] = useState("");

  const validateChar = (value, index) => {
    return matchIsNumeric(value);
  };

  const handleChange = (newValue) => {
    console.log(newValue);
    setOTP(newValue);
  };

  const handleComplete = async (totp) => {
    const sotp = parseInt(totp);
    try {
      const result = await verifyEmail(email, sotp);
      if (result) {
        dispatch({
          type: "NOTIFY",
          payload: {
            type: "success",
            message: "Xác thực email thành công, hãy đăng nhập lại!",
          },
        });
        dispatch({ type: "UN_EMAIL" });
        navigate("/login");
      }
    } catch (error) {
      console.log(error);
    }
  };

  const handleResendOTP = async () => {
    console.log("resend", email);
    try {
      const result = await sendVerifyEmail(email);
      if (result) {
        dispatch({
          type: "NOTIFY",
          payload: { type: "success", message: "Gửi lại mã OTP thành công" },
        });
      }
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <div>
      <div class="relative flex min-h-screen flex-col items-center justify-center overflow-hidden py-6 sm:py-12 bg-white">
        <div class="max-w-xl px-5 text-center">
          <img
            src="https://cdn4.iconfinder.com/data/icons/social-media-logos-6/512/112-gmail_email_mail-512.png"
            alt="mail"
            className="w-24 h-24 mx-auto"
          />
          <h2 class="mb-2 text-[42px] font-bold text-zinc-800">
            Kiểm tra mail của bạn
          </h2>
          <p class="mb-2 text-lg text-zinc-500">
            Chúng tôi cũng đã gửi mã xác minh đến địa chỉ email của bạn.{" "}
            <span class="font-medium text-indigo-500">{email}</span>.
          </p>
          <p class="mb-2 text-lg text-zinc-500">
            Vui lòng nhập mã vào ô bên dưới.
          </p>
          <Box sx={{ mb: 2, mt: 4 }}>
            {/* <Typography
              id="modal-modal-title"
              variant="h6"
              component="h2"
              className="text-center"
              sx={{ mb: 2 }}
            >
              Nhập OTP được gửi đến email của bạn ô bên dưới
            </Typography> */}
            <MuiOtpInput
              value={otp}
              onChange={handleChange}
              onComplete={handleComplete}
              length={6}
              validateChar={validateChar}
            />
            <Typography id="modal-modal-description" sx={{ mt: 2 }}>
              Không nhận được OTP?
              <Button
                sx={{ fontSize: "1rem", fontWeight: "bold" }}
                onClick={handleResendOTP}
              >
                Gửi lại
              </Button>
            </Typography>
          </Box>
          {/* <button
            onClick={() => navigate("/")}
            class="mt-3 inline-block w-96 rounded bg-den px-5 py-3 font-medium text-white shadow-md shadow-indigo-500/20 hover:bg-indigo-700"
          >
            Xác nhận OTP →
          </button> */}
        </div>
      </div>
    </div>
  );
};

export default Verify;
