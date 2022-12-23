import React from 'react';
import tw from 'tailwind-styled-components';
import { Link } from 'react-router-dom';

const LinkItem = tw.li`
  border-b-2 
  border-zinc-300 
  w-full 
  text-zombieWhite 
  text-xl
`;

export interface MobileNavLinkProps {
  text: string;
  target: string;
}

const MobileNavLink: React.FC<MobileNavLinkProps> = ({ text, target }) => {
  return (
    <Link to={target}>
      <LinkItem>{text}</LinkItem>
    </Link>
  );
};

export default MobileNavLink;
