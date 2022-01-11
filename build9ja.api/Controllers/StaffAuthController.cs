using AutoMapper;
using build9ja.api.Dto;
using build9ja.api.DtoUtility;
using build9ja.api.Helper;
using build9ja.core.Entities;
using build9ja.core.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace build9ja.api.Controllers
{

    public class StaffAuthController : BaseApiController
    {
        private readonly IStaffService _staffService;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public StaffAuthController(IStaffService staffService, IMapper mapper, ITokenService tokenService)
        {
            _staffService = staffService;
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

            var user = await _staffService.getStaffCredentialByUserName(model.UserName);
            if (user == null) return BadRequest(new ApiResponse(400, "Invalid Username or password"));

            var staff = await _staffService.getStaffByStaffId(user.StaffId);
            if(!staff.Status.Equals("Active")) return BadRequest(new ApiResponse(400, "Inactive User"));

            if (!PasswordUtility.VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Invalid Username or password");
            }
            string token =  _tokenService.CreateToken(user.StaffId, model.UserName, user.Permissions);
            LoggedInDto loggedInDto = new LoggedInDto(token, user.UserName, user.Permissions, $"{staff.FirstName} {staff.LastName}");
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
            if (string.IsNullOrEmpty(model.MemberId)) return BadRequest(new ApiResponse(400, "Staff Id is required"));
            if (string.IsNullOrEmpty(model.Permission)) return BadRequest(new ApiResponse(400, "Permission is required"));
            //check if staff Id exist
            var staffId = await _staffService.getStaffByStaffId(model.MemberId);
            if(staffId == null) return BadRequest(new ApiResponse(400, "Staff does not Exist"));
            //check if username exist
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
            if (created > 0) return Ok();
            return BadRequest(new ApiResponse(500, "Unable to create user credential at this time"));

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

