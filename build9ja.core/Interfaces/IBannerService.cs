using build9ja.core.Entities;

namespace build9ja.core.Interfaces
{
    public interface IBannerService
    {
        Task<int> AddOrUpdateBanner(Banner model);
        Task<Banner> GetBanners();
    }
}