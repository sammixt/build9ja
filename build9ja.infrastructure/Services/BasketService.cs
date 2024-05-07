using build9ja.core.Entities;
using build9ja.core.Interfaces;
using build9ja.core.Specifications;
using System.Text.Json;

namespace build9ja.infrastructure.Services
{
    public class BasketService : IBasketService
    {
        private readonly IUnitOfWork _unitOfWork;
        public BasketService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomerBasket> DeleteItemFromCart(string basketId, long variationId)
        {
             AppRedisSpecification spec = new AppRedisSpecification(basketId);
             var dataExist = await _unitOfWork.Repository<AppRedis>().GetEntityWithSpec(spec);
           
               var basketItems =  JsonSerializer.Deserialize<CustomerBasket>(dataExist.Value);
               var basket = basketItems.Items.FirstOrDefault(x => x.VariationId == variationId);
                basketItems.Items.Remove(basket);
                dataExist.Value = JsonSerializer.Serialize(basketItems);
                _unitOfWork.Repository<AppRedis>().Update(dataExist);

                var updated = await _unitOfWork.Complete();
           
            if (updated < 1) return null;

            return await GetBasketAsync(basketId);
        }

        public async Task<CustomerBasket> StoreCart(string basketid, BasketItem basketItem)
        {
            AppRedisSpecification spec = new AppRedisSpecification(basketid);
            var dataExist = await _unitOfWork.Repository<AppRedis>().GetEntityWithSpec(spec);
            if (dataExist == null)
            {
                
                CustomerBasket basket = new CustomerBasket();
                basket.DeliveryMethod = 0;
                basket.ShippingPrice = 0;
                basket.Items = new List<BasketItem>(){basketItem};
                basket.Id = basketid;
                var expiryDate = DateTimeOffset.UtcNow.AddDays(20);
                string data = JsonSerializer.Serialize(basket);
                AppRedis redis = new AppRedis();
                redis.Id = basketid;
                redis.Value = data;
                redis.Expiry = expiryDate;
                _unitOfWork.Repository<AppRedis>().Add(redis);
            }
            else
            {
              var basketItems =  JsonSerializer.Deserialize<CustomerBasket>(dataExist.Value);
              
                var basket = basketItems.Items.FirstOrDefault(x => x.VariationId == basketItem.VariationId);
                if(basket != null){
                    basket.Quantity += basketItem.Quantity;
                }else{
                   basketItems.Items.Add(basketItem);
                }
                 dataExist.Value = JsonSerializer.Serialize(basketItems);
                _unitOfWork.Repository<AppRedis>().Update(dataExist);
            }
            
            var created = await _unitOfWork.Complete();
            if (created < 1) return null;

            return await GetBasketAsync(basketid);
        }

        public async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
            //var data = await _database.StringGetAsync(basketId);
            AppRedisSpecification spec = new AppRedisSpecification(basketId);
            var data = await _unitOfWork.Repository<AppRedis>().GetEntityWithSpec(spec);
            if (data == null) return null;

            return JsonSerializer.Deserialize<CustomerBasket>(data.Value);

            //return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);
        }

        public async Task<CustomerBasket> ReduceCart(string basketid, long variationId)
        {
            AppRedisSpecification spec = new AppRedisSpecification(basketid);
           var dataExist = await _unitOfWork.Repository<AppRedis>().GetEntityWithSpec(spec);
           
               var basketItems =  JsonSerializer.Deserialize<CustomerBasket>(dataExist.Value);
               var basket = basketItems.Items.FirstOrDefault(x => x.VariationId == variationId);
               basket.Quantity -= 1;
               if(basket.Quantity < 1) basketItems.Items.Remove(basket);
               dataExist.Value = JsonSerializer.Serialize(basketItems);
                _unitOfWork.Repository<AppRedis>().Update(dataExist);

                var updated = await _unitOfWork.Complete();
           
            if (updated < 1) return null;

            return await GetBasketAsync(basketid);
           
        }
    }
}