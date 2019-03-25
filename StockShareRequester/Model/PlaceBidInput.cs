using System;

namespace StockShareRequester.Model
{
    public class PlaceBidInput
    {
        public long StockId { get; set; }
        public int AmountOfShares { get; set; }
        public double Price { get; set; }
        public DateTime TimeOut { get; set; }
    }
}