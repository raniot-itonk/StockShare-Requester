using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockShareRequester.Model
{
    public class Stock
    {
        public long Id { get; set; }
        public Guid StockOwner { get; set; }
        public string Name { get; set; }
        public double LastTradedValue { get; set; }
        public List<Shareholder> ShareHolders { get; set; }
    }
}
