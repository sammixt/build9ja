using build9ja.core.Entities;
using build9ja.core.Specifications;

namespace build9ja.core.Interfaces
{
    public interface IWislistService
    {
        Task<int> CreateWishlist(WishList wishlist);
        Task<int> countWishlist(string email);
        Task<List<WishList>> GetWishlistPaginated(string email,PaginationSpecification spec);
         Task<int> DeleteWishlist(WishList wishlist);
    }
}