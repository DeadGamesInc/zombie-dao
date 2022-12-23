import React from 'react';

import apis from 'api';
import { useAppSelector } from 'store/hooks';

import CreateProjectDTO from 'dtos/CreateProjectDTO';
import FailedResponse from 'types/FailedResponse';

import PageContainer from 'components/PageContainer';
import HeaderText from 'components/HeaderText';
import StandardForm from 'components/StandardForm';
import FormInput from 'components/FormInput';
import FormButton from 'components/FormButton';

import NotAuthorized from 'views/NotAuthorized';

const CreateProject: React.FC = () => {
  const logged_in = useAppSelector((state) => state.user.logged_in);

  const submit = async (event: any): Promise<void> => {
    event.preventDefault();
    event.stopPropagation();

    const form = event.currentTarget;

    const dto: CreateProjectDTO = {
      name: form.name.value,
      website: form.website.value,
      email_address: form.email_address.value,
    };

    const added = await apis.projects.create(dto);
    if (added === FailedResponse) return;
  };

  if (!logged_in) return <NotAuthorized />;

  return (
    <PageContainer>
      <HeaderText text="Create Project" />
      <StandardForm onSubmit={submit}>
        <FormInput text="Name" type="text" name="name" />
        <FormInput text="Website" type="text" name="website" />
        <FormInput text="Email Address" type="text" name="email_address" />
        <FormButton text="CREATE" />
      </StandardForm>
    </PageContainer>
  );
};

export default CreateProject;
