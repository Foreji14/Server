using System;
using Microsoft.EntityFrameworkCore;
using story_maker.Models;

namespace story_maker 
{
    public class DataContext : DbContext
    {
        
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
        public DbSet<Character> Characters {get; set;}
        public DbSet<CharacterClass> CharacterClasses { get; set; }
        public DbSet<Trait> Traits { get; set; }

    }
}