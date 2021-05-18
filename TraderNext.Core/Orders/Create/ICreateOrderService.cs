using System.Threading.Tasks;
using TraderNext.Core.Models;

namespace TraderNext.Core.Orders.Create
{
    public interface ICreateOrderService
    {
        Task<Order> CreateOrderAsync(Order order);
    }
}
