using System;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;
using StockShareRequester.Helpers;
using StockShareRequester.OptionModels;

namespace StockShareRequester.Clients
{
    public interface IStockTraderBrokerClient
    {
        Task PostBuyRequest(BuyRequestInput request, string jwtToken);
    }

    public class StockTraderBrokerClient : IStockTraderBrokerClient
    {
        private readonly StockTraderBroker _publicShareOwnerControl;

        public StockTraderBrokerClient(IOptionsMonitor<Services> serviceOption)
        {
            _publicShareOwnerControl = serviceOption.CurrentValue.StockTraderBroker ??
                           throw new ArgumentNullException(nameof(serviceOption.CurrentValue.StockTraderBroker));
        }
        public async Task PostBuyRequest(BuyRequestInput request, string jwtToken)
        {
            await PolicyHelper.ThreeRetriesAsync().ExecuteAsync(() =>
                _publicShareOwnerControl.BaseAddress
                    .AppendPathSegment(_publicShareOwnerControl.StockTraderBrokerPath.BuyRequest)
                    .WithOAuthBearerToken(jwtToken).PostJsonAsync(request));
        }
    }
}
