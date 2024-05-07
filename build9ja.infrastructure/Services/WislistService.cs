using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using build9ja.core.Entities;
using build9ja.core.Interfaces;
using build9ja.core.Specifications;

namespace build9ja.infrastructure.Services
{

    public class WislistService : IWislistService
    {
        private readonly IUnitOfWork _unitOfWork;
        public WislistService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> CreateWishlist(WishList wishlist)
        {
             WishListSpecification spec = new WishListSpecification(wishlist.user,wishlist.ProductId);
            var hasproduct = await _unitOfWork.Repository<WishList>().GetEntityWithSpec(spec);
            if(hasproduct != null) return 409;
            _unitOfWork.Repository<WishList>().Add(wishlist);
            int created = await _unitOfWork.Complete();
            return created;
        }

        public async Task<int> DeleteWishlist(WishList wishlist)
        {
             WishListSpecification spec = new WishListSpecification(wishlist.ProductId);
            var hasproduct = await _unitOfWork.Repository<WishList>().GetEntityWithSpec(spec);
            _unitOfWork.Repository<WishList>().Delete(hasproduct);
            int deleted = await _unitOfWork.Complete();
            return deleted;
        }

        public async Task<int> countWishlist(string email)
        {
            WishListSpecification spec = new WishListSpecification(email);
            int count = await _unitOfWork.Repository<WishList>().CountAsync(spec);
            return count;
        }

        public async Task<List<WishList>> GetWishlistPaginated(string email,PaginationSpecification spec)
        {
            
            WishListSpecification wSpec = new WishListSpecification(email,spec);
            var wishlist = await _unitOfWork.Repository<WishList>().ListAsync(wSpec);
            return (List<WishList>)wishlist; 
        }
    }
}