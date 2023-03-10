import { executor, getAxios } from 'api';

import FailedResponse from 'types/FailedResponse';
import CreateGnosisSafeDTO from 'dtos/CreateGnosisSafeDTO';
import GnosisSafeDetailsDTO from 'dtos/GnosisSafeDetailsDTO';
import CreateGnosisSafeTokenDTO from 'dtos/CreateGnosisSafeTokenDTO';
import CreateGnosisSafeTransactionDTO from 'dtos/CreateGnosisSafeTransactionDTO';
import CreateGnosisSafeConfirmationDTO from '../dtos/CreateGnosisSafeConfirmationDTO';

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

const add_token = async (
  safe_id: string,
  dto: CreateGnosisSafeTokenDTO,
): Promise<void | FailedResponse> => {
  const action = async (): Promise<void> => {
    await getAxios().request({
      method: 'PUT',
      url: 'gnosis/' + safe_id + '/add_token',
      data: dto,
    });
  };

  return await executor(action, 'add_gnosis_safe_token');
};

const add_transaction = async (
  safe_id: string,
  dto: CreateGnosisSafeTransactionDTO,
): Promise<void | FailedResponse> => {
  const action = async (): Promise<void> => {
    await getAxios().request({
      method: 'PUT',
      url: 'gnosis/' + safe_id + '/add_transaction',
      data: dto,
    });
  };

  return await executor(action, 'add_gnosis_safe_transaction');
};

const delete_transaction = async (
  safe_id: string,
  id: string,
): Promise<void | FailedResponse> => {
  const action = async (): Promise<void> => {
    await getAxios().request({
      method: 'DELETE',
      url: 'gnosis/' + safe_id + '/delete_transaction/' + id,
    });
  };

  return await executor(action, 'delete_gnosis_safe_transaction');
};

const add_confirmation = async (
  safe_id: string,
  tx_id: string,
  dto: CreateGnosisSafeConfirmationDTO,
): Promise<void | FailedResponse> => {
  const action = async (): Promise<void> => {
    await getAxios().request({
      method: 'PUT',
      url: `gnosis/${safe_id}/transactions/${tx_id}/add_confirmation`,
      data: dto,
    });
  };

  return await executor(action, 'add_gnosis_transaction_confirmation');
};

const set_executed = async (
  safe_id: string,
  tx_id: string,
): Promise<void | FailedResponse> => {
  const action = async (): Promise<void> => {
    await getAxios().request({
      method: 'PUT',
      url: `gnosis/${safe_id}/transactions/${tx_id}/set_executed`,
    });
  };

  return await executor(action, 'set_gnosis_transaction_executed');
};

export interface GnosisApi {
  get_by_id: (id: string) => Promise<GnosisSafeDetailsDTO | FailedResponse>;

  create: (
    project_id: string,
    dto: CreateGnosisSafeDTO,
  ) => Promise<GnosisSafeDetailsDTO | FailedResponse>;

  add_token: (
    safe_id: string,
    dto: CreateGnosisSafeTokenDTO,
  ) => Promise<void | FailedResponse>;

  add_transaction: (
    safe_id: string,
    dto: CreateGnosisSafeTransactionDTO,
  ) => Promise<void | FailedResponse>;

  delete_transaction: (
    safe_id: string,
    id: string,
  ) => Promise<void | FailedResponse>;

  add_confirmation: (
    safe_id: string,
    tx_id: string,
    dto: CreateGnosisSafeConfirmationDTO,
  ) => Promise<void | FailedResponse>;

  set_executed: (
    safe_id: string,
    tx_id: string,
  ) => Promise<void | FailedResponse>;
}

const gnosis_api_set: GnosisApi = {
  get_by_id,
  create,
  add_token,
  add_transaction,
  delete_transaction,
  add_confirmation,
  set_executed,
};

export default gnosis_api_set;
