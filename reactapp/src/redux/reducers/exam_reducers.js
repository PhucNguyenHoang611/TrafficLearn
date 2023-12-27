import { createReducer, createAction } from "@reduxjs/toolkit";

// Initial State
const initialState = {
  examId: "",
  examData: null,
  questionsData: []
};

// Actions
export const storeExamData = createAction("STORE_EXAM_DATA");
export const storeQuestionsData = createAction("STORE_QUESTIONS_DATA");


// Reducer
const examReducer = createReducer(initialState, (builder) => {
  builder
    .addCase(storeExamData, (state, action) => {
      state.examId = action.payload.examId;
      state.examData = action.payload.examData;
    })
    .addCase(storeQuestionsData, (state, action) => {
      state.questionsData = action.payload;
    });
});

export default examReducer;
