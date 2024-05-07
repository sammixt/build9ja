using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
 
    public class PeopleController : Controller
    {
        private readonly ILogger<PeopleController> _logger;

        private readonly IStaffService _staffService;
        private readonly IVendorService _vendorService;
        private readonly IMapper _mapper;
        public PeopleController(ILogger<PeopleController> logger,IStaffService staffService,
         IMapper mapper, IVendorService vendorService)
        {
             _staffService = staffService;
            _mapper = mapper;
            _logger = logger;
            _vendorService = vendorService;
        }
        #region admin
        public async Task<ActionResult> Admin()
        {
            var staff = await _staffService.getStaffs();
            IEnumerable<StaffDto> staffDto = _mapper.Map<IReadOnlyList<Staff>, IReadOnlyList<StaffDto>>(staff);
            return View(staffDto);
        }

        [HttpPost]
        public async Task<ActionResult> AddUser([FromBody]StaffDto model)
        {
            try
            {
                if(!ModelState.IsValid){
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
                if(!string.IsNullOrEmpty(model.DateOfBirthString)) model.DateOfBirth = 
                DateTime.SpecifyKind(DateTime.ParseExact(model.DateOfBirthString, "yyyy-MM-dd", CultureInfo.InvariantCulture),DateTimeKind.Utc);
            var emailExist = await _staffService.checkEmail(model.EmailAddress);
            if(emailExist) return Conflict(new ApiResponse(409, "Email Exist"));
            Guid guid = Guid.NewGuid(); 
            model.StaffId = guid.ToString("N");
            var staff = _mapper.Map<StaffDto, Staff>(model);
            var created = await _staffService.createStaff(staff);
            if (created > 0) return Ok(new ApiResponse(200,"User successfully created"));
            return BadRequest(new ApiResponse(500, "Unable to create user at this time"));
            }
            catch (System.Exception e)
            {
                _logger.LogError("AddUser",e);
            }
            return StatusCode(StatusCodes.Status500InternalServerError,new ApiResponse(500, "Unable to create user at this time"));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateStaff([FromBody] StaffDto model)
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
            if(!string.IsNullOrEmpty(model.DateOfBirthString)) model.DateOfBirth = 
                DateTime.SpecifyKind(DateTime.ParseExact(model.DateOfBirthString, "yyyy-MM-dd", CultureInfo.InvariantCulture),DateTimeKind.Utc);
            var staff = _mapper.Map<StaffDto, Staff>(model);
            if (await _staffService.updateStaff(model.StaffId, staff) == "00") return Ok(new ApiResponse(200,"Staff updated"));
            return BadRequest(new ApiResponse(500, "Unable to update user at this time"));
            }
            catch (System.Exception e)
            {
                _logger.LogError("UpdateStaff",e);
            }
            return StatusCode(StatusCodes.Status500InternalServerError,new ApiResponse(500, "Unable to update user at this time"));

        }
        public async Task<ActionResult<StaffDto>> GetStaff([FromQuery]string staffId)
        {
            var staff = await _staffService.getStaffByStaffId(staffId);
            StaffDto staffDto = _mapper.Map<Staff, StaffDto>(staff);
            return staffDto;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAdminLogin([FromBody] LoginDto model)
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
                    if (string.IsNullOrEmpty(model.MemberId)) return BadRequest(new ApiResponse(400, "Staff Id is required"));
                    if (string.IsNullOrEmpty(model.Permission)) return BadRequest(new ApiResponse(400, "Permission is required"));
                    //check if staff Id exist
                    var staffId = await _staffService.getStaffByStaffId(model.MemberId);
                    if(staffId == null) return BadRequest(new ApiResponse(400, "Staff does not Exist"));
                    //check if username exist
                    var loginCredentialExist = await _staffService.checkStaffHasLoginCredential(model.MemberId);
                    if (loginCredentialExist) return Conflict(new ApiResponse(409, "Staff has login credential"));
                    var usernameExist = await  _staffService.checkUserName(model.UserName);
                    if (usernameExist) return Conflict(new ApiResponse(409, "Username Exist"));


                    Guid guid = Guid.NewGuid();
                    string resetCypher = guid.ToString("N");

                    PasswordUtility.CreatePasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);
                    StaffCredential staffCredential = new StaffCredential();
                    staffCredential.UserName = model.UserName;
                    staffCredential.Permissions = model.Permission;
                    staffCredential.StaffId = model.MemberId;
                    staffCredential.PasswordHash = passwordHash;
                    staffCredential.PasswordSalt = passwordSalt;
                    staffCredential.PasswordResetCypher = resetCypher;
                    int created = await _staffService.createStaffCredential(staffCredential);
                    if (created > 0) return Ok(new ApiResponse(200,"Login credential created"));
                    return BadRequest(new ApiResponse(500, "Unable to create user credential at this time"));
            }
            catch (System.Exception e)
            {
                 _logger.LogError("CreateAdminLogin",e);
            }

            return StatusCode(StatusCodes.Status500InternalServerError,new ApiResponse(500, "Unable to create login at this time"));

        }
        
        #endregion

        #region vendor
        public async Task<ActionResult> Vendor()
        {
            return View();
        }

            [HttpPost]
        public async Task<ActionResult<VendorDto>> AddVendor([FromBody]VendorDto model)
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
                    //check Email exist
                    var emailExist = await _vendorService.checkEmail(model.Email);
                    if(emailExist) return Conflict(new ApiResponse(409, "Email Exist"));
                    var sellerId = "VEN"+ DateTime.Now.ToString("ddMMyyyyHHmmss") ;
                    model.SellerId = sellerId;
                    var vendor = _mapper.Map<VendorDto, Vendor>(model);
                    var created = await _vendorService.CreateVendor(vendor);
                    if (created > 0) return Ok(new ApiResponse(200,"Vendor Created"));
                    return BadRequest(new ApiResponse(500, "Unable to create vendor  at this time"));
            }catch (System.Exception e)
            {
                 _logger.LogError("AddVendor",e);
            }

            return StatusCode(StatusCodes.Status500InternalServerError,new ApiResponse(500, "Unable to add vendor at this time"));

        }

        [HttpPut]
        public async Task<ActionResult> UpdateVendor([FromBody] VendorDto model)
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
                var vendor = _mapper.Map<VendorDto, Vendor>(model);
                if (await _vendorService.UpdateVendor(model.SellerId,vendor) == "00") return Ok(new ApiResponse(200,"Vendor Updated"));
                return BadRequest(new ApiResponse(500, "Unable to update vendor at this time"));
            }
            catch (System.Exception e)
            {
                 _logger.LogError("UpdateVendor",e);
            }

            return StatusCode(StatusCodes.Status500InternalServerError,new ApiResponse(500, "Unable to update vendor at this time"));

        }

        public async Task<ActionResult<VendorDto>> GetVendorById(string id)
        {
            var vendor = await _vendorService.getVendorById(id);
            VendorDto vendorDto = _mapper.Map<Vendor, VendorDto>(vendor);
            return vendorDto;
        }

        public async Task<ActionResult<List<VendorDto>>> GetAllVendors()
        {
            var vendor = await _vendorService.getVendors();
            List<VendorDto> vendorDto = (List<VendorDto>) _mapper.Map<IReadOnlyList<Vendor>, IReadOnlyList<VendorDto>>(vendor);
            return vendorDto;
        }

        [HttpPost]
        public async Task<ActionResult> GetVendors()
        {
            var DataTableRequest = Request.GetDataTableRequestForm();
            var drRequest = _mapper.Map<DataTableRequestDto,DataTableRequestSpecification>(DataTableRequest);
            int count = await _vendorService.getCount();
            var resultSet = await _vendorService.getVendorsDataTable(drRequest);
            var results = _mapper.Map<IEnumerable<Vendor>,IEnumerable<VendorDto>>(resultSet);
            var output = new DataTableDto<VendorDto> {  
                    Draw = Convert.ToInt32(DataTableRequest.Draw),  
                    Data = results,  
                    RecordsFiltered = count,  
                    RecordsTotal = count  
                };
            return Ok(output);
        }

        [HttpPost]
        public async Task<ActionResult> CreateVendorLogin([FromBody] LoginDto model)
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
            if (string.IsNullOrEmpty(model.MemberId)) return BadRequest(new ApiResponse(400, "vendor Id is required"));
            //if (string.IsNullOrEmpty(model.Permission)) return BadRequest(new ApiResponse(400, "Permission is required"));
            //check if vendor Id exist
            var vendorId = await _vendorService.getVendorById(model.MemberId);
            if(vendorId == null) return BadRequest(new ApiResponse(400, "vendor does not Exist"));
            //check if username exist
            var usernameExist = await  _vendorService.checkUserName(model.UserName);
            if (usernameExist) return Conflict(new ApiResponse(409, "Username Exist"));

            Guid guid = Guid.NewGuid();
            string resetCypher = guid.ToString("N");

            PasswordUtility.CreatePasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);
            VendorCredential vendorCredential = new VendorCredential();
            vendorCredential.UserName = model.UserName;
            vendorCredential.Permissions = model.Permission;
            vendorCredential.VendorId = model.MemberId;
            vendorCredential.PasswordHash = passwordHash;
            vendorCredential.PasswordSalt = passwordSalt;
            vendorCredential.PasswordResetCypher = resetCypher;
            int created = await _vendorService.createVendorCredential(vendorCredential);
            if (created > 0) return Ok(new ApiResponse(200,"Successful"));
            return BadRequest(new ApiResponse(500, "Unable to create vendor credential at this time"));

        }
        [HttpPatch]
        public async Task<ActionResult> AddBankVendorBankInfo([FromBody]VendorBankInfoDto vendorBankInfoDto)
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
                var vendorBankInfo = _mapper.Map<VendorBankInfoDto, VendorBankInfo>(vendorBankInfoDto);
                if(await _vendorService.AddOrUpdateVendorBankInfo(vendorBankInfo) > 0) return Ok(new ApiResponse(200,"Successful"));
            } catch (System.Exception e)
            {
                 _logger.LogError("AddBankVendorBankInfo",e);
            }

            return StatusCode(StatusCodes.Status500InternalServerError,new ApiResponse(500, "Unable to update vendor bank info at this time"));
        }

        public async Task<ActionResult> GetVendorBankInfoById(string vid)
        {
            try{
                var bankInfo = await _vendorService.GetBankInfo(vid);
                var bankInfoDto = _mapper.Map<VendorBankInfo,VendorBankInfoDto>(bankInfo);
                return Ok(bankInfoDto);
            }catch (System.Exception e)
            {
                 _logger.LogError("GetVendorBankInfoById",e);
            }

            return StatusCode(StatusCodes.Status500InternalServerError,new ApiResponse(500, "Unable to get vendor bank info at this time"));
        }
        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}