using System;
using Microsoft.EntityFrameworkCore;

namespace BLACKJACK.Models
{
    public class DataBaseContext: DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options)
           : base(options)
        {
        }
        public DbSet<Deck> Decks { get; set; }

        public DbSet<Card> Cards { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Draw> Draws { get; set; }
    }
}