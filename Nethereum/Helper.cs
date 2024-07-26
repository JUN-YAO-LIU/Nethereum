using Nethereum.Web3.Accounts;
using Nethereum.Hex.HexConvertors.Extensions;
using Org.BouncyCastle.Math;

namespace Nethereum
{
    internal class Helper
    {
        private const string node = "https://eth-sepolia.g.alchemy.com/v2/DbEb9i79GXBoPu12o66beAkCtixxnaOg";
        private readonly Web3.Web3 _web3;

        public Helper() 
        {
            _web3 = new Web3.Web3(node);
        }

        public void GenerateAddress() 
        {
            var ecKey = Signer.EthECKey.GenerateKey();
            var privateKey = ecKey.GetPrivateKeyAsBytes();
            var account = new Account(privateKey);
            Console.WriteLine($"新地址: {account.Address}");
            Console.WriteLine($"私鑰: {privateKey.ToHex()}");
        }

        public async Task SearchETHAmount() 
        {
            var address = "0xc8Bd59d60961bEdb12c95079250d3075b9328a2d"; // 你想要查詢的以太坊地址
            var balance = await _web3.Eth.GetBalance.SendRequestAsync(address);

            // unit = wei
            var etherAmount = balance.Value;

            // 1000000000000 ETH
            var result = Math.Pow(10, 18);
            Console.WriteLine($"地址 {address} 的以太幣餘額是: {(double)etherAmount/result} ETH");
        }
    }
}
