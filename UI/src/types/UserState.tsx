import UserDetailsDTO, { defaultUser } from 'dtos/UserDetailsDTO';

export const defaultUserState: UserState = {
  logged_in: false,
  wallet: '',
  chain_id: 0,
  details: defaultUser,
};

export default interface UserState {
  logged_in: boolean;
  wallet: string;
  chain_id: number;
  details: UserDetailsDTO;
}
