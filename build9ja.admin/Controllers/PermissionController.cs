using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using build9ja.admin.Dto;
using build9ja.admin.Responses;
using build9ja.core.Entities;
using build9ja.core.Interfaces;
using build9ja.core.Specifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace build9ja.admin.Controllers
{
    public class PermissionController : Controller
    {
        private readonly ILogger<PermissionController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PermissionController(ILogger<PermissionController> logger,IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index()
        {
            
             var permissions = await  _unitOfWork.Repository<Permission>().ListAllAsync();
            IEnumerable<PermissionDto> permissionDto = _mapper.Map<IReadOnlyList<Permission>,IReadOnlyList<PermissionDto>>(permissions);
            
            return View(permissionDto);
        }

        public async Task<ActionResult> GetPermissions()
        {
            
             var permissions = await  _unitOfWork.Repository<Permission>().ListAllAsync();
            IEnumerable<PermissionDto> permissionDto = _mapper.Map<IReadOnlyList<Permission>,IReadOnlyList<PermissionDto>>(permissions);
            
            return Ok(permissionDto);
        }

        [HttpPost]
        public async Task<ActionResult> Addpermission([FromBody]PermissionDto model)
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
            model.dateCreate = DateTime.UtcNow;
            model.CreatedBy = "Samuel Ezeala";
            var modelEntity = _mapper.Map<PermissionDto, Permission>(model);
            _unitOfWork.Repository<Permission>().Add(modelEntity);
            await _unitOfWork.Complete();
            return Ok(new ApiResponse(200,"Permission created"));
        }

        public async Task<ActionResult<PermissionDto>> GetPermission(long id)
        {
            PermissionSpecification spec = new PermissionSpecification(id);
            var permission = await _unitOfWork.Repository<Permission>().GetEntityWithSpec(spec);
            var permissionDto = _mapper.Map<Permission, PermissionDto>(permission);
            return Ok(permissionDto); 
        }

        [HttpPut]
        public async Task<ActionResult> EditPermission([FromBody] PermissionDto model)
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
            return Ok(new ApiResponse(200,"Permission Modified"));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}