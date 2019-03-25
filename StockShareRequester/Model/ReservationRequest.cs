using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockShareRequester.Model
{
    public class ReservationRequest
    {
        public Guid AccountId { get; set; }
        public double Amount { get; set; }
    }
}
