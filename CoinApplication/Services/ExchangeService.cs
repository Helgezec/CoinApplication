using System;
using System.Collections.Generic;
using System.Linq;
using CoinApplication.Data;
using CoinApplication.DataTransferObjects;
using CoinApplication.Extensions;
using CoinApplication.Models;

namespace CoinApplication.Services
{
    public class ExchangeService
    {

        public static IEnumerable<MoneyDto> Exchange(IEnumerable<MoneyDto> monies, Context context)
        {
            return new ExchangeService(context).Exchange(monies);
        }

        private readonly Context context;
        private int acceptedValue;
        private readonly List<MoneyDto> moniesThatCannotBeAccepted = new List<MoneyDto>();

        ExchangeService(Context context)
        {
            this.context = context;
        }
        
        private IEnumerable<MoneyDto> Exchange(IEnumerable<MoneyDto> monies)
        {
            monies = AdjustAmountsAndAcceptMonies(monies);
            acceptedValue = monies.Sum(m => m.Value());

            var moniesToGive = CountMoniesToGive();

            context.SaveChanges();

            return moniesToGive;
        }

        private IEnumerable<MoneyDto> AdjustAmountsAndAcceptMonies(IEnumerable<MoneyDto> monies)
        {
            monies = monies.Select(moneyDto =>
            {
                var money = context.Monies.FindByDenomination(moneyDto.DenominationValue);
                
                moneyDto = AdjustAmount(money, moneyDto);

                money.AddAmount(moneyDto.Amount);

                return moneyDto;
            });
            return monies;
        }

        private MoneyDto AdjustAmount(Money money, MoneyDto moneyDto)
        {
            var acceptableAmount = money.AcceptableAmount();

            if (acceptableAmount < moneyDto.Amount)
            {
                moniesThatCannotBeAccepted
                    .Add(new MoneyDto(
                        moneyDto.Amount - acceptableAmount, moneyDto.DenominationValue));

                moneyDto = new MoneyDto(acceptableAmount, moneyDto.DenominationValue);
            }

            return moneyDto;
        }

        private IEnumerable<MoneyDto> CountMoniesToGive()
        {
            IQueryable<Money> moneyOrderedByDenomination =
                context.Monies.OrderByDescending(m => m.DenominationValue);

            var result = new List<MoneyDto>();

            while (acceptedValue > 0)
            {
                var money = moneyOrderedByDenomination.First();
                moneyOrderedByDenomination = moneyOrderedByDenomination.Where(d => d != money);

                var amountToGive = CountAmountToGiveAndUpdateMoneyAndAcceptedValue(money);

                if(amountToGive > 0)
                    result.Add(new MoneyDto(amountToGive, money.DenominationValue));
            }

            AddMoniesThatCannotBeAccepted(result);

            return result;
        }

        private int CountAmountToGiveAndUpdateMoneyAndAcceptedValue(Money money)
        {
            var amountToGive = CountAmountToGive(money);
            acceptedValue -= amountToGive * money.DenominationValue;
            money.AddAmount(-amountToGive);
            return amountToGive;
        }


        private void AddMoniesThatCannotBeAccepted(List<MoneyDto> result)
        {
            foreach (var moneyThatCannotBeAccepted in moniesThatCannotBeAccepted)
            {
                var moneyFromResult = 
                    result.Find(m => m.DenominationValue == moneyThatCannotBeAccepted.DenominationValue);
                if (moneyFromResult != null)
                    moneyFromResult.Amount += moneyThatCannotBeAccepted.Amount;
                else
                    result.Add(moneyThatCannotBeAccepted);
            }
        }

        private int CountAmountToGive(Money money)
        {
            var amountToGive = acceptedValue / money.DenominationValue;
            return Math.Min(amountToGive, money.Amount);
        }
    }
}
