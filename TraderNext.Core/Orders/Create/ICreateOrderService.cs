using System.Threading.Tasks;
using TraderNext.Models;

namespace TraderNext.Orders.Create
{
    public interface ICreateOrderService
    {
        Task<Order> CreateOrderAsync(CreateOrderRequest createOrderRequest);
    }
}
