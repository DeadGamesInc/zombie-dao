import React from 'react';
import { Link } from 'react-router-dom';

export interface NavMenuLinkProps {
  text: string;
  target: string;
}

const NavMenuLink: React.FC<NavMenuLinkProps> = ({ text, target }) => {
  return (
    <Link to={target} className="text-zombieWhite hover:text-zombieRed">
      {text}
    </Link>
  );
};

export default NavMenuLink;
