import React from 'react';
import tw from 'tailwind-styled-components';

const Copyright = tw.div`
  text-zombieRed
`;

export interface FooterCopyrightProps {
  text: string;
}

const FooterCopyright: React.FC<FooterCopyrightProps> = ({ text }) => {
  return <Copyright>{text}</Copyright>;
};

export default FooterCopyright;
