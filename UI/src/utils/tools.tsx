export interface ToolSet {
  project_member_level_string: (level: number) => string;
}

const project_member_level_string = (level: number): string => {
  if (level === 255) return 'ADMIN';
  return 'MEMBER';
};

const tool_set: ToolSet = {
  project_member_level_string,
};

export default tool_set;
