import Web3 from 'web3';
import { AbiItem } from 'web3-utils';
import { Contract } from 'web3-eth-contract';

import gnosisAbi from 'config/abis/IGnosisSafe.json';

import GnosisSafeOnchainDetails from 'types/GnosisSafeOnchainDetails';
import FailedResponse from 'types/FailedResponse';

let ChainId = 0;
let Account = '';

let web3: Web3;

export const getContract = (abi: any, address: string): Contract =>
  new web3.eth.Contract(abi as AbiItem, address);

export const getGnosisSafe = (address: string): Contract =>
  getContract(gnosisAbi, address);

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

export const get_gnosis_details = async (
  address: string,
): Promise<GnosisSafeOnchainDetails | FailedResponse> => {
  if (address === undefined) return FailedResponse;

  const contract = getGnosisSafe(address);
  const nonce = await contract.methods.nonce().call();
  const owners = await contract.methods.getOwners().call();

  return { nonce, owners };
};
