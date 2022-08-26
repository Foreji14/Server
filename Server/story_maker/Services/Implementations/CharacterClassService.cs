using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using story_maker.Dtos;
using story_maker.Models;
using story_maker.Services.Interfaces;

namespace story_maker.Services.Implementations{

    public class CharacterClassService : ICharacterClassService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CharacterClassService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<GetCharacterClassDto>> AddCharacterClass(AddCharacterClassDto newCharacterClass)
        {
            var res = new ServiceResponse<GetCharacterClassDto>();
            try{

            }catch(Exception ex){
                res.Message = ex.Message;
                res.Success = false;
            }
            return res;
        }

        public async Task<ServiceResponse<List<GetCharacterClassDto>>> DeleteCharacterClassById(int id)
        {
            var res = new ServiceResponse<List<GetCharacterClassDto>>();
            try{

            }catch(Exception ex){
                res.Message = ex.Message;
                res.Success = false;
            }
            return res;
        }

        public async Task<ServiceResponse<List<GetCharacterClassDto>>> GetAllCharacterClasses()
        {
            var res = new ServiceResponse<List<GetCharacterClassDto>>();
            try{
                res.Data = await _context.CharacterClasses
                    .Select(x=>_mapper.Map<GetCharacterClassDto>(x))
                    .ToListAsync();
            }catch(Exception ex){
                res.Message = ex.Message;
                res.Success = false;
            }
            return res;
        }

        public async Task<ServiceResponse<GetCharacterClassDto>> GetCharacterClassById(int id)
        {
            var res = new ServiceResponse<GetCharacterClassDto>();
            try{
                res.Data = await _context.CharacterClasses
                    .Select(x=>_mapper.Map<GetCharacterClassDto>(x))
                    .FirstOrDefaultAsync(x=>x.CharacterClassId == id);
            }catch(Exception ex){
                res.Message = ex.Message;
                res.Success = false;
            }
            return res;
        }

        public async Task<ServiceResponse<GetCharacterClassDto>> UpdateCharacterClass(int id, UpdateCharacterClassDto updatedCharacterClass)
        {
            var res = new ServiceResponse<GetCharacterClassDto>();
            try{
                var newCharacterClass = await _context.CharacterClasses
                    .FirstOrDefaultAsync(x=>x.CharacterClassId == id);
                if(newCharacterClass != null){
                    newCharacterClass.Name = updatedCharacterClass.Name;

                    await _context.SaveChangesAsync();

                    res.Data = _mapper.Map<GetCharacterClassDto>(newCharacterClass);
                }
                else{
                    res.Message = "Character Class not found!";
                    res.Success = false;
                }
            }catch(Exception ex){
                res.Message = ex.Message;
                res.Success = false;
            }
            return res;
        }
    }
}