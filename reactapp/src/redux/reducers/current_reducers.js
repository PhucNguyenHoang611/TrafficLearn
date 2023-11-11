import { createReducer, createAction } from "@reduxjs/toolkit";

// Initial State
const initialState = {
  current: "",
};

// Actions
export const current = createAction("CURRENT");
export const uncurrent = createAction("UN_CURRENT");

// Reducer
const currentReducer = createReducer(initialState, (builder) => {
  builder
    .addCase(current, (state, action) => {
      state.current = action.payload.current;
    })
    .addCase(uncurrent, (state) => {
      state.current = "";
    });
});

export default currentReducer;
