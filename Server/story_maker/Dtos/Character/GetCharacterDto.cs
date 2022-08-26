using System;
using story_maker.Models;

namespace story_maker.Dtos
{
    public class GetCharacterDto
    {
        public int CharacterId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Story { get; set; }
        public GetCharacterClassDto CharacterClass { get; set; }
        public List<GetTraitDto> Traits { get; set; }
    }
}