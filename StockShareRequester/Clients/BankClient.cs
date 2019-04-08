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
    public interface IBankClient
    {
        Task<ReservationResult> Reserve(ReservationRequest reservationRequest, string jwtToken);
    }

    public class BankClient : IBankClient
    {
        private readonly BankService _bankService;

        public BankClient(IOptionsMonitor<Services> serviceOption)
        {
            _bankService = serviceOption.CurrentValue.BankService ??
                           throw new ArgumentNullException(nameof(serviceOption.CurrentValue.BankService));
        }

        public async Task<ReservationResult> Reserve(ReservationRequest reservationRequest, string jwtToken)
        {
            return await PolicyHelper.ThreeRetriesAsync().ExecuteAsync(() =>
                _bankService.BaseAddress.AppendPathSegment(_bankService.BankPath.Reservation)
                    .WithOAuthBearerToken(jwtToken).PostJsonAsync(reservationRequest).ReceiveJson<ReservationResult>());
        }
    }
}
