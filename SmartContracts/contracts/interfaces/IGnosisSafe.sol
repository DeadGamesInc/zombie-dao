// SPDX-License-Identifier: MIT
pragma solidity ^0.8.17;

contract Enum {
    enum Operation {
        Call,
        DelegateCall
    }
}

interface IGnosisSafe {
    function getOwners() external view returns (address[] memory);
    function nonce() external view returns (uint256);
    function threshold() external view returns (uint256);
    function execTransaction(address to, uint256 value, bytes calldata data, Enum.Operation operation, uint256 safeTxGas,
        uint256 baseGas, uint256 gasPrice, address gasToken, address payable refundReceiver, bytes memory signatures) 
        external payable returns (bool success);
    function getThreshold() external view returns (uint256);
}