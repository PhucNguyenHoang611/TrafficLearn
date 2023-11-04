import axios from "axios";

export const baseURL = "https://localhost:7220/api";

let instance = null;

export const getMainApi = () => {
  if (!instance) {
    instance = axios.create({
      baseURL: baseURL,
    });
  }
  return instance;
};

// You can use `getMainApi()` to get the Axios instance
