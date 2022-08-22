using System;
using story_maker.Dtos;
using story_maker.Models;
namespace story_maker.Services.Interfaces
{
    public interface ITraitService
    {
        Task<ServiceResponse<List<GetTraitDto>>> GetAllTraits();
        Task<ServiceResponse<GetTraitDto>> AddTrait(AddTraitDto newTrait);
        Task<ServiceResponse<GetTraitDto>> GetTraitById(int id);
        Task<ServiceResponse<GetTraitDto>> UpdateTrait(int id, UpdateTraitDto updatedTrait);
        Task<ServiceResponse<List<GetTraitDto>>> DeleteTraitById(int id);
    }

}