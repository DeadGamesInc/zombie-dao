import axios from 'axios';
import axiosRetry from 'axios-retry';

import user_api_set, { UserApi } from 'api/users';
import general_api_set, { GeneralApi } from 'api/general';
import projects_api_set, { ProjectsApi } from 'api/projects';
import gnosis_api_set, { GnosisApi } from 'api/gnosis';

import FailedResponse from 'types/FailedResponse';

export interface Apis {
  general: GeneralApi;
  users: UserApi;
  projects: ProjectsApi;
  gnosis: GnosisApi;
}

const apis: Apis = {
  general: general_api_set,
  users: user_api_set,
  projects: projects_api_set,
  gnosis: gnosis_api_set,
};

export const getAxios = () => {
  const instance = axios.create({
    baseURL: '/api/',
    withCredentials: true,
    headers: {
      get: {
        'Access-Control-Allow-Origin': '*',
        'Content-Type': 'application/json',
      },
      patch: {
        'Access-Control-Allow-Origin': '*',
        'Content-Type': 'application/json',
      },
      post: {
        'Access-Control-Allow-Origin': '*',
        'Content-Type': 'application/json',
      },
      put: {
        'Access-Control-Allow-Origin': '*',
        'Content-Type': 'application/json',
      },
      delete: {
        'Access-Control-Allow-Origin': '*',
        'Content-Type': 'application/json',
      },
    },
  });

  axiosRetry(instance, {
    retries: 3,
    retryDelay: axiosRetry.exponentialDelay,
    shouldResetTimeout: true,
  });

  return instance;
};

export async function executor<T>(
  action: any,
  name: string,
): Promise<T | FailedResponse> {
  try {
    return await action();
  } catch (error) {
    console.log('Error on API action', name, error);
    return FailedResponse;
  }
}

export default apis;
