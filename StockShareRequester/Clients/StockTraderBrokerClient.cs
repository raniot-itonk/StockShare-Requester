using System;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StockShareRequester.Helpers;
using StockShareRequester.Model;
using StockShareRequester.OptionModels;

namespace StockShareRequester.Clients
{
    public interface IStockTraderBrokerClient
    {
        Task<ValidationResult> PostBuyRequest(BuyRequestInput request, string jwtToken);
        Task<ActionResult<ValidationResult>> RemoveBuyRequest(long id, string jwtToken);
    }

    public class StockTraderBrokerClient : IStockTraderBrokerClient
    {
        private readonly StockTraderBroker _stockTraderBroker;

        public StockTraderBrokerClient(IOptionsMonitor<Services> serviceOption)
        {
            _stockTraderBroker = serviceOption.CurrentValue.StockTraderBroker ??
                           throw new ArgumentNullException(nameof(serviceOption.CurrentValue.StockTraderBroker));
        }
        public async Task<ValidationResult> PostBuyRequest(BuyRequestInput request, string jwtToken)
        {
            return await PolicyHelper.ThreeRetriesAsync().ExecuteAsync(() =>
                _stockTraderBroker.BaseAddress
                    .AppendPathSegment(_stockTraderBroker.StockTraderBrokerPath.BuyRequest)
                    .WithOAuthBearerToken(jwtToken).PostJsonAsync(request).ReceiveJson<ValidationResult>());
        }

        public async Task<ActionResult<ValidationResult>> RemoveBuyRequest(long id, string jwtToken)
        {
            return await PolicyHelper.ThreeRetriesAsync().ExecuteAsync(() =>
                _stockTraderBroker.BaseAddress
                    .AppendPathSegments(_stockTraderBroker.StockTraderBrokerPath.BuyRequest, id)
                    .WithOAuthBearerToken(jwtToken).DeleteAsync().ReceiveJson<ValidationResult>());
        }
    }
}
