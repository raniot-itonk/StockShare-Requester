using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StockShareRequester.Clients;
using StockShareRequester.Helpers;
using StockShareRequester.Model;
using StockShareRequester.OptionModels;

namespace StockShareRequester.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockBidController : ControllerBase
    {
        private readonly ILogger<StockBidController> _logger;
        private readonly IPublicShareOwnerControlClient _publicShareOwnerControlClient;
        private readonly IBankClient _bankClient;
        private readonly ITobinTaxerClient _tobinTaxerClient;
        private readonly IStockTraderBrokerClient _stockTraderBrokerClient;

        public StockBidController(ILogger<StockBidController> logger, IPublicShareOwnerControlClient publicShareOwnerControlClient, 
            IBankClient bankClient, ITobinTaxerClient tobinTaxerClient, IStockTraderBrokerClient stockTraderBrokerClient)
        {
            _logger = logger;
            _publicShareOwnerControlClient = publicShareOwnerControlClient;
            _bankClient = bankClient;
            _tobinTaxerClient = tobinTaxerClient;
            _stockTraderBrokerClient = stockTraderBrokerClient;
        }

        //Place Bid
        //[Authorize("BankingService.UserActions")]
        [HttpPost]
        public async Task<ActionResult> PostPlaceBid(PlaceBidInput placeBidInput)
        {
            var (jwtToken, accountId) = RequestHelper.GetJwtFromHeader(Request);
            var validateStock = _publicShareOwnerControlClient.ValidateStock(placeBidInput.StockId, jwtToken);
            var tobinTaxRate = _tobinTaxerClient.GetTobinTaxRate();

            Task.WaitAll(validateStock, tobinTaxRate);

            var reserveAmount = placeBidInput.Price * placeBidInput.AmountOfShares * (1 + tobinTaxRate.Result);
            var reservationRequest = new ReservationRequest{Amount = reserveAmount, AccountId = accountId};
            var reserveGuid = await _bankClient.Reserve(reservationRequest, jwtToken);
            var buyRequestInput = new BuyRequestInput
            {
                AccountId = accountId,
                AmountOfShares = placeBidInput.AmountOfShares,
                Price = placeBidInput.Price,
                ReserveId = reserveGuid,
                StockId = placeBidInput.StockId,
                TimeOut = placeBidInput.TimeOut
            };
            await _stockTraderBrokerClient.PostBuyRequest(buyRequestInput, jwtToken);
            return Ok();
        }
    }
}
