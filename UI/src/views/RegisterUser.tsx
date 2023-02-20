import React from 'react';
import { useNavigate } from 'react-router-dom';

import apis from 'api';
import routes from 'config/routes';
import { sign_message } from 'utils/web3';
import { set_logged_in } from 'store/user';
import { useAppSelector, useAppDispatch } from 'store/hooks';

import FailedResponse from 'types/FailedResponse';
import CreateUserDTO from 'dtos/CreateUserDTO';
import UserDetailsDTO from '../dtos/UserDetailsDTO';

import PageContainer from 'components/PageContainer';
import HeaderText from 'components/HeaderText';
import StandardForm from 'components/StandardForm';
import FormInput from 'components/FormInput';
import FormButton from 'components/FormButton';

const RegisterUser: React.FC = () => {
  const navigate = useNavigate();
  const dispatch = useAppDispatch();

  const wallet = useAppSelector((state) => state.user.wallet);

  const submit = async (event: any): Promise<void> => {
    event.preventDefault();
    event.stopPropagation();

    const form = event.currentTarget;

    if (wallet === '') {
      alert('Your wallet is not connected');
      return;
    }

    const challenge = await apis.users.start_registration(wallet);
    if (challenge === FailedResponse) return;

    const signature = await sign_message(challenge as string);
    const dto: CreateUserDTO = {
      wallet,
      signature,
      display_name: form.display_name.value,
    };

    console.log(dto);

    const result = await apis.users.complete_registration(dto);
    if (result === FailedResponse) return;

    dispatch(set_logged_in(result as UserDetailsDTO));
    navigate(routes.HOME);
  };

  return (
    <PageContainer>
      <HeaderText text="User Registration" />
      <StandardForm onSubmit={submit}>
        <FormInput text="Display Name" type="text" name="display_name" />
        <FormButton text="REGISTER" />
      </StandardForm>
    </PageContainer>
  );
};

export default RegisterUser;
