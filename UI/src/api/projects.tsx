import { getAxios, executor } from 'api';

import FailedResponse from 'types/FailedResponse';
import CreateProjectDTO from 'dtos/CreateProjectDTO';
import ProjectDetailsDTO from 'dtos/ProjectDetailsDTO';
import UpdateProjectDTO from 'dtos/UpdateProjectDTO';
import AddProjectMemberDTO from 'dtos/AddProjectMemberDTO';

const get_all = async (): Promise<ProjectDetailsDTO[] | FailedResponse> => {
  const action = async (): Promise<ProjectDetailsDTO[]> => {
    const result = await getAxios().request({
      method: 'GET',
      url: 'projects',
    });

    return result.data;
  };

  return executor(action, 'get_all_projects');
};

const get_by_id = async (
  id: string,
): Promise<ProjectDetailsDTO | FailedResponse> => {
  const action = async (): Promise<ProjectDetailsDTO> => {
    const result = await getAxios().request({
      method: 'GET',
      url: 'projects/' + id,
    });

    return result.data;
  };

  return executor(action, 'get_project_by_id');
};

const create = async (
  dto: CreateProjectDTO,
): Promise<ProjectDetailsDTO | FailedResponse> => {
  const action = async (): Promise<ProjectDetailsDTO> => {
    const result = await getAxios().request({
      method: 'POST',
      url: 'projects',
      data: dto,
    });

    return result.data;
  };

  return await executor(action, 'create_project');
};

const update = async (
  id: string,
  dto: UpdateProjectDTO,
): Promise<void | FailedResponse> => {
  const action = async (): Promise<void> => {
    await getAxios().request({
      method: 'PUT',
      url: 'projects/' + id,
      data: dto,
    });
  };

  return await executor(action, 'update_project');
};

const remove = async (id: string): Promise<void | FailedResponse> => {
  const action = async (): Promise<void> => {
    await getAxios().request({
      method: 'DELETE',
      url: 'projects/' + id,
    });
  };

  return await executor(action, 'delete_project');
};

const add_member = async (
  id: string,
  dto: AddProjectMemberDTO,
): Promise<void | FailedResponse> => {
  const action = async (): Promise<void> => {
    await getAxios().request({
      method: 'POST',
      url: 'projects/' + id,
      data: dto,
    });
  };

  return await executor(action, 'add_project_member');
};

const remove_member = async (
  id: number,
  member: string,
): Promise<void | FailedResponse> => {
  const action = async (): Promise<void> => {
    await getAxios().request({
      method: 'POST',
      url: 'projects/' + id,
      data: member,
    });
  };

  return await executor(action, 'remove_project_member');
};

export interface ProjectsApi {
  get_all: () => Promise<ProjectDetailsDTO[] | FailedResponse>;
  get_by_id: (id: string) => Promise<ProjectDetailsDTO | FailedResponse>;
  update: (id: string, dto: UpdateProjectDTO) => Promise<void | FailedResponse>;
  remove: (id: string) => Promise<void | FailedResponse>;
  remove_member: (id: number, member: string) => Promise<void | FailedResponse>;

  create: (
    dto: CreateProjectDTO,
  ) => Promise<ProjectDetailsDTO | FailedResponse>;

  add_member: (
    id: string,
    dto: AddProjectMemberDTO,
  ) => Promise<void | FailedResponse>;
}

const projects_api_set: ProjectsApi = {
  get_all,
  get_by_id,
  create,
  update,
  remove,
  add_member,
  remove_member,
};

export default projects_api_set;
