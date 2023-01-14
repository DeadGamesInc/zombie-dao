export const EIP712_NOT_SUPPORTED_ERROR_MSG =
  "EIP712 is not supported by user's wallet";

export const EIP712_DOMAIN_BEFORE_V130 = [
  {
    type: 'address',
    name: 'verifyingContract',
  },
];

export const EIP712_DOMAIN = [
  {
    type: 'uint256',
    name: 'chainId',
  },
  {
    type: 'address',
    name: 'verifyingContract',
  },
];

export type Eip712MessageTypes = {
  EIP712Domain: {
    type: string;
    name: string;
  }[];
  SafeTx: {
    type: string;
    name: string;
  }[];
};

export enum Operation {
  CALL = 0,
  DELEGATE = 1,
}

export type TxArgs = {
  baseGas: string;
  value: string;
  data: string;
  gasPrice: string;
  gasToken: string;
  nonce: number;
  operation: Operation;
  refundReceiver: string;
  safeTxGas: string;
  to: string;
};
