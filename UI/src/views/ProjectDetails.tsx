import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';

import apis from 'api';
import tool_set from 'utils/tools';
import ProjectDetailsDTO from 'dtos/ProjectDetailsDTO';
import FailedResponse from 'types/FailedResponse';

import PageContainer from 'components/PageContainer';
import HeaderText from 'components/HeaderText';

export type ProjectDetailsParams = {
  id: string;
};

const ProjectDetails: React.FC = () => {
  const [project, setProject] = useState<ProjectDetailsDTO>();

  const { id: id } = useParams<ProjectDetailsParams>();

  useEffect(() => {
    if (id === undefined) return;
    apis.projects.get_by_id(id).then((result) => {
      if (result === FailedResponse) return;
      setProject(result as ProjectDetailsDTO);
    });
  }, []);

  return (
    <PageContainer>
      <HeaderText text="Project Details" />
      Name: {project?.name}
      <br />
      Website: {project?.website}
      <br />
      Contact: {project?.email_address}
      <br />
      Members
      <br />
      <table>
        <thead>
          <tr>
            <td>Name</td>
            <td>Level</td>
          </tr>
        </thead>
        <tbody>
          {project?.members &&
            project.members.map((member) => (
              <tr key={member.display_name}>
                <td>{member.display_name}</td>
                <td>{tool_set.project_member_level_string(member.level)}</td>
              </tr>
            ))}
        </tbody>
      </table>
    </PageContainer>
  );
};

export default ProjectDetails;
