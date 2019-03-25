using System.Threading.Tasks;

namespace StockShareRequester.Clients
{
    public interface IPublicShareOwnerControlClient
    {
        Task ValidateStock(long id, string jwtToken);
    }
}