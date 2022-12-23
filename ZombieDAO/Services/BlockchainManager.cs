using Nethereum.ABI.FunctionEncoding;
using Nethereum.HdWallet;
using Nethereum.JsonRpc.Client;
using Nethereum.Signer;
using Nethereum.Web3.Accounts;
using Polly;
using Polly.Contrib.WaitAndRetry;

using Log = Serilog.Log;

namespace ZombieDAO.Services; 

public sealed class BlockchainManager {
    private readonly Serilog.ILogger _logger = Log.Logger.ForContext<BlockchainManager>();

    private readonly Dictionary<string, string> _abis = new();
    private readonly List<BlockchainSigningRequest> _signingRequests = new();
    
    private readonly IAsyncPolicy _web3Retry = 
        Policy
            .Handle<RpcClientUnknownException>(exception => exception.InnerException is HttpRequestException)
            .Or<RpcResponseException>(exception => exception.Message.Contains("public error: eth_call"))
            .Or<RpcClientTimeoutException>()
            .WaitAndRetryAsync(Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(1), 5));
    
    private readonly IAsyncPolicy _web3Breaker =
        Policy
            .Handle<RpcClientUnknownException>(exception => exception.InnerException is HttpRequestException)
            .Or<RpcResponseException>(exception => exception.Message.Contains("public error: eth_call"))
            .Or<RpcClientTimeoutException>()
            .AdvancedCircuitBreakerAsync(0.5, TimeSpan.FromSeconds(10), 10, TimeSpan.FromSeconds(30));

    private bool _initialized;
    private string _wallet = string.Empty;

    public async Task Initialize() {
        if (_initialized) return;
        _initialized = true;
        
        _logger.Information("Blockchain manager is initializing");

        try {
            foreach (var abi in Globals.ABI_FILES) {
                var file = Path.Combine(Globals.ABIS_DIR, abi.filename);
                if (!File.Exists(file)) throw new Exception("ABI file missing: " + abi.filename);
                _abis[abi.key] = await File.ReadAllTextAsync(file);
            }
            
            _wallet = GetAccount(0).Address;
        }
        catch (Exception error) {
            _logger.Error(error, "Exception occurred during Blockchain Manager initialization");
        }
    }
    
    public string GetSigningChallenge(string wallet) {
        _signingRequests.RemoveAll(a => a.Wallet == wallet);

        var request = new BlockchainSigningRequest {
            Wallet = wallet, 
            Challenge = $"ZOMBIE-DAO-SIGNING-REQUEST-{Tools.GenerateRandomString(16)}-This is not a transaction-{DateTime.Now:yyyyMMddHHmmssfff}"
        };
        
        _signingRequests.Add(request);
        return request.Challenge;
    }

    public bool ValidateSignature(string wallet, string signature) {
        var request = _signingRequests.FirstOrDefault(a => a.Wallet == wallet);
        if (request == null) return false;
        var signer = new EthereumMessageSigner();
        var address = signer.EncodeUTF8AndEcRecover(request.Challenge, signature);
        var success = string.Equals(address, request.Wallet, StringComparison.CurrentCultureIgnoreCase);
        if (success) _signingRequests.Remove(request);
        return success;
    }
    
    private static Account GetAccount(int chainId) {
        var seed = Environment.GetEnvironmentVariable("ZOMBIE_DAO_WALLET_SEED");
        var password = Environment.GetEnvironmentVariable("ZOMBIE_DAO_WALLET_PASSWORD");
        var indexVar = Environment.GetEnvironmentVariable("ZOMBIE_DAO_WALLET_INDEX");

        if (string.IsNullOrEmpty(seed)) throw new Exception("Wallet seed is not defined");
        var index = indexVar != null ? int.Parse(indexVar) : 0;

        return new Wallet(seed, password).GetAccount(index, chainId);
    }
    
    private async Task<T> Executor<T>(Func<Task<T>> action, string operation, int chainId) {
        try {
            return await Policy.WrapAsync(_web3Retry, _web3Breaker).ExecuteAsync(action);
        }
        catch (RpcClientUnknownException error) {
            if (error.InnerException is HttpRequestException) _logger.Warning("Blockchain node timeout during {Operation} on {ChainId}", operation, chainId);
            else _logger.Error(error, "An exception occured during {Operation} on {ChainId}", operation, chainId);
            throw new InternalException();
        }
        catch (RpcResponseException error) {
            if (error.Message.Contains("public error: eth_call")) 
                _logger.Warning("Blockchain node timeout during {Operation} on {ChainId}", operation, chainId);
            else _logger.Error(error, "An exception occured during {Operation} on {ChainId}", operation, chainId);
            throw new InternalException();
        }
        catch (RpcClientTimeoutException) {
            _logger.Warning("Blockchain node timeout during {Operation} on {ChainId}", operation, chainId);
            throw new InternalException();
        }
        catch (SmartContractRevertException error) {
            _logger.Error(error, "A smart contract exception occured during {Operation} on {ChainId}", operation, chainId);
            throw new InternalException();
        }
        catch (Exception error) {
            _logger.Error(error, "An exception occured during {Operation} on {ChainId}", operation, chainId);
            throw new InternalException();
        }
    }
}
