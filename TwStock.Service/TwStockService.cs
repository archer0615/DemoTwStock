using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TwStock.Extension;
using TwStock.Models;
using TwStock.Models.OpenAPI;
using TwStock.Models.ResponseModels;

namespace TwStock.Service
{
    public class TwStockService : ServiceBase
    {
        public TwStockService(IHttpClientFactory httpClient) : base(httpClient)
        {
        }

        public async Task<IEnumerable<TwStockByNo_DayRs>> GetTwStockByNo_Day(string stockNo, int searchDays)
        {
            DateTime queryDate = DateTime.Now;
            List<TWSE_StockModel> results = new List<TWSE_StockModel>();

            do
            {
                results.AddRange(await GetStockByNo(stockNo, queryDate.ToString("yyyyMMdd")));
                queryDate = queryDate.AddMonths(-1);
            } while (results.Count < searchDays);

            var returnResults = results.Select(x => new TwStockByNo_DayRs
            {
                Date = x.Date,
                StockNo = x.StockNo,
                StockName = x.StockName,
                YieldYear = ConvertExtension.ConvertToInt(x.YieldYear),
                YieldRate = ConvertExtension.ConvertToDecimal(x.YieldRate),
                PBR = ConvertExtension.ConvertToDecimal(x.PBR),
                PER = ConvertExtension.ConvertToDecimal(x.PER),
                FinancialReportQuarterly = x.FinancialReportQuarterly,
            }).TakeTopByDay(searchDays);

            return returnResults;
        }
        public async Task<IEnumerable<TwStockOfTopByDayRs>> GetOfTopByDay(string date, int top)
        {
            List<TWSE_StockModel> results = new List<TWSE_StockModel>();

            results.AddRange(await GetStockByDate(date));

            var returnResults = results.Select(x => new TwStockOfTopByDayRs
            {
                Date = x.Date,
                StockNo = x.StockNo,
                StockName = x.StockName,
                YieldYear = ConvertExtension.ConvertToInt(x.YieldYear),
                YieldRate = ConvertExtension.ConvertToDecimal(x.YieldRate),
                PBR = ConvertExtension.ConvertToDecimal(x.PBR),
                PER = ConvertExtension.ConvertToDecimal(x.PER),
                FinancialReportQuarterly = x.FinancialReportQuarterly,
            }).TakeTopByPER(top);

            return returnResults;
        }
        public async Task<TwStockOfYieldRateByNo_DateRangeRs> GetRangeOfYield(
            string stockNo, DateTime sDate, DateTime eDate)
        {
            List<TWSE_StockModel> results = new List<TWSE_StockModel>();
            do
            {
                results.AddRange(await GetStockByNo(stockNo, sDate.ToString("yyyyMMdd")));
                sDate = sDate.AddMonths(1);
            } while (sDate <= eDate);

            var returnResults = results.Select(x => new TwStockModel
            {
                Date = x.Date,
                StockNo = x.StockNo,
                YieldRate = ConvertExtension.ConvertToDecimal(x.YieldRate),
            }).OrderBy(x => ConvertExtension.ConvertToDate(x.Date)).ToList();
              
            int count = 0;
            int max = 0;
            string tmpStartDate = "";
            var tmpResult = new TwStockOfYieldRateByNo_DateRangeRs();
            for (int index = 0; index < returnResults.Count; index++)
            {
                if (index + 1 == returnResults.Count)
                {
                    if (max < count)
                    {
                        tmpResult.StartDate = tmpStartDate;
                        tmpResult.EndDate = returnResults[index].Date;
                    }
                    break;
                }
                if (returnResults[index].YieldRate < returnResults[index + 1].YieldRate)
                {
                    if (count == 0) tmpStartDate = returnResults[index].Date;
                    count++;
                }
                else
                {
                    if (max < count)
                    {
                        tmpResult.StartDate = tmpStartDate;
                        tmpResult.EndDate= returnResults[index].Date;
                        max = count;
                    }
                    count = 0;
                }
            }

            return tmpResult;
        }
    }
}
