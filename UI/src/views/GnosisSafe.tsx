import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';

import apis from 'api';
import { useAppSelector } from 'store/hooks';
import { get_gnosis_details } from 'utils/web3';

import GnosisSafeDetailsDTO from 'dtos/GnosisSafeDetailsDTO';
import GnosisSafeOnchainDetails from 'types/GnosisSafeOnchainDetails';
import FailedResponse from 'types/FailedResponse';

import PageContainer from 'components/PageContainer';
import HeaderText from 'components/HeaderText';
import NotLoggedIn from 'views/NotLoggedIn';
import Loading from 'views/Loading';
import WrongBlockchain from 'views/WrongBlockchain';

export type GnosisSafeParams = {
  id: string;
};

const GnosisSafe: React.FC = () => {
  const [safe, setSafe] = useState<GnosisSafeDetailsDTO>();
  const [onchain, setOnchain] = useState<GnosisSafeOnchainDetails>();
  const [owner, setOwner] = useState(false);

  const { id: id } = useParams<GnosisSafeParams>();
  const logged_in = useAppSelector((state) => state.user.logged_in);
  const chain_id = useAppSelector((state) => state.user.chain_id);
  const wallet = useAppSelector((state) => state.user.wallet);

  useEffect(() => {
    if (id === undefined || !logged_in) return;
    apis.gnosis.get_by_id(id).then((result) => {
      if (result === FailedResponse) return;
      const safeDetails = result as GnosisSafeDetailsDTO;
      setSafe(safeDetails);
      get_gnosis_details(safeDetails.address).then((onchainResult) => {
        if (onchainResult === FailedResponse) return;
        const onchainDetails = onchainResult as GnosisSafeOnchainDetails;
        setOnchain(onchainDetails);
        setOwner(onchainDetails.owners.includes(wallet));
      });
    });
  }, [id, logged_in]);

  if (!logged_in) return <NotLoggedIn />;
  if (safe === undefined || onchain === undefined) return <Loading />;
  if (safe.chain_id !== chain_id)
    return <WrongBlockchain correct_chain={safe.chain_id} />;

  return (
    <PageContainer>
      <HeaderText text="Gnosis Safe" />
      ID: {safe.id}
      <br />
      Name: {safe.name}
      <br />
      Chain ID: {safe.chain_id}
      <br />
      Address: {safe.address}
      <br />
      Owners:
      {onchain.owners.map((owner) => (
        <p key={owner}>{owner}</p>
      ))}
      <br />
      {owner && <>You are an owner on this safe</>}
    </PageContainer>
  );
};

export default GnosisSafe;
