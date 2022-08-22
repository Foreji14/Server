using System;

namespace story_maker.Models
{
    public class Character
    {
        public int CharacterId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Story { get; set; }
        public CharacterClass CharacterClass { get; set; }
        public int CharacterClassId { get; set; }
        public List<Trait> Traits { get; set; }
    }
}