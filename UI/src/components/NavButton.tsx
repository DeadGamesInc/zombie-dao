import React from 'react';
import tw from 'tailwind-styled-components';
import { Link } from 'react-router-dom';

const Button = tw.button`
  bg-zombieBlue 
  mx-1 
  w-24 
  h-12 
  font-bold 
  text-md 
  text-zombieWhite 
  hover:bg-zombieRed 
  rounded-pill
`;

export interface NavButtonProps {
  text: string;
  target: string;
}

const NavButton: React.FC<NavButtonProps> = ({ text, target }) => {
  return (
    <Link to={target}>
      <Button>{text}</Button>
    </Link>
  );
};

export default NavButton;
