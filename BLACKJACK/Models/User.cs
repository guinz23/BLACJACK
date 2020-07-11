using System;
namespace BLACKJACK.Models
{
    public class User
    {
       public  Guid Id { get; set; }
       public string username { get; set; }
       public int total_money { get; set; }
    }
}
