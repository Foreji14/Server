using System;
using story_maker.Models;

namespace story_maker.Dtos
{
    public class UpdateCharacterDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Story { get; set; }
        public int CharacterClassId { get; set; }
        public int[] Traits { get; set; }
    }
}