using System;
using story_maker.Dtos;
using story_maker.Models;
namespace story_maker.Services.Interfaces
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters();
        Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharactersByClass(int classId);
        Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharactersByTraits(int[] traitsIds);
        Task<ServiceResponse<GetCharacterDto>> AddCharacter(AddCharacterDto newCharacter);
        Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id);
        Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(int id, UpdateCharacterDto updatedCharacter);
        Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacterById(int id);
    }

}