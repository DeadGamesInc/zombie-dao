import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { defaultAppState } from 'types/AppState';
import AppSetupDTO from 'dtos/AppSetupDTO';

export const appSlice = createSlice({
  name: 'app',
  initialState: defaultAppState,
  reducers: {
    app_setup: (state, action: PayloadAction<AppSetupDTO>) => {
      state.supported_blockchains = action.payload.supported_blockchains;
    },
  },
});

export const { app_setup } = appSlice.actions;
export default appSlice.reducer;
