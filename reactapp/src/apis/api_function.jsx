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
};

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
};

// Traffic sign
export const getAllTrafficSigns = async () => {
  return await mainApi.get(
    apiEndpoints.GET_ALL_TRAFFIC_SIGNS
  );
};

export const getTrafficSign = async (id) => {
  return await mainApi.get(
    apiEndpoints.GET_TRAFFIC_SIGN(id)
  );
};

// Traffic sign type
export const getAllTrafficSignTypes = async () => {
  return await mainApi.get(
    apiEndpoints.GET_ALL_TRAFFIC_SIGN_TYPES
  );
};

export const getTrafficSignType = async (id) => {
  return await mainApi.get(
    apiEndpoints.GET_TRAFFIC_SIGN_TYPE(id)
  );
};

// News
export const getAllNews = async () => {
  return await mainApi.get(
    apiEndpoints.GET_ALL_NEWS
  );
};

export const getNews = async (id) => {
  return await mainApi.get(
    apiEndpoints.GET_NEWS(id)
  );
};

// License
export const getAllLicenses = async () => {
  return await mainApi.get(
    apiEndpoints.GET_ALL_LICENSES
  );
};

export const getLicense = async (id) => {
  return await mainApi.get(
    apiEndpoints.GET_LICENSE(id)
  );
};

// Title
export const getAllTitles = async () => {
  return await mainApi.get(
    apiEndpoints.GET_ALL_TITLES
  );
};

export const getTitle = async (id) => {
  return await mainApi.get(
    apiEndpoints.GET_TITLE(id)
  );
};

// License title
export const getAllLicenseTitles = async () => {
  return await mainApi.get(
    apiEndpoints.GET_ALL_LICENSE_TITLES
  );
};

export const getLicenseTitle = async (id) => {
  return await mainApi.get(
    apiEndpoints.GET_LICENSE_TITLE(id)
  );
};

export const getLicenseTitlesByLicenseId = async (id) => {
  return await mainApi.get(
    apiEndpoints.GET_LICENSE_TITLES_BY_LICENSE_ID(id)
  );
};

// Question
export const getAllQuestionsByLicenseId = async (id) => {
  return await mainApi.get(
    apiEndpoints.GET_ALL_QUESTIONS_BY_LICENSE_ID(id)
  );
};

export const getAllImportantQuestionsByLicenseId = async (id) => {
  return await mainApi.get(
    apiEndpoints.GET_ALL_IMPORTANT_QUESTIONS_BY_LICENSE_ID(id)
  );
};

export const getQuestionsByLicenseTitleId = async (id) => {
  return await mainApi.get(
    apiEndpoints.GET_QUESTIONS_BY_LICENSE_TITLE_ID(id)
  );
};

// Examination
export const getAllExaminations = async () => {
  return await mainApi.get(
    apiEndpoints.GET_ALL_EXAMINATIONS
  );
};

export const getExamination = async (id) => {
  return await mainApi.get(
    apiEndpoints.GET_EXAMINATION(id)
  );
};

export const getAllExaminationQuestions = async (id) => {
  return await mainApi.get(
    apiEndpoints.GET_ALL_EXAMINATION_QUESTIONS(id)
  );
};

// Examination result
export const getAllExaminationResults = async (userId, token) => {
  return await mainApi.get(
    apiEndpoints.GET_ALL_EXAMINATION_RESULTS(userId),
    apiEndpoints.getAccessToken(token)
  );
};

export const createExaminationResults = async (UserId, ExaminationId, ExaminationDate, Score, IsPassed, token) => {
  return await mainApi.post(
    apiEndpoints.CREATE_EXAMINATION_RESULT,
    apiEndpoints.getExaminationResultBody(UserId, ExaminationId, ExaminationDate, Score, IsPassed),
    apiEndpoints.getAccessToken(token)
  );
};

// Answer
export const validateAnswer = async (questionId, answerId, token) => {
  return await mainApi.get(
    apiEndpoints.VALIDATE_ANSWER(questionId, answerId),
    apiEndpoints.getAccessToken(token)
  );
};