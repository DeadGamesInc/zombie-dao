import React from 'react';

import PageContainer from 'components/PageContainer';
import HeaderText from 'components/HeaderText';
import ParagraphText from 'components/ParagraphText';

const NotLoggedIn: React.FC = () => {
  return (
    <PageContainer>
      <HeaderText text="Not Logged In" />
      <ParagraphText text="You must be logged in to access this resource." />
    </PageContainer>
  );
};

export default NotLoggedIn;
