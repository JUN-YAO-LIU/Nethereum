using Nethereum.ETH;

var h = new EthService();
// h.GenerateAddress();
await h.CheckETHAmountAsync("0xc8Bd59d60961bEdb12c95079250d3075b9328a2d");

await h.CheckErc20AmountAsync(
    "0x3E826335541543C1234bA0aA1C52c593ae6460a1",
    "0xc8Bd59d60961bEdb12c95079250d3075b9328a2d");