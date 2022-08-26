using System;
using story_maker.Dtos;
using story_maker.Models;
namespace story_maker{
    public class Profile : AutoMapper.Profile{
        public Profile()
        {
            CreateMap<Character, GetCharacterDto>();
            CreateMap<CharacterClass, GetCharacterClassDto>();
            CreateMap<Trait, GetTraitDto>();

            CreateMap<AddCharacterClassDto, CharacterClass>();
            CreateMap<AddTraitDto, Trait>();
        }
    }
}