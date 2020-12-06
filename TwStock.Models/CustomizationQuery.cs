using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwStock.Extension;
using TwStock.Models.ResponseModels;

namespace TwStock.Models
{
    public static class CustomizationQuery
    {
        public static IEnumerable<TwStockByNo_DayRs> TakeTopByDay(
            this IEnumerable<TwStockByNo_DayRs> source,
            int TakeNum)
        {
            return source.OrderByDescending(y => ConvertExtension.ConvertToDate(y.Date)).Take(TakeNum);
        }
        public static IEnumerable<TwStockOfTopByDayRs> TakeTopByPER(
            this IEnumerable<TwStockOfTopByDayRs> source,
            int TakeNum)
        {
            return source.OrderByDescending(y => y.PBR).Take(TakeNum);
        }
    }
}
