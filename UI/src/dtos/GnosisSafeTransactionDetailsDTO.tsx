import GnosisSafeConfirmationDetailsDTO from 'dtos/GnosisSafeConfirmationDetailsDTO';

export default interface GnosisSafeTransactionDetailsDTO {
  id: string;
  creator: string;
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
  executed: boolean;
  has_signed: boolean;
  confirmations?: GnosisSafeConfirmationDetailsDTO[];
}
