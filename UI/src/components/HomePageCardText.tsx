import React from 'react';
import tw from 'tailwind-styled-components';

const Text = tw.div`
  text-xl 
  mt-3
  text-zombieBlack
`;

export interface HomePageCardTextProps {
  text: string;
}

const HomePageCardText: React.FC<HomePageCardTextProps> = ({ text }) => {
  return <Text>{text}</Text>;
};

export default HomePageCardText;
