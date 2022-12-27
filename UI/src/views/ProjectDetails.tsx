import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';

import apis from 'api';
import tool_set from 'utils/tools';
import { useAppSelector } from 'store/hooks';

import ProjectDetailsDTO from 'dtos/ProjectDetailsDTO';
import FailedResponse from 'types/FailedResponse';
import CreateGnosisSafeDTO from 'dtos/CreateGnosisSafeDTO';

import PageContainer from 'components/PageContainer';
import HeaderText from 'components/HeaderText';
import StandardForm from 'components/StandardForm';
import FormInput from 'components/FormInput';
import FormButton from 'components/FormButton';
import TextLink from 'components/TextLink';

import NotLoggedIn from 'views/NotLoggedIn';
import Loading from 'views/Loading';

export type ProjectDetailsParams = {
  id: string;
};

const ProjectDetails: React.FC = () => {
  const [project, setProject] = useState<ProjectDetailsDTO>();
  const [refresh, setRefresh] = useState(false);

  const { id: id } = useParams<ProjectDetailsParams>();
  const admin = project !== undefined && project.member_level === 255;
  const logged_in = useAppSelector((state) => state.user.logged_in);

  const supported_chains = useAppSelector(
    (state) => state.app.supported_blockchains,
  );

  useEffect(() => {
    if (id === undefined || !logged_in) return;
    apis.projects.get_by_id(id).then((result) => {
      if (result === FailedResponse) return;
      setProject(result as ProjectDetailsDTO);
    });
  }, [id, logged_in, refresh]);

  const handle_add_gnosis_safe = async (event: any): Promise<void> => {
    event.preventDefault();
    event.stopPropagation();

    const form = event.currentTarget;

    if (project === undefined) return;

    const dto: CreateGnosisSafeDTO = {
      name: form.name.value,
      chain_id: form.chain_id.value,
      address: form.address.value,
    };

    const added = await apis.gnosis.create(project.id, dto);
    if (added === FailedResponse) return;
    setRefresh(!refresh);
  };

  if (!logged_in) return <NotLoggedIn />;
  if (project === undefined) return <Loading />;

  return (
    <PageContainer>
      <HeaderText text="Project Details" />
      Name: {project.name}
      <br />
      Website: {project.website}
      <br />
      Contact: {project.email_address}
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
          {project.members &&
            project.members.map((member) => (
              <tr key={member.display_name}>
                <td>{member.display_name}</td>
                <td>{tool_set.project_member_level_string(member.level)}</td>
              </tr>
            ))}
        </tbody>
      </table>
      Gnosis Safes
      <br />
      <table>
        <thead>
          <tr>
            <td>Name</td>
            <td>Blockchain</td>
            <td>Address</td>
          </tr>
        </thead>
        <tbody>
          {project.gnosis_safes &&
            project.gnosis_safes.map((safe) => (
              <tr key={safe.id}>
                <td>{safe.name}</td>
                <td>{safe.chain_id}</td>
                <td>
                  <TextLink text={safe.address} target={`/gnosis/${safe.id}`} />
                </td>
              </tr>
            ))}
        </tbody>
      </table>
      {admin && (
        <>
          <HeaderText text="Add Gnosis Safe" />
          <StandardForm onSubmit={handle_add_gnosis_safe}>
            <FormInput text="Name" type="text" name="name" />
            <FormInput text="Address" type="text" name="address" />
            <select name="chain_id">
              {supported_chains.map((chain) => (
                <option key={chain.chain_id} value={chain.chain_id}>
                  {chain.name}
                </option>
              ))}
            </select>
            <FormButton text="CREATE" />
          </StandardForm>
        </>
      )}
    </PageContainer>
  );
};

export default ProjectDetails;
