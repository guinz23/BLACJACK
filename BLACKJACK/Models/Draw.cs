using System;
using System.ComponentModel.DataAnnotations;

namespace BLACKJACK.Models
{
    public class Draw
    {
        [Key]
        public Guid Id { get; set; }
        public bool success { get; set; }
        public Card [] cards { get; set; }
        public Guid deck_id { get; set; }
        public int remaining { get; set;}
    }

}
