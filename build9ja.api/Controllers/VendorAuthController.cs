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
using Microsoft.AspNetCore.Mvc;

namespace build9ja.api.Controllers
{
    public class VendorAuthController : BaseApiController
    {
       private readonly IVendorService _vendorService;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public VendorAuthController(IVendorService vendorService, IMapper mapper, ITokenService tokenService)
        {
            _vendorService = vendorService;
            _mapper = mapper;
            _tokenService = tokenService;
        }
       

        [HttpPost("login")]
        public async Task<ActionResult<LoggedInDto>> Login([FromBody] LoginDto model)
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

            var user = await _vendorService.getVendorCredentialByUserName(model.UserName);
            if (user == null) return BadRequest(new ApiResponse(400, "Invalid Username or password"));

            var vendor = await _vendorService.getVendorById(user.VendorId);
            if(!vendor.Status.Equals("Active")) return BadRequest(new ApiResponse(400, "Inactive User"));

            if (!PasswordUtility.VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest(new ApiResponse(400,"Invalid Username or password"));
            }
            string token =  _tokenService.CreateToken(user.VendorId, model.UserName, user.Permissions);
            LoggedInDto loggedInDto = new LoggedInDto(token, user.UserName, user.Permissions, $"{vendor.FirstName} {vendor.LastName}");
            //Add to event Log
            return Ok(loggedInDto);
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] LoginDto model)
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
            if (created > 0) return Ok();
            return BadRequest(new ApiResponse(500, "Unable to create vendor credential at this time"));

        }

       
    }
}