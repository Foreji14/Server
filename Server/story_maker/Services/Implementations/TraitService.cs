using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using story_maker.Dtos;
using story_maker.Models;
using story_maker.Services.Interfaces;

namespace story_maker.Services.Implementations
{
    public class TraitService : ITraitService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public TraitService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<GetTraitDto>> AddTrait(AddTraitDto newTrait)
        {
            var res = new ServiceResponse<GetTraitDto>();
            try
            {
                if (await _context.Traits.FirstOrDefaultAsync(x => x.Name.ToLower().Equals(newTrait.Name.ToLower())) == null)
                {
                    var trait = _mapper.Map<Trait>(newTrait);
                    await _context.Traits.AddAsync(trait);
                    await _context.SaveChangesAsync();

                    res.Data = _mapper.Map<GetTraitDto>(trait);

                }
                else
                {
                    res.Message = "Trait already exists!";
                    res.Success = false;
                }

            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Success = false;
            }
            return res;
        }

        public async Task<ServiceResponse<List<GetTraitDto>>> DeleteTraitById(int id)
        {
            var res = new ServiceResponse<List<GetTraitDto>>();
            try
            {
                var trait = await _context.Traits.FirstOrDefaultAsync(x => x.TraitId == id);
                if (trait == null)
                {
                    res.Message = "Trait not found!";
                    res.Success = false; return res;
                }
                _context.Traits.Remove(trait);
                await _context.SaveChangesAsync();

                res.Data = await _context.Traits.Select(x => _mapper.Map<GetTraitDto>(x)).ToListAsync();
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Success = false;
            }
            return res;
        }

        public async Task<ServiceResponse<List<GetTraitDto>>> GetAllTraits()
        {
            var res = new ServiceResponse<List<GetTraitDto>>();
            try
            {
                res.Data = await _context.Traits.Select(x => _mapper.Map<GetTraitDto>(x)).ToListAsync();
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Success = false;
            }
            return res;
        }

        public async Task<ServiceResponse<GetTraitDto>> GetTraitById(int id)
        {
            var res = new ServiceResponse<GetTraitDto>();
            try
            {
                res.Data = await _context.Traits.Select(x => _mapper.Map<GetTraitDto>(x)).FirstOrDefaultAsync(x=>x.TraitId == id);
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Success = false;
            }
            return res;
        }

        public async Task<ServiceResponse<GetTraitDto>> UpdateTrait(int id, UpdateTraitDto updatedTrait)
        {
            var res = new ServiceResponse<GetTraitDto>();
            try
            {
                var newTrait = await _context.Traits.FirstOrDefaultAsync(x=>x.TraitId == id);
                if (newTrait == null)
                {
                    res.Message = "Trait not found!";
                    res.Success = false; return res;
                }
                newTrait.Name = updatedTrait.Name;
                await _context.SaveChangesAsync();

                res.Data = _mapper.Map<GetTraitDto>(newTrait);
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Success = false;
            }
            return res;
        }
    }
}