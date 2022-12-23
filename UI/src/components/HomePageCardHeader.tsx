import React from 'react';
import tw from 'tailwind-styled-components';

const CardHeader = tw.div`
  text-4xl 
  text-center
  text-zombieBlack
`;

export interface HomePageCardHeaderProps {
  text: string;
}

const HomePageCardHeader: React.FC<HomePageCardHeaderProps> = ({ text }) => {
  return <CardHeader>{text}</CardHeader>;
};

export default HomePageCardHeader;
