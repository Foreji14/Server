using System;

namespace story_maker.Models
{
    public class Trait
    {
        public int TraitId { get; set; }
        public string Name { get; set; }
        public List<Character> Characters{get;set;}
    }
}