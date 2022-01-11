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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace build9ja.api.Controllers
{
    
    public class StaffController : BaseApiController
    {
        private readonly IStaffService _staffService;
        private readonly IMapper _mapper;
        public StaffController(IStaffService staffService, IMapper mapper)
        {
            _staffService = staffService;
            _mapper = mapper;
        }
        // GET: api/values
        [HttpGet]
        public async Task<ActionResult<Pagination<StaffDto>>> Get([FromQuery] PaginationSpecification spec)
        {
            var staffs = await  _staffService.getStaffs(spec);
            var staffCount = await _staffService.getStaffsCount(spec);

            var staffDto = _mapper.Map<IReadOnlyList<Staff>, IReadOnlyList<StaffDto>>(staffs);
            return Ok(new Pagination<StaffDto>(spec.PageIndex, spec.PageSize, staffCount, staffDto));

        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StaffDto>> Get(string staffId)
        {
            var staff = await _staffService.getStaffByStaffId(staffId);
            StaffDto staffDto = _mapper.Map<Staff, StaffDto>(staff);
            return staffDto;
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]StaffDto model)
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
            var emailExist = await _staffService.checkEmail(model.EmailAddress);
            if(emailExist) return Conflict(new ApiResponse(409, "Email Exist"));
            Guid guid = Guid.NewGuid(); 
            model.StaffId = guid.ToString("N");
            var staff = _mapper.Map<StaffDto, Staff>(model);
            var created = await _staffService.createStaff(staff);
            if (created > 0) return Ok();
            return BadRequest(new ApiResponse(500, "Unable to create user at this time"));
        }

        // PUT api/values/5
        [HttpPut("{staffId}")]
        public async Task<ActionResult> Put(string staffId, [FromBody] StaffDto model)
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
            var staff = _mapper.Map<StaffDto, Staff>(model);
            if (await _staffService.updateStaff(staffId, staff) == "00") return Ok();
            return BadRequest(new ApiResponse(500, "Unable to update user at this time"));
        }

        // DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}

