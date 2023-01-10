import Web3 from 'web3';
import { AbiItem } from 'web3-utils';
import { Contract } from 'web3-eth-contract';

import gnosisAbi from 'config/abis/IGnosisSafe.json';
import ierc20Abi from 'config/abis/IERC20.json';

import GnosisSafeOnchainDetails from 'types/GnosisSafeOnchainDetails';
import FailedResponse from 'types/FailedResponse';

let ChainId = 0;
let Account = '';

let web3: Web3;

export const get_contract = (abi: any, address: string): Contract =>
  new web3.eth.Contract(abi as AbiItem, address);

export const get_erc20 = (address: string): Contract =>
  get_contract(ierc20Abi, address);

export const get_gnosis_safe = (address: string): Contract =>
  get_contract(gnosisAbi, address);

export const initialize_web3 = async (): Promise<[string, number]> => {
  const provider = (window as any).ethereum;

  if (typeof provider !== 'undefined') {
    web3 = new Web3(provider);
    ChainId = await web3.eth.getChainId();
    const result = await web3.eth.getAccounts();
    if (result.length !== 0) Account = result[0].toString();

    await provider.on('accountsChanged', function (result: any) {
      if (result[0]) Account = result[0].toString();
      else Account = '';
    });
  }

  return [Account, ChainId];
};

export const connect = async (): Promise<[string, number]> => {
  const provider = (window as any).ethereum;

  if (typeof provider !== 'undefined') {
    await provider
      .request({ method: 'eth_requestAccounts' })
      .then((result: any) => {
        Account = result[0].toString();
      })
      .catch((error: any) => {
        console.log(error);
      });
  }

  return [Account, ChainId];
};

export const sign_message = async (message: string): Promise<string> => {
  try {
    if (Account === '') return '';
    let signature = '';
    await web3.eth.personal.sign(
      message,
      Account,
      '',
      function (error, result) {
        signature = result;
      },
    );
    return signature;
  } catch (error) {
    console.log(error);
    return '';
  }
};

export const sign_transaction = async (
  tx_info: any,
): Promise<string | FailedResponse> => {
  try {
    let signature = '';
    await web3.eth.personal.signTransaction(
      tx_info,
      '',
      function (error, result) {
        if (error) {
          console.log(error);
          return FailedResponse;
        } else signature = result.raw;
      },
    );
    return signature;
  } catch (error) {
    console.log(error);
    return FailedResponse;
  }
};

export const get_gnosis_details = async (
  address: string,
): Promise<GnosisSafeOnchainDetails | FailedResponse> => {
  if (address === undefined) return FailedResponse;

  const contract = get_gnosis_safe(address);
  const nonce = await contract.methods.nonce().call();
  const owners = await contract.methods.getOwners().call();
  const threshold = await contract.methods.threshold.call();

  return {
    nonce,
    owners,
    threshold,
  };
};

export const get_balance = async (
  account: string,
): Promise<string | FailedResponse> => {
  try {
    return (await web3.eth.getBalance(account)).toString();
  } catch (error) {
    console.log(error);
    return FailedResponse;
  }
};

export const get_erc20_balance = async (
  account: string,
  address: string,
): Promise<string | FailedResponse> => {
  try {
    return (
      await get_erc20(address).methods.balanceOf(account).call()
    ).toString();
  } catch (error) {
    console.log(error);
    return FailedResponse;
  }
};

export const get_eip712_message_types = () => {
  return {
    SafeTx: [
      { type: 'address', name: 'to' },
      { type: 'uint256', name: 'value' },
      { type: 'bytes', name: 'data' },
      { type: 'uint8', name: 'operation' },
      { type: 'uint256', name: 'safeTxGas' },
      { type: 'uint256', name: 'baseGas' },
      { type: 'uint256', name: 'gasPrice' },
      { type: 'address', name: 'gasToken' },
      { type: 'address', name: 'refundReceiver' },
      { type: 'uint256', name: 'nonce' },
    ],
  };
};
