using System;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;
using StockShareRequester.Helpers;
using StockShareRequester.Model;
using StockShareRequester.OptionModels;

namespace StockShareRequester.Clients
{
    public class PublicShareOwnerControlClient : IPublicShareOwnerControlClient
    {
        private readonly PublicShareOwnerControl _publicShareOwnerControl;

        public PublicShareOwnerControlClient(IOptionsMonitor<Services> serviceOption)
        {
            _publicShareOwnerControl = serviceOption.CurrentValue.PublicShareOwnerControl ??
                           throw new ArgumentNullException(nameof(serviceOption.CurrentValue.PublicShareOwnerControl));
        }
        public async Task ValidateStock(long id, string jwtToken)
        {
            await PolicyHelper.ThreeRetriesAsync().ExecuteAsync(() =>
                _publicShareOwnerControl.BaseAddress
                    .AppendPathSegments(_publicShareOwnerControl.PublicSharePath.Stock, id)
                    .WithOAuthBearerToken(jwtToken).GetJsonAsync<Stock>());
        }
    }
}
