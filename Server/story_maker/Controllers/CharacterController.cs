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
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;
        public CharacterController(ICharacterService _characterService)
        {
            this._characterService = _characterService;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> GetAllCharacters()
        {
            return Ok(await _characterService.GetAllCharacters());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetCharacterById(int id)
        {
            return Ok(await _characterService.GetCharacterById(id));
        }
        [HttpGet("GetCharacterByClass/{classId}}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetCharacterByClass(int classId)
        {
            return Ok(await _characterService.GetAllCharactersByClass(classId));

        }
        [HttpGet("GetCharacterByTraits")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetCharacterByTrait(int[] trait)
        {
            return Ok(await _characterService.GetAllCharactersByTraits(trait));

        }
        [HttpDelete("{id}")]
         public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> DeleteCharacterById(int id)
        {
            var response = await _characterService.DeleteCharacterById(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> AddCharacter(AddCharacterDto newCharacter)
        {

            return Ok(await _characterService.AddCharacter(newCharacter));
        }
        [HttpPut("UpdateCharacterById/{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> UpdateCharacter(int id, UpdateCharacterDto updatedCharacter)
        {
            var response = await _characterService.UpdateCharacter(id, updatedCharacter);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

    }
}