using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using build9ja.core.Entities;
using build9ja.core.Interfaces;
using build9ja.core.Specifications;

namespace build9ja.infrastructure.Services
{

    public class BannerService : IBannerService
    {

        private readonly IUnitOfWork _unitOfWork;
        public BannerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Banner> GetBanners()
        {
            BannerSpecification spec = new BannerSpecification();
            var banner = await _unitOfWork.Repository<Banner>().GetEntityWithSpec(spec);
            return banner;
        }

        public async Task<int> AddOrUpdateBanner(Banner model){
            BannerSpecification spec = new BannerSpecification();
            var banner = await _unitOfWork.Repository<Banner>().GetEntityWithSpec(spec);
            if(banner == null){
                _unitOfWork.Repository<Banner>().Add(model);
            }else{
                banner.ImageOne = model.ImageOne ?? banner.ImageOne;
                banner.ImageTwo = model.ImageTwo ?? banner.ImageTwo;
                banner.ImageThree = model.ImageThree ?? banner.ImageThree;
                banner.ImageFour = model.ImageFour ?? banner.ImageFour;
                banner.TitleOne = model.TitleOne ?? banner.TitleOne;
                banner.TitleTwo = model.TitleTwo ?? banner.TitleTwo;
                banner.TitleThree = model.TitleThree ?? banner.TitleThree;
                banner.TitleFour = model.TitleFour ?? banner.TitleFour;
                banner.LinkOne = model.LinkOne ?? banner.LinkOne;
                banner.LinkTwo = model.LinkTwo ?? banner.LinkTwo;
                banner.LinkThree = model.LinkThree ?? banner.LinkThree;
                banner.LinkFour = model.LinkFour ?? banner.LinkFour;
                banner.SubTitleOne = model.SubTitleOne ?? banner.SubTitleOne;
                banner.SubTitleTwo = model.SubTitleTwo ?? banner.SubTitleTwo;
                banner.SubTitleThree = model.SubTitleThree ?? banner.SubTitleThree;
                banner.SubTitleFour = model.SubTitleFour ?? banner.SubTitleFour;
                banner.SubPageImage = model.SubPageImage ?? banner.SubPageImage;

                _unitOfWork.Repository<Banner>().Update(banner);
            }
            return await _unitOfWork.Complete();
        }
    }
}