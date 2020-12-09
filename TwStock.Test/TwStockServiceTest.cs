using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using TwStock.Service;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Linq;

namespace TwStock.Test
{
    public class TwStockServiceTest
    {
        private TwStockService twStockService;
        [SetUp]
        public void Setup()
        {
            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHttpClient();
                    services.AddScoped<TwStockService>();
                }).UseConsoleLifetime();

            var host = builder.Build();

            twStockService = host.Services.GetRequiredService<TwStockService>();
        }

        [Test]
        public async Task GetTwStockByNo_Day_1()
        {
            var result = await twStockService.GetTwStockByNo_Day("2002", 1);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("¤¤¿û", result.First().StockName);
            Assert.IsTrue(result.First().YieldRate > 2m);
        }
        [Test]
        public async Task GetTwStockByNo_Day_3()
        {
            var result = await twStockService.GetTwStockByNo_Day("2002", 3);
            Assert.AreEqual(3, result.Count());
        }
        [Test]
        public async Task GetOfTopByDay_20201203_5()
        {
            var result = await twStockService.GetOfTopByDay("20201203", 5);
            Assert.AreEqual(5, result.Count());
            Assert.AreEqual(32.73m, result.FirstOrDefault().PBR);
        }
        [Test]
        public async Task GetRangeOfYield_2303()
        {
            var result = await twStockService.GetRangeOfYield("2303", new System.DateTime(2020, 1, 1), new System.DateTime(2020, 3, 30));
            Assert.AreEqual("20200304", result.StartDate);
            Assert.AreEqual("20200317", result.EndDate);
        }
        [Test]
        public async Task GetRangeOfYield_2330()
        {
            var result = await twStockService.GetRangeOfYield("2330", new System.DateTime(2020, 1, 1), new System.DateTime(2020, 3, 30));
            Assert.AreEqual("20200310", result.StartDate);
            Assert.AreEqual("20200319", result.EndDate);
        }
    }
}