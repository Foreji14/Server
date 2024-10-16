using System;
using Microsoft.EntityFrameworkCore;
using story_maker.Dtos;
using story_maker.Models;
using story_maker.Services.Interfaces;
using AutoMapper;

namespace story_maker.Services.Implementations
{
    public class CharacterService : ICharacterService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CharacterService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<GetCharacterDto>> AddCharacter(AddCharacterDto newCharacter)
        {
            var res = new ServiceResponse<GetCharacterDto>();
            try
            {
                var character = new Character
                {
                    CharacterClassId = newCharacter.CharacterClassId,
                    Name = newCharacter.Name,
                    Story = newCharacter.Story,
                    Description = newCharacter.Description
                };


                await _context.Characters.AddAsync(character);
                await _context.SaveChangesAsync();

                var traits = await _context.Traits.Where(x => newCharacter.Traits.Contains(x.TraitId)).ToListAsync();
                character.Traits = traits;

                await _context.SaveChangesAsync();

                res.Data = _mapper.Map<GetCharacterDto>(character);
                res.Success = true;

            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = ex.Message;
            }
            return res;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacterById(int id)
        {
            var res = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
                var character = await _context.Characters.FirstOrDefaultAsync(x => x.CharacterId == id);
                if (character == null)
                {
                    res.Success = false;
                    res.Message = "Character not found!";
                }
                else
                {
                    _context.Characters.Remove(character);
                    await _context.SaveChangesAsync();

                    res.Data = await _context.Characters
                        .Include(x => x.CharacterClass)
                        .Include(x => x.Traits)
                        .Select(x => _mapper.Map<GetCharacterDto>(x)).ToListAsync();
                }
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Success = false;
                throw;
            }
            return res;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var res = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
                res.Data = await _context.Characters
                        .Include(x => x.CharacterClass)
                        .Include(x => x.Traits)
                        .Select(x => _mapper.Map<GetCharacterDto>(x)).ToListAsync();
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Success = false;
                throw;
            }
            return res;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharactersByClass(int classId)
        {
            var res = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
                var characterClass = await _context.CharacterClasses.FirstOrDefaultAsync(x => x.CharacterClassId == classId);
                if (characterClass == null)
                {
                    res.Message = "Class not found!";
                    res.Success = false;
                    return res;
                }
                res.Data = await _context.Characters
                        .Include(x => x.CharacterClass)
                        .Include(x => x.Traits)
                        .Where(x => x.CharacterClassId == characterClass.CharacterClassId)
                        .Select(x => _mapper.Map<GetCharacterDto>(x)).ToListAsync();
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Success = false;
                throw;
            }
            return res;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharactersByTraits(int[] traitsIds)
        {
            var res = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
                res.Data = await _context.Characters
                        .Include(x => x.CharacterClass)
                        .Include(x => x.Traits)
                        .Where(x => x.Traits.Any(x => traitsIds.Contains(x.TraitId)))
                        .Select(x => _mapper.Map<GetCharacterDto>(x)).ToListAsync();
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Success = false;
                throw;
            }
            return res;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var res = new ServiceResponse<GetCharacterDto>();
            try
            {
                res.Data = _mapper.Map<GetCharacterDto>(
                    await _context.Characters
                        .Include(x => x.CharacterClass)
                        .Include(x => x.Traits)
                        .FirstOrDefaultAsync(x => x.CharacterId == id));
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Success = false;
                throw;
            }
            return res;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(int id, UpdateCharacterDto updatedCharacter)
        {
            var res = new ServiceResponse<GetCharacterDto>();
            try
            {
                var oldCharacter = await _context.Characters.FirstOrDefaultAsync(x => x.CharacterId == id);
                if (oldCharacter == null)
                {
                    res.Message = "Character not found!";
                    res.Success = false;
                    return res;
                }

                await deleteTraitsByCharacterId(id);

                oldCharacter.CharacterClassId = updatedCharacter.CharacterClassId;
                oldCharacter.Description = updatedCharacter.Description;
                oldCharacter.Name = updatedCharacter.Name;
                oldCharacter.Story = updatedCharacter.Story;
                oldCharacter.Traits = await _context.Traits.Where(x => updatedCharacter.Traits.Contains(x.TraitId)).ToListAsync();
                await _context.SaveChangesAsync();
                
                await _context.SaveChangesAsync();

                res.Data = _mapper.Map<GetCharacterDto>(oldCharacter);
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Success = false;
                throw;
            }
            return res;
        }
        private async Task deleteTraitsByCharacterId(int id){
            var traits = await _context.Traits.Include(x=>x.Characters).Where(x=>x.Characters.Contains(_context.Characters.FirstOrDefault(c=>c.CharacterId == id))).ToListAsync();
            foreach (var tr in traits)
                tr.Characters.Remove(tr.Characters.FirstOrDefault(x=>x.CharacterId == id));
            await _context.SaveChangesAsync();
        } 
    }
}