export default interface CreateGnosisSafeTransactionDTO {
  to: string;
  data: string;
  operation: number;
  safe_tx_gas: string;
  base_gas: string;
  gas_price: string;
  gas_token: string;
  refund_receiver: string;
  value: string;
  nonce: number;
}
