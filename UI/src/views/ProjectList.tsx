import React, { useState, useEffect } from 'react';

import apis from 'api';

import ProjectDetailsDTO from 'dtos/ProjectDetailsDTO';
import FailedResponse from 'types/FailedResponse';

import PageContainer from 'components/PageContainer';
import HeaderText from 'components/HeaderText';
import TextLink from 'components/TextLink';

const ProjectList: React.FC = () => {
  const [projects, setProjects] = useState<ProjectDetailsDTO[]>();

  useEffect(() => {
    apis.projects.get_all().then((result) => {
      if (result === FailedResponse) return;
      setProjects(result as ProjectDetailsDTO[]);
    });
  }, []);

  return (
    <PageContainer>
      <HeaderText text="Project List" />
      <table>
        <thead>
          <tr>
            <td>Project</td>
            <td>Website</td>
          </tr>
        </thead>
        <tbody>
          {projects &&
            projects.map((project) => (
              <tr key={project.id}>
                <td>
                  <TextLink
                    text={project.name}
                    target={`/projects/${project.id}`}
                  />
                </td>
                <td>{project.website}</td>
              </tr>
            ))}
        </tbody>
      </table>
    </PageContainer>
  );
};

export default ProjectList;
