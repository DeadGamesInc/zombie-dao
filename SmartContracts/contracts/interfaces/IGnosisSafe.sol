// SPDX-License-Identifier: MIT
pragma solidity ^0.8.17;

interface IGnosisSafe {
    function getOwners() external view returns (address[] memory);
    function nonce() external view returns (uint256);
}