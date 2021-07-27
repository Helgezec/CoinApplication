using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoinApplication.Data;
using CoinApplication.DataTransferObjects;
using CoinApplication.Extensions;
using CoinApplication.Models;
using CoinApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoinApplication.Controllers
{
    [ApiController]
    [Route("exchange")]
    public class ExchangeController
    {
        private readonly Context context;

        public ExchangeController([FromServices] Context context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<IEnumerable<MoneyDto>> Exchange(MoneyDto[] acceptedMonies)
        {
            CheckDenomination(acceptedMonies);

            var returnedMoney = ExchangeService.Exchange(acceptedMonies, context);

            await LogExchangeOperation(acceptedMonies, returnedMoney);

            return returnedMoney;
        }

        private void CheckDenomination(MoneyDto[] acceptedMonies)
        {
            if (acceptedMonies.Any(m =>
                context.Monies.FindByDenomination(m.DenominationValue)?.Acceptable != true))
                throw new ArgumentException("Неправильный номинал");
        }

        private async Task LogExchangeOperation(MoneyDto[] acceptedMonies, IEnumerable<MoneyDto> returnedMoney)
        {
            var exchangeOperation = new ExchangeOperation();
            await context.ExchangeOperations.AddAsync(exchangeOperation);
            await AddExchangeLogs(acceptedMonies, exchangeOperation, true);
            await AddExchangeLogs(returnedMoney, exchangeOperation, false);
            await context.SaveChangesAsync();
        }

        private async Task AddExchangeLogs(IEnumerable<MoneyDto> monies,
            ExchangeOperation exchangeOperation, bool accepted)
        {
            foreach (var money in monies)
            {
                await context.ExchangeOperationItem.AddAsync(
                    new ExchangeOperationItem
                    {
                        Accepted = accepted,
                        Amount = money.Amount,
                        ExchangeOperation = exchangeOperation,
                        MoneyId = context.Monies.FindByDenomination(money.DenominationValue).Id
                    });
            }
        }

    }
}
