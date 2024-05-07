using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using build9ja.core.Entities;

namespace build9ja.core.Interfaces
{
    public interface IBasketService
    {
        Task<CustomerBasket> StoreCart(string basketid, BasketItem basketItem);
        Task<CustomerBasket> DeleteItemFromCart(string basketId,long variationId);

        Task<CustomerBasket>  ReduceCart(string basketid, long variationId);
        Task<CustomerBasket> GetBasketAsync(string basketId);
    }
}