using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using build9ja.client.Dto;
using build9ja.client.Dto.Products;
using build9ja.client.Extensions;
using build9ja.core.Entities;
using build9ja.core.Entities.Identity;
using build9ja.core.Interfaces;
using build9ja.core.Specifications;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace build9ja.client.Controllers
{
    public class ShopController : Controller
    {
        private readonly ILogger<ShopController> _logger;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
         private readonly IBasketService _basketService;
         private readonly IDeliveryMethodService _deliveryMethod;
         private readonly UserManager<AppUser> _userManager;


        public ShopController(ILogger<ShopController> logger,ICategoryService categoryService,IMapper mapper,
        IProductService productService, IBasketService basketService,UserManager<AppUser> userManager,IDeliveryMethodService deliveryMethod)
        {
            _logger = logger;
            _categoryService = categoryService;
            _mapper = mapper;
            _productService = productService;
            _basketService = basketService;
            _userManager = userManager;
            _deliveryMethod = deliveryMethod;
        }

        [Route("Shop")]
        [Route("Shop/Index")]
        [Route("Shop/Index/{category}")]
        public async Task<ActionResult> Index(string category, [FromQuery]PaginationSpecification spec)
        {
            ShopModel model = new ShopModel();
            var categories = await _categoryService.GetCategoryByParentId(0,false);
            var categoriesDto = _mapper.Map<List<Category>, List<CategoryAndSubDto>>(categories);
            foreach(var categoryDto in categoriesDto){
               var categoriesByPId = await _categoryService.GetCategoryByParentId(categoryDto.Id,false);
                var cateListDto =  _mapper.Map<List<Category>, List<CategoryListDto>>(categoriesByPId);
                categoryDto.SubCategories = cateListDto;
            }
           
            var totalItems = await _productService.GetProductCount(category,spec);
            var products = await _productService.GetProductByCategory(category,spec);
            var productDto = _mapper.Map<List<Product>,List<ProductDto>>(products);
            model.PaginatedProducts = new Pagination(spec.PageIndex, spec.PageSize, totalItems, productDto);
            return View(model);
        }

        public async Task<ActionResult> Product(string pid)
        {
            ProductExtDto productDto = new ProductExtDto();
            var product = await _productService.GetProductByProductId(pid);
            productDto = _mapper.Map<Product,ProductExtDto>(product);
            return View(productDto);
        }

        public async Task<ActionResult> Cart()
        {
            string cookieValueFromReq = Request.Cookies["_guest_id"];  
            if(cookieValueFromReq != null){
                var output = await _basketService.GetBasketAsync(cookieValueFromReq);
                CustomerBasketDto outputDto = _mapper.Map<CustomerBasket,CustomerBasketDto>(output);
                return View(outputDto);
            }
            return View();
        }

        public async Task<ActionResult> Checkout()
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if(user == null)
                return RedirectToAction("Login","Home",new {returnUrl = "Shop/Checkout"});
            
            CheckoutShippingAddress address = new CheckoutShippingAddress();
            address.FirstName = user.FirstName;
            address.LastName = user.LastName;
            address.City = user.Address?.City;
            address.State = user.Address?.State;
            address.Street = user.Address?.Street;
            address.PhoneNumber = user.PhoneNumber;
            address.ZipCode = user.Address?.ZipCode;
            string cookieValueFromReq = Request.Cookies["_guest_id"];  
            if(cookieValueFromReq != null){
                var output = await _basketService.GetBasketAsync(cookieValueFromReq);
                address.Basket = _mapper.Map<CustomerBasket,CustomerBasketDto>(output);
            }
            if(!string.IsNullOrEmpty(address.State)){
                var delivery = await _deliveryMethod.getDeliveryByStateAndLga(address.State,address.City);
                address.ShippingCost = delivery.Price;
            }

            address.SubTotal = address.Basket.Items.Sum(x => (x.Price * x.Quantity));
                
            return View(address);
        }

        public async Task<ActionResult> GetShipping(string state, string city){
             string cookieValueFromReq = Request.Cookies["_guest_id"];  
             CustomerBasketDto Basket = new CustomerBasketDto();
             decimal ShippingCost = 0;
             decimal Total = 0;
            if(cookieValueFromReq != null){
                var output = await _basketService.GetBasketAsync(cookieValueFromReq);
                Basket = _mapper.Map<CustomerBasket,CustomerBasketDto>(output);
                var delivery = await _deliveryMethod.getDeliveryByStateAndLga(state,city);
                ShippingCost = delivery.Price;
            }
            decimal SubTotal = Basket.Items.Sum(x => (x.Price * x.Quantity));
            Total = ShippingCost + SubTotal;

            return Ok(new{ total = Total, shippingCost = ShippingCost, subTotal = SubTotal });

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}