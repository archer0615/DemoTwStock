using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwStock.Models.OpenAPI
{
    public class TWSE_StockModel
    {        
        public string StockNo { get; set; }
        public string StockName { get; set; }
        public string Date { get; set; }
        public string YieldRate { get; set; }
        public string YieldYear { get; set; }
        public string PER { get; set; }
        public string PBR { get; set; }
        public string FinancialReportQuarterly { get; set; }
    }
}
