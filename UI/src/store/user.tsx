import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { defaultUserState } from 'types/UserState';
import UserDetailsDTO, { defaultUser } from 'dtos/UserDetailsDTO';

export const userSlice = createSlice({
  name: 'user',
  initialState: defaultUserState,
  reducers: {
    set_logged_in: (state, action: PayloadAction<UserDetailsDTO>) => {
      state.logged_in = true;
      state.details = action.payload;
    },
    set_web3: (
      state,
      action: PayloadAction<{ wallet: string; chain_id: number }>,
    ) => {
      state.wallet = action.payload.wallet;
      state.chain_id = action.payload.chain_id;
    },
    set_logged_out: (state) => {
      state.logged_in = false;
      state.details = defaultUser;
    },
  },
});

export const { set_logged_in, set_web3, set_logged_out } = userSlice.actions;
export default userSlice.reducer;
