import React, { ReactNode } from 'react';
import tw from 'tailwind-styled-components';

import HomePageCardHeader from 'components/HomePageCardHeader';

const Card = tw.div`
  flex-column 
  container 
  w-full 
  border-4 
  border-opacity-50 
  p-4 
  md:w-1/2
`;

export interface HomePageCardProps {
  title: string;
  children: ReactNode | ReactNode[];
}

const HomePageCard: React.FC<HomePageCardProps> = ({ title, children }) => {
  return (
    <Card>
      <HomePageCardHeader text={title} />
      <hr />
      {children}
    </Card>
  );
};

export default HomePageCard;
