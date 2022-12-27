import React, { useEffect } from 'react';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { ChatBubbleBottomCenterTextIcon } from '@heroicons/react/24/solid';

import routes from 'config/routes';
import apis from 'api';
import { useAppDispatch, useAppSelector } from 'store/hooks';
import { set_web3, set_logged_in } from 'store/user';
import { app_setup } from 'store/app';
import { initialize_web3 } from 'utils/web3';

import FailedResponse from 'types/FailedResponse';
import UserDetailsDTO from 'dtos/UserDetailsDTO';
import AppSetupDTO from 'dtos/AppSetupDTO';

import NavMenu from 'components/NavMenu';
import Footer from 'components/Footer';
import FooterIcon from 'components/FooterIcon';

import Home from 'views/Home';
import RegisterUser from 'views/RegisterUser';
import Login from 'views/Login';
import CreateProject from 'views/CreateProject';
import ProjectList from 'views/ProjectList';
import ProjectDetails from 'views/ProjectDetails';
import GnosisSafe from 'views/GnosisSafe';

const App: React.FC = () => {
  const logged_in = useAppSelector((state) => state.user.logged_in);
  const dispatch = useAppDispatch();

  useEffect(() => {
    initialize_web3().then((result) => {
      if (result)
        dispatch(set_web3({ wallet: result[0], chain_id: result[1] }));
    });

    apis.general.app_setup().then((result) => {
      if (result !== FailedResponse) {
        dispatch(app_setup(result as AppSetupDTO));
      }
    });

    if (!logged_in) {
      apis.users.whoami().then((result) => {
        if (result !== FailedResponse) {
          dispatch(set_logged_in(result as UserDetailsDTO));
        }
      });
    }
  }, []);

  return (
    <div>
      <BrowserRouter>
        <div className="container mx-auto">
          <NavMenu />
          <Routes>
            <Route path={routes.HOME} element={<Home />} />
            <Route path={routes.REGISTER_USER} element={<RegisterUser />} />
            <Route path={routes.LOGIN} element={<Login />} />
            <Route path={routes.CREATE_PROJECT} element={<CreateProject />} />
            <Route path={routes.PROJECTS} element={<ProjectList />} />
            <Route path={routes.PROJECT_DETAILS} element={<ProjectDetails />} />
            <Route path={routes.GNOSIS_SAFE} element={<GnosisSafe />} />
          </Routes>
          <Footer
            brand_text="Zombie DAO"
            copyright_text="Dead Games Inc"
            footer_icons={[
              <FooterIcon
                key="twitter-icon"
                target="https://twitter.com/RugZombie"
                child={
                  <ChatBubbleBottomCenterTextIcon className="w-6 text-zombieBlue" />
                }
              />,
            ]}
          />
        </div>
      </BrowserRouter>
    </div>
  );
};

export default App;
