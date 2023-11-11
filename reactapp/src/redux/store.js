import {
  persistStore,
  persistReducer,
  FLUSH,
  REHYDRATE,
  PAUSE,
  PERSIST,
  PURGE,
  REGISTER,
} from "redux-persist";
import storage from "redux-persist/lib/storage";
import storageSession from "redux-persist/lib/storage/session";
import { configureStore, combineReducers } from "@reduxjs/toolkit";
import authReducer from "./reducers/auth_reducers";
import verifyReducer from "./reducers/verify_reducers";
import notifyReducer from "./reducers/notify_reducers";
import currentReducer from "./reducers/current_reducers";

// Define your root reducer
const rootReducer = combineReducers({
  auth: authReducer,
  verify: verifyReducer,
  notify: notifyReducer,
  current: currentReducer,
});

// Define your persist config
const persistConfig = {
  key: "root",
  version: 1.1,
  storage: storageSession,
};

// Define your persisted reducer
const persistedReducer = persistReducer(persistConfig, rootReducer);

// Define your store
export const store = configureStore({
  reducer: persistedReducer,
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware({
      serializableCheck: {
        ignoredActionPaths: ["payload.headers"],
        ignoredActions: [FLUSH, REHYDRATE, PAUSE, PERSIST, PURGE, REGISTER],
      },
    }),
});

export const persistor = persistStore(store);

// persistor
//   .purge()
//   .then(() => {
//     console.log("Data reset successful");
//   })
//   .catch(() => {
//     console.log("Data reset failed");
//   });
