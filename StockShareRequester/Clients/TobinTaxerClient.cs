using System;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;
using StockShareRequester.Helpers;
using StockShareRequester.OptionModels;

namespace StockShareRequester.Clients
{
    public interface ITobinTaxerClient
    {
        Task<double> GetTobinTaxRate();
    }

    public class TobinTaxerClient : ITobinTaxerClient
    {
        private readonly TobinTaxer _tobinTaxer;

        public TobinTaxerClient(IOptionsMonitor<Services> serviceOption)
        {
            _tobinTaxer = serviceOption.CurrentValue.TobinTaxer ??
                           throw new ArgumentNullException(nameof(serviceOption.CurrentValue.TobinTaxer));
        }

        public async Task<double> GetTobinTaxRate()
        {
            return await PolicyHelper.ThreeRetriesAsync().ExecuteAsync(() =>
                _tobinTaxer.BaseAddress.AppendPathSegment(_tobinTaxer.TobinTaxerPath.TaxRate).GetJsonAsync<double>());
        }
    }
}
