import React from 'react';
import { useNavigate } from 'react-router-dom';

import { useAppSelector, useAppDispatch } from 'store/hooks';
import { set_logged_in } from 'store/user';
import apis from 'api';
import { sign_message } from 'utils/web3';
import routes from 'config/routes';

import FailedResponse from 'types/FailedResponse';
import UserDetailsDTO from 'dtos/UserDetailsDTO';

import PageContainer from 'components/PageContainer';
import HeaderText from 'components/HeaderText';
import ActionButton from 'components/ActionButton';

const Login: React.FC = () => {
  const wallet = useAppSelector((state) => state.user.wallet);
  const logged_in = useAppSelector((state) => state.user.logged_in);
  const dispatch = useAppDispatch();
  const navigate = useNavigate();

  const submit = async (): Promise<void> => {
    if (wallet === '') {
      alert('You must connect your wallet before you can login');
      return;
    }

    const challenge = await apis.users.start_login(wallet);
    if (challenge === FailedResponse) return;

    const signature = await sign_message(challenge as string);
    if (!signature) return;

    const user = await apis.users.complete_login({ wallet, signature });
    if (user === FailedResponse) {
      alert('Login Failed');
      return;
    }

    dispatch(set_logged_in(user as UserDetailsDTO));
    navigate(routes.HOME);
  };

  if (logged_in) navigate(routes.HOME);

  return (
    <PageContainer>
      <HeaderText text="User Login" />
      <ActionButton text="LOGIN" onClick={submit} />
    </PageContainer>
  );
};

export default Login;
