import React from 'react';

import PageContainer from 'components/PageContainer';
import HeaderText from 'components/HeaderText';
import ParagraphText from 'components/ParagraphText';

const NotAuthorized: React.FC = () => {
  return (
    <PageContainer>
      <HeaderText text="Not Authorized" />
      <ParagraphText text="You are not authorized for this resource." />
    </PageContainer>
  );
};

export default NotAuthorized;
