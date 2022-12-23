import { getAxios, executor } from 'api';

import FailedResponse from 'types/FailedResponse';
import CreateUserDTO from 'dtos/CreateUserDTO';
import UserDetailsDTO from 'dtos/UserDetailsDTO';
import CompleteLoginDTO from 'dtos/CompleteLoginDTO';

const start_registration = async (
  wallet: string,
): Promise<string | FailedResponse> => {
  const action = async (): Promise<string> => {
    const result = await getAxios().request({
      method: 'POST',
      url: 'users/start_registration',
      data: wallet,
    });

    return result.data;
  };

  return await executor(action, 'start_registration');
};

const complete_registration = async (
  dto: CreateUserDTO,
): Promise<UserDetailsDTO | FailedResponse> => {
  const action = async (): Promise<UserDetailsDTO> => {
    const result = await getAxios().request({
      method: 'POST',
      url: 'users/complete_registration',
      data: dto,
    });

    return result.data;
  };

  return await executor(action, 'complete_registration');
};

const start_login = async (
  wallet: string,
): Promise<string | FailedResponse> => {
  const action = async (): Promise<string> => {
    const result = await getAxios().request({
      method: 'POST',
      url: 'users/start_login',
      data: wallet,
    });

    return result.data;
  };

  return await executor(action, 'start_login');
};

const complete_login = async (
  dto: CompleteLoginDTO,
): Promise<UserDetailsDTO | FailedResponse> => {
  const action = async (): Promise<UserDetailsDTO> => {
    const result = await getAxios().request({
      method: 'POST',
      url: 'users/complete_login',
      data: dto,
    });

    return result.data;
  };

  return await executor(action, 'complete_login');
};

const whoami = async (): Promise<UserDetailsDTO | FailedResponse> => {
  const action = async (): Promise<UserDetailsDTO> => {
    const result = await getAxios().request({
      method: 'GET',
      url: 'users/whoami',
    });

    return result.data;
  };

  return await executor(action, 'whoami');
};

const logout = async (): Promise<void | FailedResponse> => {
  const action = async (): Promise<void> => {
    await getAxios().request({
      method: 'GET',
      url: 'users/logout',
    });
  };

  return await executor(action, 'logout');
};

export interface UserApi {
  start_registration: (wallet: string) => Promise<string | FailedResponse>;
  start_login: (wallet: string) => Promise<string | FailedResponse>;
  whoami: () => Promise<UserDetailsDTO | FailedResponse>;
  logout: () => Promise<void | FailedResponse>;

  complete_registration: (
    dto: CreateUserDTO,
  ) => Promise<UserDetailsDTO | FailedResponse>;

  complete_login: (
    dto: CompleteLoginDTO,
  ) => Promise<UserDetailsDTO | FailedResponse>;
}

const user_api_set: UserApi = {
  start_registration,
  complete_registration,
  start_login,
  complete_login,
  whoami,
  logout,
};
export default user_api_set;
