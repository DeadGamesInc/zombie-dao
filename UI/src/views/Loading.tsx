import React from 'react';

import PageContainer from 'components/PageContainer';
import HeaderText from 'components/HeaderText';
import ParagraphText from 'components/ParagraphText';

const Loading: React.FC = () => {
  return (
    <PageContainer>
      <HeaderText text="Content Loading" />
      <ParagraphText text="Content is loading, please standby." />
    </PageContainer>
  );
};

export default Loading;
