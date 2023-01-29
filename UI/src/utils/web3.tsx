import Web3 from 'web3';
import { AbiItem } from 'web3-utils';
import { Contract } from 'web3-eth-contract';

import gnosisAbi from 'config/abis/IGnosisSafe.json';
import ierc20Abi from 'config/abis/IERC20.json';

import GnosisSafeOnchainDetails from 'types/GnosisSafeOnchainDetails';
import FailedResponse from 'types/FailedResponse';
import { AbstractProvider } from 'web3-core';
import GnosisSafeDetailsDTO from '../dtos/GnosisSafeDetailsDTO';
import { adjustV, GenerateTypedData, generateTypedDataFrom } from './eip712';
import { EMPTY_DATA } from '../types/Web3';
import { EIP712_NOT_SUPPORTED_ERROR_MSG, TxArgs } from '../types/EIP712';

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

const eip712Signer = (
  signer: string,
  typedData: GenerateTypedData,
): Promise<string> => {
  const jsonTypedData = JSON.stringify(typedData);

  // this will have to before supporting more safe versions
  const signedTypedData = {
    jsonrpc: '2.0',
    method: 'eth_signTypedData_v3',
    params: [signer, jsonTypedData],
    from: signer,
    id: new Date().getTime(),
  };

  return new Promise((resolve) => {
    const provider = web3.currentProvider as AbstractProvider;
    provider.sendAsync(signedTypedData, (err, signature) => {
      if (err) {
        throw err;
      }

      if (signature?.result == null) {
        throw new Error(EIP712_NOT_SUPPORTED_ERROR_MSG);
      }

      const sig = adjustV('eth_signTypedData', signature.result);
      resolve(sig.replace(EMPTY_DATA, ''));
    });
  });
};

export const sign_gnosis_transaction = async (
  tx_info: TxArgs,
  safe: GnosisSafeDetailsDTO,
): Promise<string | FailedResponse> => {
  try {
    const typedData = generateTypedDataFrom(tx_info, safe);
    return await eip712Signer(Account, typedData);
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
  const threshold = await contract.methods.getThreshold().call();

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
