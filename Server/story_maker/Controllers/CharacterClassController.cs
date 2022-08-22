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
    public class ClassController : ControllerBase
    {
        private readonly ICharacterClassService _classService;
        public ClassController(ICharacterClassService _classService)
        {
            this._classService = _classService;
        }
        [HttpGet("GetAllCharacterClasses")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterClassDto>>>> GetAllCharacterClasses()
        {
            return Ok(await _classService.GetAllCharacterClasses());
        }
        [HttpGet("GetCharacterClassById/{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterClassDto>>> GetCharacterClassById(int id)
        {
            return Ok(await _classService.GetCharacterClassById(id));
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterClassDto>>>> DeleteCharacterClassById(int id)
        {
            var response = await _classService.DeleteCharacterClassById(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterClassDto>>>> AddCharacterClass(AddCharacterClassDto newClass)
        {

            return Ok(await _classService.AddCharacterClass(newClass));
        }
        [HttpPut("UpdateCharacterClassById/{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterClassDto>>> UpdateCharacterClass(int id, UpdateCharacterClassDto updatedClass)
        {
            var response = await _classService.UpdateCharacterClass(id, updatedClass);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}