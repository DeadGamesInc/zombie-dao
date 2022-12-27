import React from 'react';

import PageContainer from 'components/PageContainer';
import HeaderText from 'components/HeaderText';
import ParagraphText from 'components/ParagraphText';

export interface WrongBlockchainProps {
  correct_chain: number;
}

const WrongBlockchain: React.FC<WrongBlockchainProps> = ({ correct_chain }) => {
  return (
    <PageContainer>
      <HeaderText text="Wrong Blockchain" />
      <ParagraphText text="You are on the wrong blockchain for this resource." />
      <ParagraphText text={`You should be on ${correct_chain}`} />
    </PageContainer>
  );
};

export default WrongBlockchain;
