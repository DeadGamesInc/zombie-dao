import { getAxios, executor } from 'api';

import AppSetupDTO from 'dtos/AppSetupDTO';
import FailedResponse from 'types/FailedResponse';

const app_setup = async (): Promise<AppSetupDTO | FailedResponse> => {
  const action = async (): Promise<AppSetupDTO> => {
    const result = await getAxios().request({
      method: 'GET',
      url: 'app_setup',
    });

    return result.data;
  };

  return await executor(action, 'app_setup');
};

export interface GeneralApi {
  app_setup: () => Promise<AppSetupDTO | FailedResponse>;
}

const general_api_set: GeneralApi = {
  app_setup,
};

export default general_api_set;
