using System;

namespace CoinApplication.Models
{
    public class ExchangeOperation : Entity
    {
        public DateTime DateTime { get; private set; } = DateTime.Now;
    }
}
