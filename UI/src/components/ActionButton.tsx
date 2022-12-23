import React from 'react';
import tw from 'tailwind-styled-components';

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

export interface ActionButtonProps {
  text: string;
  onClick: () => Promise<void>;
}

const ActionButton: React.FC<ActionButtonProps> = ({ text, onClick }) => {
  return <Button onClick={onClick}>{text}</Button>;
};

export default ActionButton;
