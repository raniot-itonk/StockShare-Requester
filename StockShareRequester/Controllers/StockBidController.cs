using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace StockShareRequester.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockBidController : ControllerBase
    {
        private readonly ILogger<StockBidController> _logger;

        public StockBidController(ILogger<StockBidController> logger)
        {
            _logger = logger;
        }

        //Place Bid
        //[Authorize("BankingService.UserActions")]
        [HttpPost]
        public async Task<ActionResult> PostPlaceBid()
        {
            return Ok(new[] { "value1", "value2" });
        }
    }
}
