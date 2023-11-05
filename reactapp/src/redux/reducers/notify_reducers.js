import { createReducer, createAction } from "@reduxjs/toolkit";

// Initial State
const initialState = {
  type: "",
  message: "",
};

// Actions
export const notify = createAction("NOTIFY");
export const unnotify = createAction("UN_NOTIFY");

// Reducer
const notifyReducer = createReducer(initialState, (builder) => {
  builder
    .addCase(notify, (state, action) => {
      state.type = action.payload.type;
      state.message = action.payload.message;
    })
    .addCase(unnotify, (state) => {
      state.type = "";
      state.message = "";
    });
});

export default notifyReducer;
