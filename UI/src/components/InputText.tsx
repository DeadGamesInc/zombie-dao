import React from 'react';
import tw from 'tailwind-styled-components';

const Text = tw.div`
  text-zombieBlue 
  text-lg 
  w-40
`;

export interface InputTextProps {
  text: string;
}

const InputText: React.FC<InputTextProps> = ({ text }) => {
  return <Text>{text}</Text>;
};

export default InputText;
