import { getAxios, executor } from 'api';

import FailedResponse from 'types/FailedResponse';
import CreateGnosisSafeDTO from 'dtos/CreateGnosisSafeDTO';
import GnosisSafeDetailsDTO from 'dtos/GnosisSafeDetailsDTO';

const get_by_id = async (
  id: string,
): Promise<GnosisSafeDetailsDTO | FailedResponse> => {
  const action = async (): Promise<GnosisSafeDetailsDTO> => {
    const result = await getAxios().request({
      method: 'GET',
      url: 'gnosis/' + id,
    });

    return result.data;
  };

  return executor(action, 'get_gnosis_safe_by_id');
};

const create = async (
  project_id: string,
  dto: CreateGnosisSafeDTO,
): Promise<GnosisSafeDetailsDTO | FailedResponse> => {
  const action = async (): Promise<GnosisSafeDetailsDTO> => {
    const result = await getAxios().request({
      method: 'POST',
      url: 'gnosis/' + project_id + '/create',
      data: dto,
    });

    return result.data;
  };

  return await executor(action, 'create_gnosis_safe');
};

export interface GnosisApi {
  get_by_id: (id: string) => Promise<GnosisSafeDetailsDTO | FailedResponse>;

  create: (
    project_id: string,
    dto: CreateGnosisSafeDTO,
  ) => Promise<GnosisSafeDetailsDTO | FailedResponse>;
}

const gnosis_api_set: GnosisApi = {
  get_by_id,
  create,
};

export default gnosis_api_set;
