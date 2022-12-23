import React from 'react';
import tw from 'tailwind-styled-components';

const Button = tw.button`
  bg-zombieBlue 
  mx-1 
  w-24 
  h-12 
  font-bold 
  text-lg 
  text-zombieWhite 
  hover:bg-zombieRed 
  rounded-pill
`;

export interface FormButtonProps {
  text: string;
}

const FormButton: React.FC<FormButtonProps> = ({ text }) => {
  return <Button type="submit">{text}</Button>;
};

export default FormButton;
