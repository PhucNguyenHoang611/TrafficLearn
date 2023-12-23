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

export const googleLoginCallback = () => {
  window.open(`${baseURL}/login/google/callback`, "_self");
};

export const googleLoginSuccess = async () => {
  return axios.get(`https://localhost:7220/api/login/google/success`, {
    withCredentials: true,
  });
  // return await mainApi.get(apiEndpoints.GOOGLE_LOGIN_SUCCESS, {
  //   withCredentials: true,
  // });
};

export const googleLogout = () => {
  window.open(`${baseURL}/auth/google/logout`, "_self");
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
  return mainApi.post(apiEndpoints.SEND_VERIFY_EMAIL(email));
};

// Check valid
export const checkValid = (email) => {
  // const params = new URLSearchParams([["email", email]]);
  // return mainApi.get(apiEndpoints.CHECK_VALID, { params });
  return mainApi.get(apiEndpoints.CHECK_VALID, {
    params: { email: email },
  });
};

// Forget password
export const forgetPassword = (email) => {
  return mainApi.post(apiEndpoints.FORGET_PASSWORD(email));
};

// Reset password
export const resetPassword = (email, password, TOTP) => {
  return mainApi.post(apiEndpoints.RESET_PASSWORD, {
    Email: email,
    TOTP: TOTP,
    NewPassword: password,
  });
};

// Traffic fine
export const getAllTrafficFines = async () => {
  return await mainApi.get(
    apiEndpoints.GET_ALL_TRAFFIC_FINES
  );
};

export const getTrafficFine = async (id) => {
  return await mainApi.get(
    apiEndpoints.GET_TRAFFIC_FINE(id)
  );
}

// Traffic fine type
export const getAllTrafficFineTypes = async () => {
  return await mainApi.get(
    apiEndpoints.GET_ALL_TRAFFIC_FINE_TYPES
  );
};

export const getTrafficFineType = async (id) => {
  return await mainApi.get(
    apiEndpoints.GET_TRAFFIC_FINE_TYPE(id)
  );
}