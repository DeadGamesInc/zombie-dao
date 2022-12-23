import React, { ReactNode } from 'react';
import tw from 'tailwind-styled-components';

const FooterCenterWrapper = tw.div`
  flex
`;

export interface FooterCenterProps {
  children: ReactNode[];
}

const FooterCenter: React.FC<FooterCenterProps> = ({ children }) => {
  return <FooterCenterWrapper>{children}</FooterCenterWrapper>;
};

export default FooterCenter;
