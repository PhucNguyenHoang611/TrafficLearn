import axios from "axios";
import { getMainApi } from "./main_api";
import * as apiEndpoints from "./api_endpoints";
import { baseURL } from "./main_api";

const mainApi = getMainApi();

// Login
export const login = (email, password) => {
  return mainApi.post(apiEndpoints.LOGIN, {
    Email: email,
    Password: password,
  });
};

// Register
export const registerUser = (
  email,
  password,
  firstName,
  lastName,
  birthday,
  gender,
  phoneNumber
) => {
  return mainApi.post(apiEndpoints.REGISTER, {
    Email: email,
    Password: password,
    FirstName: firstName,
    LastName: lastName,
    Birthday: birthday,
    Gender: gender,
    PhoneNumber: phoneNumber,
    Provider: "default",
    Role: "User",
  });
};

// Verify Email
export const verifyEmail = (email, TOTP) => {
  return mainApi.post(apiEndpoints.VERIFY_EMAIL, {
    Email: email,
    TOTP: TOTP,
  });
};

// Send verify email
export const sendVerifyEmail = (email) => {
  return mainApi.post(
    apiEndpoints.SEND_VERIFY_EMAIL,
    {},
    {
      params: { email: email },
    }
  );
};

// Check valid
export const checkValid = (email) => {
  // const params = new URLSearchParams([["email", email]]);
  // return mainApi.get(apiEndpoints.CHECK_VALID, { params });
  return mainApi.get(apiEndpoints.CHECK_VALID, {
    params: { email: email },
  });
};
