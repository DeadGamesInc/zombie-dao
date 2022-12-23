import React from 'react';
import tw from 'tailwind-styled-components';

const Brand = tw.div`
  text-3xl 
  font-bold 
  text-zombieWhite 
  sm:text-4xl
`;

export interface NavMenuBrandProps {
  text: string;
}

const NavMenuBrand: React.FC<NavMenuBrandProps> = ({ text }) => {
  return <Brand>{text}</Brand>;
};

export default NavMenuBrand;
