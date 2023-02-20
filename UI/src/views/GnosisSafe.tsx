import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';

import apis from 'api';
import config from 'config';
import tools from 'utils/tools';
import { useAppSelector } from 'store/hooks';
import { get_gnosis_safe } from 'utils/web3';

import GnosisSafeDetailsDTO from 'dtos/GnosisSafeDetailsDTO';
import CreateGnosisSafeTokenDTO from 'dtos/CreateGnosisSafeTokenDTO';
import GnosisSafeTransactionDetailsDTO from 'dtos/GnosisSafeTransactionDetailsDTO';
import CreateGnosisSafeTransactionDTO from 'dtos/CreateGnosisSafeTransactionDTO';

import GnosisSafeOnchainDetails from 'types/GnosisSafeOnchainDetails';
import TokenBalance from 'types/TokenBalance';
import FailedResponse from 'types/FailedResponse';

import PageContainer from 'components/PageContainer';
import HeaderText from 'components/HeaderText';
import StandardForm from 'components/StandardForm';
import FormInput from 'components/FormInput';
import FormButton from 'components/FormButton';
import ActionButton from 'components/ActionButton';

import NotLoggedIn from 'views/NotLoggedIn';
import Loading from 'views/Loading';
import WrongBlockchain from 'views/WrongBlockchain';

import {
  get_balance,
  get_erc20_balance,
  get_gnosis_details,
  sign_gnosis_transaction,
} from 'utils/web3';

export type GnosisSafeParams = {
  id: string;
};

const GnosisSafe: React.FC = () => {
  const [safe, setSafe] = useState<GnosisSafeDetailsDTO>();
  const [onchain, setOnchain] = useState<GnosisSafeOnchainDetails>();
  const [balances, setBalances] = useState<TokenBalance[]>([]);
  const [nonce, setNonce] = useState(0);
  const [owner, setOwner] = useState(false);
  const [reload, setReload] = useState(false);
  const [reloadRest, setReloadRest] = useState(false);

  const [pendingTransactions, setPendingTransactions] = useState<
    GnosisSafeTransactionDetailsDTO[]
  >([]);

  const { id: id } = useParams<GnosisSafeParams>();
  const logged_in = useAppSelector((state) => state.user.logged_in);
  const chain_id = useAppSelector((state) => state.user.chain_id);
  const wallet = useAppSelector((state) => state.user.wallet);

  const chain = tools.get_blockchain(chain_id);

  useEffect(() => {
    if (id === undefined || !logged_in) return;

    apis.gnosis.get_by_id(id).then((result) => {
      if (result !== FailedResponse) {
        const safeDetails = result as GnosisSafeDetailsDTO;
        setSafe(safeDetails);

        let pending_transactions: GnosisSafeTransactionDetailsDTO[] = [];
        if (safeDetails.transactions)
          pending_transactions = safeDetails.transactions.filter(
            (a) => !a.executed,
          );

        setPendingTransactions(pending_transactions);
        get_gnosis_details(safeDetails.address).then((onchainResult) => {
          if (onchainResult === FailedResponse) return;
          const onchainDetails = onchainResult as GnosisSafeOnchainDetails;
          setOnchain(onchainDetails);
          setOwner(onchainDetails.owners.includes(wallet));
          const new_nonce =
            +onchainDetails.nonce + +pending_transactions.length;
          setNonce(new_nonce);
        });
      }

      setReloadRest(!reloadRest);
    });
  }, [id, logged_in, reload]);

  useEffect(() => {
    if (safe === undefined) return;
    if (safe.chain_id !== chain_id) return;

    const new_balances: TokenBalance[] = [];

    get_balance(safe.address).then((result) => {
      if (result !== FailedResponse) {
        new_balances.push({
          balance: result as string,
          address: config.zeroAddress,
          symbol: chain.base_token,
        });
      }
    });

    safe.tokens?.forEach((token) => {
      get_erc20_balance(safe.address, token.address).then((result) => {
        if (result !== FailedResponse) {
          new_balances.push({
            balance: result as string,
            address: token.address,
            symbol: token.symbol,
          });
        }
      });
    });

    setBalances(new_balances);
  }, [reloadRest]);

  const handle_add_token = async (event: any): Promise<void> => {
    event.preventDefault();
    event.stopPropagation();

    if (safe === undefined) return;
    const form = event.currentTarget;

    const dto: CreateGnosisSafeTokenDTO = {
      symbol: form.symbol.value,
      address: form.address.value,
    };

    const result = await apis.gnosis.add_token(safe.id, dto);
    if (result === FailedResponse) return;
    setReload(!reload);
  };

  const handle_create_transaction = async (event: any): Promise<void> => {
    event.preventDefault();
    event.stopPropagation();

    if (safe === undefined) return;
    const form = event.currentTarget;
    const is_base_token = form.token.value === config.zeroAddress;

    const recipient = is_base_token ? form.recipient.value : form.token.value;
    const data = is_base_token ? '0x' : '';

    const value = is_base_token
      ? tools.get_wei_amount(form.amount.value, 18).toString()
      : '0';

    const dto: CreateGnosisSafeTransactionDTO = {
      to: recipient,
      data,
      operation: 0,
      safe_tx_gas: form.safe_tx_gas.value,
      base_gas: '0',
      gas_price: '0',
      gas_token: config.zeroAddress,
      refund_receiver: config.zeroAddress,
      value,
      nonce: parseInt(form.nonce.value),
    };

    const result = await apis.gnosis.add_transaction(safe.id, dto);
    if (result === FailedResponse) return;
    setReload(!reload);
  };

  const handle_delete_transaction = async (id: string): Promise<void> => {
    if (safe === undefined) return;
    const result = await apis.gnosis.delete_transaction(safe.id, id);
    if (result === FailedResponse) return;
    setReload(!reload);
  };

  const handle_sign_transaction = async (
    tx: GnosisSafeTransactionDetailsDTO,
    safe: GnosisSafeDetailsDTO,
  ): Promise<void> => {
    const tx_args = {
      to: tx.to,
      value: tx.value,
      data: tx.data,
      operation: tx.operation,
      safeTxGas: tx.safe_tx_gas,
      baseGas: tx.base_gas,
      gasPrice: tx.gas_price,
      gasToken: tx.gas_token,
      refundReceiver: tx.refund_receiver,
      nonce: tx.nonce,
    };

    const signature = await sign_gnosis_transaction(tx_args, safe);
    if (signature === FailedResponse) return;

    await apis.gnosis.add_confirmation(safe.id, tx.id, {
      signature: signature.toString(),
    });

    setReload(!reload);
  };

  const handle_execute_transaction = async (
    tx: GnosisSafeTransactionDetailsDTO,
    safe: GnosisSafeDetailsDTO,
  ): Promise<void> => {
    const sorted = tx.confirmations?.sort((a, b) => {
      if (a.wallet > b.wallet) return 1;
      if (b.wallet > a.wallet) return -1;
      return 0;
    });

    let signature = '0x';
    sorted?.forEach(
      (confirmation) => (signature = signature + confirmation.signature),
    );

    const contract = get_gnosis_safe(safe.address);
    await contract.methods
      .execTransaction(
        tx.to,
        tx.value,
        tx.data,
        tx.operation,
        tx.safe_tx_gas,
        tx.base_gas,
        tx.gas_price,
        tx.gas_token,
        tx.refund_receiver,
        signature,
      )
      .send({ from: wallet });

    await apis.gnosis.set_executed(safe.id, tx.id);
    setReload(!reload);
  };

  if (!logged_in) return <NotLoggedIn />;
  if (safe === undefined || onchain === undefined) return <Loading />;
  if (safe.chain_id !== chain_id)
    return <WrongBlockchain correct_chain={safe.chain_id} />;

  console.log(onchain?.threshold);
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
      Balances:
      <table>
        <thead>
          <tr>
            <td>Token</td>
            <td>Balance</td>
          </tr>
        </thead>
        <tbody>
          {balances.map((balance) => (
            <tr key={balance.address}>
              <td>{balance.symbol}</td>
              <td>{tools.get_balance_display(balance.balance, 18, 6)}</td>
            </tr>
          ))}
        </tbody>
      </table>
      Pending Transactions:
      {pendingTransactions.map((tx) => (
        <div key={tx.id}>
          Nonce: {tx.nonce}
          <br />
          Creator: {tx.creator}
          <br />
          Confirmations({tx.confirmations?.length} / {onchain?.threshold}):
          <br />
          {tx.confirmations?.map((confirmation) => (
            <div key={confirmation.id}>
              &ensp; - {confirmation.wallet}
              <br />
            </div>
          ))}
          <br />
          {tx.data === '0x' ? (
            <>
              {tools.get_balance_display(tx.value, 18, 6)} {chain.base_token} to{' '}
              {tx.to}
            </>
          ) : (
            <></>
          )}
          {owner && (
            <>
              <ActionButton
                text="DELETE"
                onClick={() => handle_delete_transaction(tx.id)}
              />
              <ActionButton
                text="SIGN"
                onClick={() => handle_sign_transaction(tx, safe)}
                disabled={tx.has_signed}
              />
              <ActionButton
                text="EXECUTE"
                onClick={() => handle_execute_transaction(tx, safe)}
                disabled={
                  (tx.confirmations?.length ?? 0) < (onchain?.threshold ?? 99)
                }
              />
            </>
          )}
        </div>
      ))}
      {owner && (
        <>
          <HeaderText text="Create Transaction" />
          <StandardForm onSubmit={handle_create_transaction}>
            <FormInput text="Recipient" type="text" name="recipient" />
            <FormInput text="Amount" type="text" name="amount" />
            <FormInput
              text="Safe Tx Gas"
              type="text"
              name="safe_tx_gas"
              defaultValue="39873"
            />
            <FormInput
              text="Nonce"
              type="text"
              name="nonce"
              defaultValue={nonce.toString()}
            />
            <select name="token">
              <option value={config.zeroAddress}>{chain.base_token}</option>
              {safe.tokens?.map((token) => (
                <option key={token.address} value={token.address}>
                  {token.symbol}
                </option>
              ))}
            </select>
            <FormButton text="CREATE" />
          </StandardForm>
          <HeaderText text="Add Safe Token" />
          <StandardForm onSubmit={handle_add_token}>
            <FormInput text="Symbol" type="text" name="symbol" />
            <FormInput text="Address" type="text" name="address" />
            <FormButton text="ADD" />
          </StandardForm>
        </>
      )}
    </PageContainer>
  );
};

export default GnosisSafe;
