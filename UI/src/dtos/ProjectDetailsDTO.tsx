import ProjectMemberDTO from 'dtos/ProjectMemberDTO';

export default interface ProjectDetailsDTO {
  id: string;
  name: string;
  website: string;
  email_address: string;
  is_member: boolean;
  level?: number;
  members?: ProjectMemberDTO[];
}
