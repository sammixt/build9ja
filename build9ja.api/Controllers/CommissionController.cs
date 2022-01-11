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
using Microsoft.AspNetCore.Mvc;

namespace build9ja.api.Controllers
{
    
    public class CommissionController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CommissionController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CommissionDto>>> Get()
        {

            var commisions = await _unitOfWork.Repository<Commission>().ListAllAsync();
            var commissionsDto = _mapper.Map<IReadOnlyList<Commission>, IReadOnlyList<CommissionDto>>(commisions);
            return Ok(commissionsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CommissionDto>> Get(long id)
        {
            CommissionSpecification spec = new CommissionSpecification(id);
            var commisions = await _unitOfWork.Repository<Commission>().GetEntityWithSpec(spec);
            var commissionsDto = _mapper.Map<Commission, CommissionDto>(commisions);
            return Ok(commissionsDto);
        }

        [HttpGet("type/{id}")]
        public async Task<ActionResult<CommissionDto>> GetByType(string id)
        {
            CommissionSpecification spec = new CommissionSpecification(id);
            var commisions = await _unitOfWork.Repository<Commission>().GetEntityWithSpec(spec);
            var commissionsDto = _mapper.Map<Commission, CommissionDto>(commisions);
            return Ok(commissionsDto);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CommissionDto model)
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
            model.Status = "Active";
            var commission = _mapper.Map<CommissionDto, Commission>(model);
            //check if commission exist
            CommissionSpecification spec = new CommissionSpecification(model.CommissionType);
            var commisions = await _unitOfWork.Repository<Commission>().GetEntityWithSpec(spec);
            if(commisions != null) return Conflict(new ApiResponse(500, "Commission already exist"));

            _unitOfWork.Repository<Commission>().Add(commission);
           int created = await _unitOfWork.Complete();
           if(created > 0) return Ok();

           return BadRequest(new ApiResponse(500, "Unable to create commission at this time"));
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] CommissionDto model)
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
           if(string.IsNullOrEmpty(model.Status)) return BadRequest(new ApiResponse(500, "Status is required"));
            
            CommissionSpecification spec = new CommissionSpecification(model.Id);
            var commisions = await _unitOfWork.Repository<Commission>().GetEntityWithSpec(spec);
            if(commisions == null) return Conflict(new ApiResponse(500, "Commission doesn't exist"));
            commisions.CommissionPercentage = model.CommissionPercentage != 0 ? model.CommissionPercentage:commisions.CommissionPercentage;
            commisions.CommissionType = model.CommissionType?? commisions.CommissionType;
            commisions.Status = model.Status??commisions.Status;
            _unitOfWork.Repository<Commission>().Update(commisions);
           int created = await _unitOfWork.Complete();
           if(created > 0) return Ok();

           return BadRequest(new ApiResponse(500, "Unable to update commission at this time"));
        }
        
    }
}