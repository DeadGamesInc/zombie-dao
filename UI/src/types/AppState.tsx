import SupportedBlockchain from 'types/SupportedBlockchain';

export const defaultAppState: AppState = {
  supported_blockchains: [],
};

export default interface AppState {
  supported_blockchains: SupportedBlockchain[];
}
