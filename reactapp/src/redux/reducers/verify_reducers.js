import { createReducer, createAction } from "@reduxjs/toolkit";

// Initial State
const initialState = {
  email: "",
  TOTP: "",
};

// Actions
export const verify = createAction("SET_EMAIL");
export const unverify = createAction("UN_EMAIL");

// Reducer
const verifyReducer = createReducer(initialState, (builder) => {
  builder
    .addCase(verify, (state, action) => {
      state.email = action.payload.email;
      state.TOTP = action.payload.TOTP;
    })
    .addCase(unverify, (state) => {
      state.email = "";
      state.TOTP = "";
    });
});

export default verifyReducer;
