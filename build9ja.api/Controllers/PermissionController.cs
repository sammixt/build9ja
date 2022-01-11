using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using build9ja.api.Dto;
using build9ja.api.DtoUtility;
using build9ja.core.Entities;
using build9ja.core.Interfaces;
using build9ja.core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace build9ja.api.Controllers 
{
    
    public class PermissionController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PermissionController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        // GET: api/values
        [HttpGet]
        [Authorize(Roles ="Administrators")]
        public async Task<ActionResult<IReadOnlyList<PermissionDto>>> Get()
        {
            var permissions = await  _unitOfWork.Repository<Permission>().ListAllAsync();
            return Ok(permissions);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PermissionDto>> Get(long id)
        {
            PermissionSpecification spec = new PermissionSpecification(id);
            var permission = await _unitOfWork.Repository<Permission>().GetEntityWithSpec(spec);
            var permissionDto = _mapper.Map<Permission, PermissionDto>(permission);
            return Ok(permissionDto); 
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]PermissionDto model)
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
            var modelEntity = _mapper.Map<PermissionDto, Permission>(model);
            _unitOfWork.Repository<Permission>().Add(modelEntity);
            await _unitOfWork.Complete();
            return Ok();
        }

        // PUT api/values/5
        [HttpPut()]
        public async Task<ActionResult> Put([FromBody] PermissionDto model)
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

            PermissionSpecification spec = new PermissionSpecification(model.Id);
            var permission = await _unitOfWork.Repository<Permission>().GetEntityWithSpec(spec);
            if (permission == null) return BadRequest(new ApiResponse(500, "Permission does not exist"));

            permission.PermissionName = model.PermissionName;
            permission.Status = model.Status;
            _unitOfWork.Repository<Permission>().Update(permission);
            await _unitOfWork.Complete();
            return Ok();
        }

    }
}

