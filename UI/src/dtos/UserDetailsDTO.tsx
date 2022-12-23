import ProjectDetailsDTO from 'dtos/ProjectDetailsDTO';

export default interface UserDetailsDTO {
  wallet: string;
  display_name: string;
  projects?: ProjectDetailsDTO[];
}

export const defaultUser: UserDetailsDTO = {
  wallet: '',
  display_name: '',
  projects: [],
};
