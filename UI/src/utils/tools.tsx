import { store } from 'store';
import BigNumber from 'bignumber.js';
import SupportedBlockchain from 'types/SupportedBlockchain';

const get_balance_amount = (amount: BigNumber, decimals = 18): BigNumber =>
  new BigNumber(amount).dividedBy(BigNumber(10).pow(decimals));

const get_wei_amount = (amount: string, decimals = 18): BigNumber =>
  new BigNumber(amount).times(BigNumber(10).pow(decimals));

const get_balance_display = (
  balance: string,
  decimals = 18,
  decimalsToAppear = 4,
): string =>
  get_balance_amount(BigNumber(balance), decimals)
    .toFixed(decimalsToAppear, BigNumber.ROUND_FLOOR)
    .toString();

const project_member_level_string = (level: number): string => {
  if (level === 255) return 'ADMIN';
  return 'MEMBER';
};

const get_blockchain = (chain_id: number): SupportedBlockchain => {
  const state = store.getState();
  const chains = state.app.supported_blockchains;
  return chains.filter((a) => a.chain_id === chain_id)[0];
};

export interface ToolSet {
  project_member_level_string: (level: number) => string;
  get_blockchain: (chain_id: number) => SupportedBlockchain;
  get_balance_amount: (amount: BigNumber, decimals: number) => BigNumber;
  get_wei_amount: (amount: string, decimals: number) => BigNumber;

  get_balance_display: (
    balance: string,
    decimals: number,
    decimalsToAppear: number,
  ) => string;
}

const tool_set: ToolSet = {
  project_member_level_string,
  get_blockchain,
  get_balance_amount,
  get_wei_amount,
  get_balance_display,
};

export default tool_set;
