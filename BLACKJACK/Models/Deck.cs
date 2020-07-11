using System;
using System.ComponentModel.DataAnnotations;

namespace BLACKJACK.Models
{
    public class Deck
    {
        [Key]
        public Guid deck_id { get; set; }
        public bool success { get; set; }
        public bool shuffled { get; set; }
        public int remaining { get; set; }
    }
}
