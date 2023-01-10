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
  disabled?: boolean;
}

const ActionButton: React.FC<ActionButtonProps> = ({
  text,
  onClick,
  disabled,
}) => {
  return (
    <Button onClick={onClick} disabled={disabled}>
      {text}
    </Button>
  );
};

export default ActionButton;
