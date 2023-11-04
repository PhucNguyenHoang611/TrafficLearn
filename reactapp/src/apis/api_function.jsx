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

//register
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
