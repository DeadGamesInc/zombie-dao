import React from 'react';
import tw from 'tailwind-styled-components';

const FooterBrandWrapper = tw.div`
  text-zombieRed 
  font-bold
`;

export interface FooterBrandProps {
  text: string;
}

const FooterBrand: React.FC<FooterBrandProps> = ({ text }) => {
  return <FooterBrandWrapper>{text}</FooterBrandWrapper>;
};

export default FooterBrand;
