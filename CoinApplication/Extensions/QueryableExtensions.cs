using System.Linq;
using CoinApplication.Models;

namespace CoinApplication.Extensions
{
    public static class QueryableExtensions
    {
        public static Money FindByDenomination(this IQueryable<Money> monies, int denominationValue)
        {
            return monies.FirstOrDefault(x => x.DenominationValue == denominationValue);
        }
    }
}
