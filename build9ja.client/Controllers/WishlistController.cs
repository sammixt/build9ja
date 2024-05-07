using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using build9ja.client.Dto;
using build9ja.core.Entities;
using build9ja.core.Interfaces;
using build9ja.core.Specifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace build9ja.client.Controllers
{
    public class WishlistController : Controller
    {
        private readonly ILogger<WishlistController> _logger;
        private readonly IWislistService _wishlistService;
        private readonly IProductService _productService;
        
        private readonly IMapper _mapper;

        public WishlistController(ILogger<WishlistController> logger, IWislistService wishlistService
        , IMapper mapper,IProductService productService)
        {
            _logger = logger;
            _wishlistService = wishlistService;
            _mapper = mapper;
            _productService = productService;
        }

        public async Task<IActionResult> Index([FromQuery]PaginationSpecification spec)
        {
             ShopModel model = new ShopModel();
            
            //var email = HttpContext.User.RetrieveEmailFromPrincipal();
           string email = "email";
            var totalItems = await _wishlistService.countWishlist(email);
            var wishlist = await _wishlistService.GetWishlistPaginated(email,spec);
            var wishlistDto = _mapper.Map<List<WishList>,List<WishListDto>>(wishlist);
            model.PaginatedWishlist = new PaginationWishlist(spec.PageIndex, spec.PageSize, totalItems, wishlistDto);
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> CreateWishList([FromBody] WishListDto wishList)
        {
            //var email = HttpContext.User.RetrieveEmailFromPrincipal();

            var mappedList = _mapper.Map<WishListDto, WishList>(wishList);
            mappedList.user = "email";
            mappedList.DateCreated = DateTime.UtcNow;

            var product = await _productService.GetProductByProductId(wishList.ProductIdString);
            mappedList.Product = product;
            mappedList.ProductId = product.Id;
            int result = await _wishlistService.CreateWishlist(mappedList);
            if (result == 1)
            {
                return Ok(new {code = 200, message = "Product added to wishlist"});
            }
            if(result == 409)
            {
                return Conflict(new {code = 409, message = "Product exist in wishlist"});
            }

            return BadRequest(new {code = 500, message ="An error occurred when adding Product to wishlist"});
        }

        [HttpDelete]
         public async Task<ActionResult<bool>> DeleteWishList([FromBody] WishListDto wishList)
        {
            //var email = HttpContext.User.RetrieveEmailFromPrincipal();

            var mappedList = _mapper.Map<WishListDto, WishList>(wishList);
            mappedList.user = "email";
            mappedList.DateCreated = DateTime.UtcNow;

            var product = await _productService.GetProductByProductId(wishList.ProductIdString);
            mappedList.Product = product;
            mappedList.ProductId = product.Id;
            int result = await _wishlistService.DeleteWishlist(mappedList);
            if (result == 1)
            {
                return Ok(new {code = 200, message = "Product removed from wishlist"});
            }

            return BadRequest(new {code = 500, message ="An error occurred when removing Product from wishlist"});
        }

        [HttpGet]
        public async Task<ActionResult> WishListCount()
        {
           //var email = HttpContext.User.RetrieveEmailFromPrincipal();
           string email = "email";
            var output = await _wishlistService.countWishlist(email);
           return Ok(new {count = output});
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}