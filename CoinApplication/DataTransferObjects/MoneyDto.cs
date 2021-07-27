namespace CoinApplication.DataTransferObjects
{
    public class MoneyDto
    {
        private MoneyDto() { }

        public MoneyDto(int amount, int denominationValue)
        {
            Amount = amount;
            DenominationValue = denominationValue;
        }

        public int Amount { get; set; }

        public int DenominationValue { get; set; }

        public int Value()
        {
            return Amount * DenominationValue;
        }
    }
}
