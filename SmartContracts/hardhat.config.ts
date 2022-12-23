require("@nomicfoundation/hardhat-toolbox");
require("hardhat-abi-exporter");
require("@openzeppelin/test-helpers")

const fs = require("fs");

const mnemonic = fs.readFileSync('.secret').toString().trim();
const bscApiKey = fs.readFileSync('.bscscanApiKey').toString().trim();
const polygonApiKey = fs.readFileSync('.polygonscanApiKey').toString().trim();

module.exports = {
    abiExporter: [
        {
            clear: true,
            flat: true,
            path: '../ZombieDAO/abis',
            format: 'json',
            runOnCompile: true,
            spacing: 4
        },
        {
            clear: true,
            flat: true,
            path: '../UI/src/config/abis',
            format: 'json',
            runOnCompile: true,
            spacing: 4
        }
    ],
    etherscan: {
        apiKey: {
            bsc: bscApiKey,
            bscTestnet: bscApiKey,
            polygon: polygonApiKey,
            polygonMumbai: polygonApiKey
        }
    },
    networks: {
        bsc: {
            accounts: { mnemonic: mnemonic },
            chainId: 56,
            url: 'https://bsc-dataseed2.binance.org/'
        },
        bsc_testnet: {
            accounts: { mnemonic: mnemonic },
            chainId: 97,
            url: 'https://data-seed-prebsc-2-s1.binance.org:8545/'            
        },
        ckb: {
            accounts: { mnemonic: mnemonic },
            chainId: 71402,
            url: 'wss://v1.mainnet.godwoken.io/ws'
        },
        ckb_testnet: {
            accounts: { mnemonic: mnemonic },
            chainId: 71401,
            url: 'wss://godwoken-testnet-v1.ckbapp.dev/ws'
        },
        matic: {
            accounts: { mnemonic: mnemonic },
            chainId: 137,
            url: 'https://rpc-mainnet.maticvigil.com'
        },
        matic_testnet: {
            accounts: { mnemonic: mnemonic },
            chainId: 80001,
            url: 'https://rpc-mumbai.maticvigil.com'
        }
    },
    solidity: '0.8.17'
};
