using System;
using Microsoft.AspNetCore.Mvc;
using story_maker.Models;
using story_maker.Dtos;
using story_maker.Services;
using story_maker.Services.Interfaces;

namespace story_maker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TraitController : ControllerBase
    {
        private readonly ITraitService _traitService;
        public TraitController(ITraitService _traitService)
        {
            this._traitService = _traitService;
        }
        [HttpGet("GetAllTraits")]
        public async Task<ActionResult<ServiceResponse<List<GetTraitDto>>>> GetAllTraits()
        {
            return Ok(await _traitService.GetAllTraits());
        }
        [HttpGet("GetTraitById/{id}")]
        public async Task<ActionResult<ServiceResponse<GetTraitDto>>> GetTraitById(int id)
        {
            return Ok(await _traitService.GetTraitById(id));
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetTraitDto>>>> DeleteTraitById(int id)
        {
            var response = await _traitService.DeleteTraitById(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetTraitDto>>>> AddTrait(AddTraitDto newTrait)
        {

            return Ok(await _traitService.AddTrait(newTrait));
        }
        [HttpPut("UpdateTraitById/{id}")]
        public async Task<ActionResult<ServiceResponse<GetTraitDto>>> UpdateTraitById(int id, UpdateTraitDto updatedTrait)
        {
            var response = await _traitService.UpdateTrait(id, updatedTrait);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}