global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.Mvc;
global using Newtonsoft.Json;
global using Serilog;
global using System.ComponentModel.DataAnnotations;
global using System.ComponentModel.DataAnnotations.Schema;
global using ZombieDAO.Attributes;
global using ZombieDAO.DTOs;
global using ZombieDAO.Exceptions;
global using ZombieDAO.Models;
global using ZombieDAO.Objects;
global using ZombieDAO.Repositories;
global using ZombieDAO.Services;

namespace ZombieDAO; 

public static class Globals {
    public static readonly string ABIS_DIR = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "abis");
    public static readonly string LOGS_DIR = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
    
    public static readonly List<(string filename, string key)> ABI_FILES = new() {
        new("IERC20.json", "IERC20"), new("IERC721.json", "IERC721"), new("IERC1155.json", "IERC1155")
    };
    
    public static readonly Dictionary<int, BlockchainNode> BLOCKCHAIN_NODES = new() {
        { 56, new BlockchainNode { ChainID = 56, Name = "BSC", URL = "https://bsc-dataseed1.binance.org/", 
            ExplorerTxPrefix = "https://bscscan.com/tx/", BaseToken = "BNB"} },
        { 97, new BlockchainNode { ChainID = 97, Name = "BSC-TESTNET", URL = "https://data-seed-prebsc-2-s1.binance.org:8545/", 
            ExplorerTxPrefix = "https://testnet.bscscan.com/tx/", BaseToken = "BNB"} }
    };

    public static readonly TimeSpan IDLE_SESSION_TIMEOUT = TimeSpan.FromMinutes(30);
    public static readonly TimeSpan PERIODIC_INTERVAL = TimeSpan.FromMinutes(10);
    
    public static IEnumerable<int> SupportedBlockchainIDs => BLOCKCHAIN_NODES.Keys.ToArray();
    public static BlockchainNode[] SupportedBlockchains => SupportedBlockchainIDs.Select(id => BLOCKCHAIN_NODES[id]).ToArray();
}
