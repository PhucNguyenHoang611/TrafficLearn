import { createReducer, createAction } from "@reduxjs/toolkit";

// Initial State
const initialState = {
  email: "",
};

// Actions
export const verify = createAction("SET_EMAIL");
export const unverify = createAction("UN_EMAIL");

// Reducer
const verifyReducer = createReducer(initialState, (builder) => {
  builder
    .addCase(verify, (state, action) => {
      state.email = action.payload.email;
    })
    .addCase(unverify, (state) => {
      state.email = "";
    });
});

export default verifyReducer;
