using System;

namespace StockShareRequester.Model
{
    public class Shareholder
    {
        public long Id { get; set; }
        public Guid ShareholderId { get; set; }
        public int Amount { get; set; }
    }
}