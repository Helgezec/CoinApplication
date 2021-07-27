namespace CoinApplication.Models
{
    public class ExchangeOperationItem : Entity
    {
        public bool Accepted { get; set; }

        public bool GivenAway => !Accepted;


        public int Amount { get; set; }


        public int MoneyId { get; set; }
        

        public int ExchangeOperationId { get; set; }

        public virtual ExchangeOperation ExchangeOperation { get; set; }
    }
}
