import React from 'react';

import PageContainer from 'components/PageContainer';
import HomePageCard from 'components/HomePageCard';
import HomePageCardText from 'components/HomePageCardText';

const Home: React.FC = () => {
  return (
    <PageContainer>
      <div className="flex">
        <HomePageCard title="Zombie DAO">
          <HomePageCardText text="Welcome to the Zombie DAO where fun decentralized things will happen." />
        </HomePageCard>
      </div>
    </PageContainer>
  );
};

export default Home;
