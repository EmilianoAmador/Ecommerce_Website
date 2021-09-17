using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetBasketAsync(string basketId);     //gets basket ID
        Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket); 
        Task<bool> DeleteBasketAsync(string basketId);
    }
}