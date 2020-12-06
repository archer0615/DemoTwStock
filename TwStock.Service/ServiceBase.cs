using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using TwStock.Models.OpenAPI;
using System.Threading;

namespace TwStock.Service
{
    public class ServiceBase
    {
        private readonly IHttpClientFactory httpClient;
        private const string BASE_URI = "https://www.twse.com.tw/exchangeReport/";
        public ServiceBase(IHttpClientFactory httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<IEnumerable<TWSE_StockModel>> GetStockByDate(string date)
        {
            List<TWSE_StockModel> results = new List<TWSE_StockModel>();

            using var client = httpClient.CreateClient();
            client.BaseAddress = new Uri(BASE_URI);

            var responseJson = await client.GetStringAsync($"BWIBBU_d?response=json&date={date}");

            using (JsonDocument doc = JsonDocument.Parse(responseJson, new JsonDocumentOptions { AllowTrailingCommas = true }))
            {
                JsonElement root = doc.RootElement;
                JsonElement data = root.GetProperty("data");

                foreach (var element in data.EnumerateArray())
                {
                    var tmpObject = new TWSE_StockModel();

                    var tmpArray = element.EnumerateArray().ToArray();

                    if (tmpArray.Length > 0)
                    {
                        tmpObject.StockNo = tmpArray[0].ToString();
                        tmpObject.StockName = tmpArray[1].ToString();
                        tmpObject.Date = date;
                        tmpObject.YieldRate = tmpArray[2].ToString();
                        tmpObject.YieldYear = tmpArray[3].ToString();
                        tmpObject.PER = tmpArray[4].ToString();
                        tmpObject.PBR = tmpArray[5].ToString();
                        tmpObject.FinancialReportQuarterly = tmpArray[6].ToString();

                        results.Add(tmpObject);
                    }
                }
            }
            return results;
        }
        public async Task<IEnumerable<TWSE_StockModel>> GetStockByNo(string stockNo, string date)
        {
            List<TWSE_StockModel> results = new List<TWSE_StockModel>();

            using var client = httpClient.CreateClient();
            client.BaseAddress = new Uri(BASE_URI);

            Thread.Sleep(new Random().Next(100, 1000));

            var responseJson = await client.GetStringAsync($"BWIBBU?response=json&date={date}&stockNo={stockNo}");

            using (JsonDocument doc = JsonDocument.Parse(responseJson, new JsonDocumentOptions { AllowTrailingCommas = true }))
            {
                JsonElement root = doc.RootElement;
                JsonElement data = root.GetProperty("data");

                JsonElement title = root.GetProperty("title");

                string stockName = title.ToString().Split(' ')[1];

                foreach (var element in data.EnumerateArray())
                {
                    var tmpObject = new TWSE_StockModel();

                    var tmpArray = element.EnumerateArray().ToArray();

                    if (tmpArray.Length > 0)
                    {
                        tmpObject.StockNo = stockNo;
                        tmpObject.StockName = stockName;
                        tmpObject.Date = ConvertToStringDate(tmpArray[0].ToString());
                        tmpObject.YieldRate = tmpArray[1].ToString();
                        tmpObject.YieldYear = tmpArray[2].ToString();
                        tmpObject.PER = tmpArray[3].ToString();
                        tmpObject.PBR = tmpArray[4].ToString();
                        tmpObject.FinancialReportQuarterly = tmpArray[5].ToString();

                        results.Add(tmpObject);
                    }
                }
            }

            return results;
        }

        private string ConvertToStringDate(string strDate)
        {
            string result = "";

            strDate = strDate.Replace("年", " ").Replace("月", " ").Replace("日", " ");
            var tmpValue = strDate.Split(' ');
            string year = (Int32.Parse(tmpValue[0]) + 1911).ToString();
            result = year + tmpValue[1] + tmpValue[2];
            return result;
        }       
    }
}
