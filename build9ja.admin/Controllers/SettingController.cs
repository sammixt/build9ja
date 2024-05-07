using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using build9ja.admin.Dto;
using build9ja.admin.Extensions;
using build9ja.admin.Helper;
using build9ja.admin.Responses;
using build9ja.core.Entities;
using build9ja.core.Interfaces;
using build9ja.core.Specifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace build9ja.admin.Controllers
{
    public class SettingController : Controller
    {
        private readonly ILogger<SettingController> _logger;
        private readonly ICommissionService _commissionService;
        private readonly IBannerService _bannerService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment webHostEnvironment;

        private readonly IDeliveryMethodService _deliveryMethodService;

        public SettingController(ILogger<SettingController> logger,ICommissionService commissionService,
        IMapper mapper, IBannerService bannerService,IWebHostEnvironment hostEnvironment,IDeliveryMethodService deliveryMethodService)
        {
            _logger = logger;
            _commissionService = commissionService;
            _bannerService = bannerService;
            _mapper = mapper;
            webHostEnvironment = hostEnvironment;
            _deliveryMethodService = deliveryMethodService;
        }

        #region commission
        public async Task<ActionResult> Commission()
        {
            var commissions = await _commissionService.GetCommissions();
            List<CommissionDto> commissionDto = _mapper.Map<List<Commission>,List<CommissionDto>>(commissions);
            return View(commissionDto);;
        }

        [HttpPost]
        public async Task<ActionResult> AddCommission([FromBody] CommissionDto commissionDto)
        {
            try{
                if (!ModelState.IsValid)
                {
                    var modelErrors = new List<string>();
                    foreach (var modelState in ModelState.Values)
                    {
                        foreach (var modelError in modelState.Errors)
                        {
                            modelErrors.Add(modelError.ErrorMessage);
                        }
                    }
                    return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = modelErrors });
                }
                var commission = _mapper.Map<CommissionDto, Commission>(commissionDto);
                var create = await  _commissionService.CreateCommission(commission);
                if(create > 0) return Ok(new ApiResponse(200,"Successful"));
            } catch (System.Exception e)
            {
                 _logger.LogError("AddCommission",e);
            }
            return StatusCode(StatusCodes.Status500InternalServerError,new ApiResponse(500, "Unable to add commission at this time"));
        }

         [HttpPut]
        public async Task<ActionResult> UpdateCommission([FromBody] CommissionDto commissionDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var modelErrors = new List<string>();
                    foreach (var modelState in ModelState.Values)
                    {
                        foreach (var modelError in modelState.Errors)
                        {
                            modelErrors.Add(modelError.ErrorMessage);
                        }
                    }
                    return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = modelErrors });
                }
                
                var commission = _mapper.Map<CommissionDto, Commission>(commissionDto);
                if (await _commissionService.UpdateCommission(commission)> 0) return Ok(new ApiResponse(200,"Successful"));
            }
             catch (System.Exception e)
            {
                 _logger.LogError("UpdateCommission",e);
            }
            return StatusCode(StatusCodes.Status500InternalServerError,new ApiResponse(500, "Unable to update category at this time"));
        }

        public async Task<ActionResult> GetCommissionById(long id)
        {
            var commissions = await _commissionService.GetCommissionsById(id);
            CommissionDto commissionDto = _mapper.Map<Commission,CommissionDto>(commissions);
            return Ok(commissionDto);;
        }

        #endregion

        #region banner

        public async Task<ActionResult> Banner()
        {
            var banner = await _bannerService.GetBanners();
            var mapped = _mapper.Map<Banner, BannerDto> (banner);
            return View(mapped);
        }

        [HttpPatch]
        public async Task<ActionResult> AddSlider([FromForm] BannerInputDto input)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var modelErrors = new List<string>();
                    foreach (var modelState in ModelState.Values)
                    {
                        modelErrors.AddRange(from modelError in modelState.Errors
                                             select modelError.ErrorMessage);
                    }
                    return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = modelErrors });
                }
                if (input.UploadImage == null) return BadRequest(new ApiResponse(400, "Provide Image"));
                 string fileName = Utility.ProcessUploadedFile(input.UploadImage,GetPathAndFilename());
                 BannerDto model = BindFileName(input.SliderTypes, input, fileName);
                 var banner = _mapper.Map<BannerDto,Banner>(model);
                if (await _bannerService.AddOrUpdateBanner(banner) > 0) return Ok(new ApiResponse(200,"Successful"));
            }
            catch (System.Exception e)
            {
                 _logger.LogError("AddSlider",e);
            }
            return StatusCode(StatusCodes.Status500InternalServerError,new ApiResponse(500, "Unable to Upload banner at this time"));
        }

        private BannerDto BindFileName(SliderTypes slider, BannerInputDto model, string file)
        {
            BannerDto bannerDto = new BannerDto();
            switch(slider)
            {

                case SliderTypes.BannerOne:
                    bannerDto.ImageOne = file;
                    bannerDto.SubTitleOne = model.SubTitle;
                    bannerDto.TitleOne = model.Title;
                    bannerDto.LinkOne = model.Link;
                break;
                case SliderTypes.BannerTwo:
                    bannerDto.ImageTwo = file;
                    bannerDto.SubTitleTwo = model.SubTitle;
                    bannerDto.TitleTwo = model.Title;
                    bannerDto.LinkTwo = model.Link;
                    break;
                case SliderTypes.BannerThree:
                    bannerDto.ImageThree = file;
                    bannerDto.SubTitleThree = model.SubTitle;
                    bannerDto.TitleThree = model.Title;
                    bannerDto.LinkThree = model.Link;
                    break;
                case SliderTypes.BannerFour:
                    bannerDto.ImageFour = file;
                    bannerDto.SubTitleFour = model.SubTitle;
                    bannerDto.TitleFour = model.Title;
                    bannerDto.LinkFour = model.Link;
                    break;
                case SliderTypes.SubPageBanner:
                    bannerDto.SubPageImage = file;
                    break;
                
            };

            return bannerDto;
        }
        private string GetPathAndFilename()
        {
            return @$"{this.webHostEnvironment.WebRootPath}/Uploads/Banner/";
        }

        #endregion

        #region shipping
        
        public async Task<ActionResult> Shipping()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> GetDeliveryMethod()
        {
           
            var DataTableRequest = Request.GetDataTableRequestForm();
            var drRequest = _mapper.Map<DataTableRequestDto,DataTableRequestSpecification>(DataTableRequest);
            int count = await _deliveryMethodService.getCount();
            var resultSet = await _deliveryMethodService.getDeliveryMehtodDataTable(drRequest);

            var results = _mapper.Map<List<DeliveryMethod>,List<DeliveryMethodDto>>(resultSet);
            
            
            var output = new DataTableDto<DeliveryMethodDto> {  
                    Draw = Convert.ToInt32(DataTableRequest.Draw),  
                    Data = results,  
                    RecordsFiltered = count,  
                    RecordsTotal = count  
                };
            return Ok(output);
        }

        [ProducesResponseType(typeof(DeliveryMethodDto), 200)]
        public async Task<ActionResult> GetDeliveryMethodById(string id)
        {
            var deliveryMethod = await _deliveryMethodService.getDeliveryMethodById(id);
            var deliveryMethodDto = _mapper.Map<DeliveryMethod,DeliveryMethodDto>(deliveryMethod);
            return Ok(deliveryMethodDto);
        }

        [HttpPost]
        public async Task<ActionResult> AddDeliveryMethod([FromForm]DeliveryMethodDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var modelErrors = new List<string>();
                    foreach (var modelState in ModelState.Values)
                    {
                        foreach (var modelError in modelState.Errors)
                        {
                            modelErrors.Add(modelError.ErrorMessage);
                        }
                    }
                    return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = modelErrors });
                }

                model.ShippingId = Guid.NewGuid().ToString("N");
                var delivery = _mapper.Map<DeliveryMethodDto, DeliveryMethod>(model);
                var create = await  _deliveryMethodService.Create(delivery);
                if(create > 0) return Ok(new ApiResponse(200,"Successful"));
                return StatusCode(StatusCodes.Status500InternalServerError,new ApiResponse(500, "An error has occured"));
            }
             catch (System.Exception e)
            {
                 _logger.LogError("AddBrand",e);
            }
            return StatusCode(StatusCodes.Status500InternalServerError,new ApiResponse(500, "Unable to add delivery method at this time"));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateDeliveryMethod([FromForm] DeliveryMethodDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var modelErrors = new List<string>();
                    foreach (var modelState in ModelState.Values)
                    {
                        foreach (var modelError in modelState.Errors)
                        {
                            modelErrors.Add(modelError.ErrorMessage);
                        }
                    }
                    return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = modelErrors });
                }
                
                var delivery = _mapper.Map<DeliveryMethodDto, DeliveryMethod>(model);
                if (await _deliveryMethodService.Update(delivery) == 404) return NotFound(new ApiResponse(404,"Delivery Method Not Found"));
                if (await _deliveryMethodService.Update(delivery) > 0) return Ok(new ApiResponse(200,"Successful"));
            }
             catch (System.Exception e)
            {
                 _logger.LogError("UpdateDeliveryMethod",e);
            }
            return StatusCode(StatusCodes.Status500InternalServerError,new ApiResponse(500, "Unable to update delivery method at this time"));
        }

        #endregion
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}