import React, { ReactNode } from 'react';

export interface FooterIconProps {
  target: string;
  child: ReactNode;
}

const FooterIcon: React.FC<FooterIconProps> = ({ target, child }) => {
  return <a href={target}>{child}</a>;
};

export default FooterIcon;
