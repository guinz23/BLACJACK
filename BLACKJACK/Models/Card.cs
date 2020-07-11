using System;
using System.ComponentModel.DataAnnotations;

namespace BLACKJACK.Models
{
    public class Card
    {
        public Card()
        {

        }
        [Key]
        public Guid Id { get; set; }
        public string code { get; set; }
        public string image { get; set;}
        public string value { get; set; }
        public string suit { get; set; }
    }
}
