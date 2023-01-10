import GnosisSafeTokenDetailsDTO from 'dtos/GnosisSafeTokenDetailsDTO';
import GnosisSafeTransactionDetailsDTO from 'dtos/GnosisSafeTransactionDetailsDTO';

export default interface GnosisSafeDetailsDTO {
  id: string;
  name: string;
  chain_id: number;
  address: string;
  tokens?: GnosisSafeTokenDetailsDTO[];
  transactions?: GnosisSafeTransactionDetailsDTO[];
}
