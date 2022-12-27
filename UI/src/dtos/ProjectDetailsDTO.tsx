import ProjectMemberDTO from 'dtos/ProjectMemberDTO';
import GnosisSafeDetailsDTO from 'dtos/GnosisSafeDetailsDTO';

export default interface ProjectDetailsDTO {
  id: string;
  name: string;
  website: string;
  email_address: string;
  is_member: boolean;
  member_level?: number;
  members?: ProjectMemberDTO[];
  gnosis_safes?: GnosisSafeDetailsDTO[];
}
