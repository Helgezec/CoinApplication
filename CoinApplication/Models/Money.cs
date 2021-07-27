namespace CoinApplication.Models
{
    public class Money : Entity
    {
        public Money(int amount, int denominationValue, bool acceptable, int maxAmount)
        {
            Amount = amount;
            DenominationValue = denominationValue;
            Acceptable = acceptable;
            MaxAmount = maxAmount;
        }

        public int Amount { get; private set; }

        public int DenominationValue { get; private set; }

        public bool Acceptable { get; private set; }

        public int MaxAmount { get; private set; }

        public void AddAmount(int value)
        {
            Amount += value;
        }

        public int AcceptableAmount() => MaxAmount - Amount;
    }
}
