using System;
using story_maker.Dtos;
using story_maker.Models;
namespace story_maker.Services.Interfaces
{
    public interface ICharacterClassService
    {
        Task<ServiceResponse<List<GetCharacterClassDto>>> GetAllCharacterClasses();
        Task<ServiceResponse<GetCharacterClassDto>> AddCharacterClass(AddCharacterClassDto newCharacterClass);
        Task<ServiceResponse<GetCharacterClassDto>> GetCharacterClassById(int id);
        Task<ServiceResponse<GetCharacterClassDto>> UpdateCharacterClass(int id, UpdateCharacterClassDto updatedCharacterClass);
        Task<ServiceResponse<List<GetCharacterClassDto>>> DeleteCharacterClassById(int id);
    }

}