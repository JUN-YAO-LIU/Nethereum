using System.Security;
using Nethereum.ETH;

const string node = "https://eth-sepolia.g.alchemy.com/v2/DbEb9i79GXBoPu12o66beAkCtixxnaOg";

var h = new EthService(node);
var h2 = new EthService("aa0ac0da5a336360c8517fdca341642accc55a8c318d536b302c6f93c95179ff",node);
// h.GenerateAddress();
await h.CheckETHAmountAsync("0xc8Bd59d60961bEdb12c95079250d3075b9328a2d");

await h.CheckErc20AmountAsync(
    "0x3E826335541543C1234bA0aA1C52c593ae6460a1",
    "0xc8Bd59d60961bEdb12c95079250d3075b9328a2d");

await h2.SendErc20Async(
    "0x3E826335541543C1234bA0aA1C52c593ae6460a1",
    "0xc8Bd59d60961bEdb12c95079250d3075b9328a2d",
    "0x598871bE40CBB3F04fBEA738D4F730a452036046",
    1);