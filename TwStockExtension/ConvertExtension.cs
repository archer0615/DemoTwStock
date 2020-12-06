using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwStock.Extension
{
    public static class ConvertExtension
    {
        public static DateTime ConvertToDate(string strDate)
        {
            DateTime result = DateTime.ParseExact(strDate, "yyyyMMdd", CultureInfo.InvariantCulture);

            return result;
        }
        public static int ConvertToInt(string inStr)
        {
            int.TryParse(inStr, out int result);
            return result;
        }
        public static decimal ConvertToDecimal(string inStr)
        {
            decimal.TryParse(inStr, out decimal result);
            return result;
        }
    }
}
