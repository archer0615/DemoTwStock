using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using TwStock.Models.ResponseModels;
using TwStock.Service;

namespace TwStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TWSEController : ControllerBase
    {
        private readonly TwStockService twStockService;

        public TWSEController(TwStockService twStockService)
        {
            this.twStockService = twStockService;
        }
        [HttpGet]
        [Route("StockNo/{stockNo}/SearchDays/{searchDays}")]
        public async Task<IEnumerable<TwStockByNo_DayRs>> GetByNo_DayRs(string stockNo, int searchDays)
        {
            return await twStockService.GetTwStockByNo_Day(stockNo, searchDays);
        }
        [HttpGet]
        [Route("Date/{date}/Top/{top}")]
        public async Task<IEnumerable<TwStockOfTopByDayRs>> GetOfTopByDay(string date, int top)
        {
            return await twStockService.GetOfTopByDay(date, top);
        }

        [HttpGet]
        [Route("StockNo/{stockNo}/Start/{startDate}/End/{endDate}")]
        public async Task<TwStockOfYieldRateByNo_DateRangeRs> GetRangeOfYield(
            string stockNo = "2002", string startDate = "20201023", string endDate = "20201030")
        {
            if (string.IsNullOrWhiteSpace(stockNo) ||
                string.IsNullOrWhiteSpace(startDate) ||
                string.IsNullOrWhiteSpace(endDate))
            {
                return new TwStockOfYieldRateByNo_DateRangeRs();
            }
            DateTime.TryParseExact(startDate, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime sDate);
            DateTime.TryParseExact(endDate, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime eDate);
            if (sDate >= eDate)
            {
                return new TwStockOfYieldRateByNo_DateRangeRs();
            }
            return await twStockService.GetRangeOfYield(stockNo, sDate, eDate);
        }
    }
}
