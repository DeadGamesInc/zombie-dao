import React from 'react';
import tw from 'tailwind-styled-components';

const LinkItem = tw.li`
  border-b-2 
  border-zinc-300 
  w-full 
  text-zombieWhite 
  text-xl
`;

export interface MobileActionLinkProps {
  text: string;
  onClick: () => Promise<void>;
}

const MobileActionLink: React.FC<MobileActionLinkProps> = ({
  text,
  onClick,
}) => {
  return <LinkItem onClick={onClick}>{text}</LinkItem>;
};

export default MobileActionLink;
