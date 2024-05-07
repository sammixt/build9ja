using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using build9ja.client.Dto;
using build9ja.core.Entities;
using build9ja.core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace build9ja.client.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ILogger<CartController> _logger;
        private readonly IMapper _mapper;
        private readonly IBasketService _basketService;

        public CartController(ILogger<CartController> logger,IMapper mapper, IBasketService basketService)
        {
            _logger = logger;
            _mapper = mapper;
            _basketService = basketService;
        }

        [HttpPost]
        [Route("addtocart")]
        //[ProducesResponseType(typeof(CustomerBasketDto), 200)]
        public async Task<ActionResult> AddToCart([FromBody]BasketItemDto basketItem)
        {
            try{
                string cookieValueFromReq = Request.Cookies["_guest_id"];  
                if(cookieValueFromReq == null){
                     cookieValueFromReq = Guid.NewGuid().ToString("N");
                }
                BasketItem basket = _mapper.Map<BasketItemDto,BasketItem>(basketItem);
                var output = await _basketService.StoreCart(cookieValueFromReq,basket);
                CustomerBasketDto outputDto = _mapper.Map<CustomerBasket,CustomerBasketDto>(output);
                Set("_guest_id", cookieValueFromReq, 10);
                return Ok(outputDto);
            }catch(Exception e){
                _logger.LogError("AddToCart", e);
            }
            return new BadRequestObjectResult(new {error = "an error occures"});
        }

        [HttpPost]
        [Route("reducecart")]
        [ProducesResponseType(typeof(CustomerBasketDto), 200)]
        public async Task<ActionResult> ReduceCart(BasketItemDto basketItem)
        {
            try{
                string cookieValueFromReq = Request.Cookies["_guest_id"];  
                if(cookieValueFromReq != null){
                    var output = await _basketService.ReduceCart(cookieValueFromReq,basketItem.VariationId);
                    CustomerBasketDto outputDto = _mapper.Map<CustomerBasket,CustomerBasketDto>(output);
                    return Ok(outputDto);
                }
               
            }catch(Exception e){
                _logger.LogError("AddToCart", e);
            }
            return new BadRequestObjectResult(new {error = "an error occures"});
        }

        [HttpDelete]
        [ProducesResponseType(typeof(CustomerBasketDto), 200)]
        public async Task<ActionResult> DeleteCart(BasketItemDto basketItem)
        {
            try{
                string cookieValueFromReq = Request.Cookies["_guest_id"];  
                if(cookieValueFromReq != null){
                    var output = await _basketService.DeleteItemFromCart(cookieValueFromReq,basketItem.VariationId);
                    CustomerBasketDto outputDto = _mapper.Map<CustomerBasket,CustomerBasketDto>(output);
                    return Ok(outputDto);
                }
               
            }catch(Exception e){
                _logger.LogError("AddToCart", e);
            }
            return new BadRequestObjectResult(new {error = "an error occures"});
        }

        [HttpGet]
        public async Task<ActionResult> GetCart()
        {
            try{
                string cookieValueFromReq = Request.Cookies["_guest_id"];  
                if(cookieValueFromReq != null){
                    var output = await _basketService.GetBasketAsync(cookieValueFromReq);
                    CustomerBasketDto outputDto = _mapper.Map<CustomerBasket,CustomerBasketDto>(output);
                    return Ok(outputDto);
                }
            }catch(Exception e){
                _logger.LogError("AddToCart", e);
            }
            return new BadRequestObjectResult(new {error = "an error occures"});
        }

        [HttpGet]
        [Route("cartcount")]
        public async Task<ActionResult> CartCount()
        {
            string cookieValueFromReq = Request.Cookies["_guest_id"];  
            if(cookieValueFromReq == null) return Ok(new {count = 0});
            var output = await _basketService.GetBasketAsync(cookieValueFromReq);
            CustomerBasketDto outputDto = _mapper.Map<CustomerBasket,CustomerBasketDto>(output);
           int count =  outputDto.Items.Count();
           return Ok(new {count = count});
        }

         private void Set(string key, string value, int? expireTime)  
            {  
            CookieOptions option = new CookieOptions();  

            if (expireTime.HasValue)  
                    option.Expires = DateTime.Now.AddDays(expireTime.Value);  
            else  
                    option.Expires = DateTime.Now.AddDays(10);  
            
            Response.Cookies.Append(key, value, option);  
            } 
    }
}