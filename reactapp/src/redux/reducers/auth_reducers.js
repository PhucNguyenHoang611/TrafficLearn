import { createReducer, createAction } from "@reduxjs/toolkit";

// Initial State
const initialState = {
  token: "",
  id: "",
  firstName: "",
  lastName: "",
  email: "",
  birthday: "",
  gender: "",
  phoneNumber: "",
  provider: "",
  isAdmin: "",
};

// Actions
export const login = createAction("LOGIN");
export const logout = createAction("LOGOUT");

// Reducer
const authReducer = createReducer(initialState, (builder) => {
  builder
    .addCase(login, (state, action) => {
      state.token = action.payload.token;
      state.id = action.payload.id;
      state.firstName = action.payload.firstName;
      state.lastName = action.payload.lastName;
      state.email = action.payload.email;
      state.birthday = action.payload.birthday;
      state.gender = action.payload.gender;
      state.phoneNumber = action.payload.phoneNumber;
      state.provider = action.payload.provider;
      state.isAdmin = action.payload.isAdmin;
    })
    .addCase(logout, (state) => {
      state.token = "";
      state.id = "";
      state.firstName = "";
      state.lastName = "";
      state.email = "";
      state.birthday = "";
      state.gender = "";
      state.phoneNumber = "";
      state.provider = "";
      state.isAdmin = "";
    });
});

export default authReducer;
