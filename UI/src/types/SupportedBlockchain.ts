export default interface SupportedBlockchain {
  chain_id: number;
  name: string;
  url: string;
  explorer_tx_prefix: string;
  base_token: string;
}
