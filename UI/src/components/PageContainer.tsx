import React from 'react';
import tw from 'tailwind-styled-components';

const Container = tw.div`
  container 
  items-center 
  px-6
`;

export interface PageContainerProps {
  children?: React.ReactNode;
}

const PageContainer: React.FC<PageContainerProps> = ({ children }) => {
  return <Container>{children}</Container>;
};

export default PageContainer;
