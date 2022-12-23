import { configureStore } from '@reduxjs/toolkit';
import userReducer from 'store/user';
import appReducer from 'store/app';

export const store = configureStore({
  reducer: {
    app: appReducer,
    user: userReducer,
  },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
