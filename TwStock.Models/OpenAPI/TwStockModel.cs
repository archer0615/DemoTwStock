using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwStock.Models.OpenAPI
{
    public class TwStockModel
    {
        public string StockNo { get; set; }
        public string StockName { get; set; }
        public string Date { get; set; }
        public decimal YieldRate { get; set; }
        public int YieldYear { get; set; }
        public decimal PER { get; set; }
        public decimal PBR { get; set; }
        public string FinancialReportQuarterly { get; set; }
    }
}
