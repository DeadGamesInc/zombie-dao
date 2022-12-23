import Web3 from 'web3';

let ChainId = 0;
let Account = '';

let web3: Web3;

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
