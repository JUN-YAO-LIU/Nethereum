﻿using Nethereum.Web3.Accounts;
using Nethereum.Hex.HexConvertors.Extensions;
using ETH.Models;
using System.Numerics;
using Nethereum.Contracts.Standards.ERC20.ContractDefinition;
using Nethereum.Web3;
using Nethereum.Util;
using Nethereum.RPC.Eth.DTOs;

namespace Nethereum.ETH
{
    internal class EthService
    {
        private readonly Web3.Web3 _web3;

        public EthService(string node)
        {
            _web3 = new Web3.Web3(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="privateKey"></param>
        /// <param name="url"></param>
        public EthService(string privateKey, string url)
        {
            var account = new Account(privateKey);
            _web3 = new Web3.Web3(account, url);
        }

        public GenData GenerateAddress()
        {
            var ecKey = Signer.EthECKey.GenerateKey();
            var privateKey = ecKey.GetPrivateKeyAsBytes();
            var account = new Account(privateKey);

            Console.WriteLine($"新地址: {account.Address}");
            Console.WriteLine($"私鑰: {privateKey.ToHex()}");

            return new GenData
            {
                Address = account.Address,
                PrivateKey = privateKey.ToHex()
            };
        }

        /// <summary>
        /// 查詢以太幣餘額
        /// </summary>
        /// <param name="address">0xc8Bd59d60961bEdb12c95079250d3075b9328a2d</param>
        /// <returns></returns>
        public async Task<double> CheckETHAmountAsync(string address)
        {
            var balance = await _web3.Eth.GetBalance.SendRequestAsync(address);

            Console.WriteLine($"地址 {address} 的以太幣餘額是: {CalAmountToETHUint(balance.Value)} ETH");

            // unit = wei
            return CalAmountToETHUint(balance.Value);
        }
        
        /// <summary>
        /// 查詢 ERC20 代幣餘額
        /// </summary>
        /// <param name="address">0xc8Bd59d60961bEdb12c95079250d3075b9328a2d</param>
        /// <param name="token">0x3E826335541543C1234bA0aA1C52c593ae6460a1</param>
        /// <returns></returns>
        public async Task<BigInteger> CheckErc20AmountAsync(string token,string address)
        {
            var contract = _web3.Eth.ERC20.GetContractService(token);

            if (contract == null)
            {
                throw new Exception("Failed to get contract service.");
            }

            var amount = await contract.BalanceOfQueryAsync(address);

            Console.WriteLine($"Token {token} 的餘額是: {amount}");

            return amount;
        }

        // send erc20 token
        public async Task<string> SendErc20Async(string token, string from, string to, BigInteger amount)
        {
            // gas 欄位問題，有gas 就能發起交易，但是還是會失敗。
            //var transferHandler = _web3.Eth.GetContractTransactionHandler<TransferFunction>();
            //var transfer = new TransferFunction()
            //{
            //    To = to,
            //    AmountToSend = amount,
            //    Gas = 1000000,
            //};

            //var transactionReceipt = await transferHandler.SendRequestAsync(token, transfer);

            // trasfer sucessfully。
            var contract = _web3.Eth.ERC20.GetContractService(token);
            var transactionReceipt = await contract.TransferRequestAsync(new TransferFunction
            {
                To = to,
                Value = amount
            });

            Console.WriteLine($"交易Hash {transactionReceipt}");
            return transactionReceipt;
        }

        private double CalAmountToETHUint(BigInteger amount)
        {
            // 1000000000000 ETH
            var result = Math.Pow(10, 18);
            return (double)amount / result;
        }
    }
}
