using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using build9ja.api.Dto;
using build9ja.api.DtoUtility;
using build9ja.api.Helper;
using build9ja.core.Entities;
using build9ja.core.Interfaces;
using build9ja.core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace build9ja.api.Controllers
{
    
    public class VendorController : BaseApiController
    {
        private readonly IVendorService _vendorService;
        private readonly IMapper _mapper;
        public VendorController(IVendorService vendorService, IMapper mapper)
        {
            _vendorService = vendorService;
            _mapper = mapper;
        }

        //get paginated vendor list
        [HttpGet]
        public async Task<ActionResult<Pagination<VendorDto>>> Get([FromQuery] PaginationSpecification spec)
        {
            var vendors = await _vendorService.getVendors(spec);
            var vendorsCount = await _vendorService.getVendorsCount(spec);

            var vendorDto = _mapper.Map<IReadOnlyList<Vendor>, IReadOnlyList<VendorDto>>(vendors);
            return Ok(new Pagination<VendorDto>(spec.PageIndex, spec.PageSize, vendorsCount, vendorDto));

        }
        //create vendor
        [HttpPost]
        public async Task<ActionResult<VendorDto>> Post([FromBody]VendorDto model)
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
            //check Email exist
            var emailExist = await _vendorService.checkEmail(model.Email);
            if(emailExist) return Conflict(new ApiResponse(409, "Email Exist"));
            var sellerId = "VEN"+ DateTime.Now.ToString("ddMMyyyyHHmmss") ;
            model.SellerId = sellerId;
            var vendor = _mapper.Map<VendorDto, Vendor>(model);
            var created = await _vendorService.CreateVendor(vendor);
            if (created > 0) return Ok(model);
            return BadRequest(new ApiResponse(500, "Unable to create vendor  at this time"));
        }

        //update vendor details
        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(long id, [FromBody] VendorDto model)
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
            var vendor = _mapper.Map<VendorDto, Vendor>(model);
            if (await _vendorService.UpdateVendor(id,vendor) == "00") return Ok();
            return BadRequest(new ApiResponse(500, "Unable to update vendor at this time"));
        }
        
        //update vendor status
        [HttpPut("update/status/{id}")]
        public async Task<ActionResult> UpdateStatus(long id, [FromBody] VendorStatusDto model)
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
            if (await _vendorService.UpdateVendorStatus(id, model.Status) == "00") return Ok();
            return BadRequest(new ApiResponse(500, "Unable to update vendor status at this time"));
        }
        //get vendor info by  Id
        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VendorDto>> Get(long id)
        {
            var vendor = await _vendorService.getVendorById(id);
            VendorDto vendorDto = _mapper.Map<Vendor, VendorDto>(vendor);
            return vendorDto;
        }
        //get vendor info by vendor Id
        [HttpGet("vendorid/{id}")]
        public async Task<ActionResult<VendorDto>> ByVendorId(string id)
        {
            var vendor = await _vendorService.getVendorById(id);
            VendorDto vendorDto = _mapper.Map<Vendor, VendorDto>(vendor);
            return vendorDto;
        }

        //create bank info
        //create vendor
        [HttpPost("bankinfo/add")]
        public async Task<ActionResult<VendorBankInfoDto>> AddBankInfo([FromBody]VendorBankInfoDto model)
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
            //check Email exist
            var bankInfo = await _vendorService.getVendorById(model.SellerId);
            if(bankInfo != null) return Conflict(new ApiResponse(409, "Bank Infor Exist for Vendor"));
            
            var vendorBankInfo = _mapper.Map<VendorBankInfoDto, VendorBankInfo>(model);
            var created = await _vendorService.CreateBankInfo(vendorBankInfo);
            if (created > 0) return Ok(model);
            return BadRequest(new ApiResponse(500, "Unable to add bank information at this time"));
        }
        //update bank info
         [HttpPut("bankinfo/update/{id}")]
        public async Task<ActionResult> UpdateBankInfo(long id, [FromBody] VendorBankInfoDto model)
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
             var vendor = _mapper.Map<VendorBankInfoDto, VendorBankInfo>(model);
            if (await _vendorService.UpdateBankInfo(id, vendor) == "00") return Ok();
            return BadRequest(new ApiResponse(500, "Unable to update vendor status at this time"));
        }
        [HttpPut("bankinfo/vendor/{id}")]
        public async Task<ActionResult<VendorBankInfoDto>> GetBankInfo(string sellerId)
        {
            var bankInfo = await _vendorService.GetBankInfo(sellerId);
            if(bankInfo == null) return NoContent();
            var bankInfoDto = _mapper.Map<VendorBankInfo,VendorBankInfoDto>(bankInfo);
            return Ok(bankInfo);
        }
    }
}