import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import tw from 'tailwind-styled-components';
import { Bars3Icon, XMarkIcon } from '@heroicons/react/24/outline';

import routes from 'config/routes';
import apis from 'api';
import { useAppSelector, useAppDispatch } from 'store/hooks';
import { set_web3, set_logged_out } from 'store/user';
import { connect } from 'utils/web3';

import NavMenuBrand from 'components/NavMenuBrand';
import NavMenuLink from 'components/NavMenuLink';
import ActionButton from 'components/ActionButton';
import NavButton from 'components/NavButton';
import MobileNavLink from 'components/MobileNavLink';
import MobileActionLink from 'components/MobileActionLink';

const Wrapper = tw.div`
  relative 
  bg-zombieBlack 
  p-4 
  mt-2 
  mb-6
`;

const Container = tw.div`
  flex 
  items-center 
  justify-between
`;

const DesktopSection = tw.div`
  hidden 
  md:flex
`;

const MobileSection = tw.div`
  flex
  md:hidden
`;

const MobileMenu = tw.ul`
  absolute 
  bg-zombieBlack 
  w-full px-8
`;

const NavMenu: React.FC = () => {
  const [menuOpen, setMenuOpen] = useState(false);

  const dispatch = useAppDispatch();
  const navigate = useNavigate();

  const logged_in = useAppSelector((state) => state.user.logged_in);
  const wallet = useAppSelector((state) => state.user.wallet);

  const logout = async (): Promise<void> => {
    await apis.users.logout();
    dispatch(set_logged_out());
    navigate(routes.HOME);
  };

  const connect_wallet = async (): Promise<void> => {
    const web3 = await connect();
    if (!web3) return;
    dispatch(set_web3({ wallet: web3[0], chain_id: web3[1] }));
  };

  return (
    <Wrapper>
      <Container>
        <NavMenuBrand text="ZOMBIE DAO" />
        <DesktopSection className="space-x-4">
          <NavMenuLink text="HOME" target={routes.HOME} />
          <NavMenuLink text="PROJECTS" target={routes.HOME} />
          <NavMenuLink text="CREATE PROJECT" target={routes.CREATE_PROJECT} />
        </DesktopSection>
        <DesktopSection>
          {wallet === '' && (
            <>
              <ActionButton text="CONNECT WALLET" onClick={connect_wallet} />
            </>
          )}
          {logged_in ? (
            <>
              <ActionButton text="LOGOUT" onClick={logout} />
            </>
          ) : (
            <>
              <NavButton text="SIGN UP" target={routes.REGISTER_USER} />
              <NavButton text="LOGIN" target={routes.LOGIN} />
            </>
          )}
        </DesktopSection>
        <MobileSection>
          {menuOpen ? (
            <XMarkIcon
              className="text-zombieWhite w-8"
              onClick={() => setMenuOpen(!menuOpen)}
            />
          ) : (
            <Bars3Icon
              className="text-zombieWhite w-8"
              onClick={() => setMenuOpen(!menuOpen)}
            />
          )}
        </MobileSection>
      </Container>

      {menuOpen && (
        <MobileMenu>
          <MobileNavLink text="HOME" target={routes.HOME} />
          <MobileNavLink text="PROJECTS" target={routes.HOME} />
          {logged_in ? (
            <>
              <MobileActionLink text="LOGOUT" onClick={logout} />
            </>
          ) : (
            <>
              <MobileNavLink text="SIGN UP" target={routes.REGISTER_USER} />
              <MobileNavLink text="LOGIN" target={routes.LOGIN} />
            </>
          )}
        </MobileMenu>
      )}
    </Wrapper>
  );
};

export default NavMenu;
