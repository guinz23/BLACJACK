using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using BLACKJACK.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BLACKJACK.Controllers
{
    public class DrawController : Controller
    {
        public readonly DataBaseContext _context;

        public DrawController(DataBaseContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("api/deck/{id}/draw")]
        public async Task<ActionResult<Draw>> Draw([FromQuery] int count)
        {
            var deckdb = await _context.Decks.ToListAsync();
            var id= (string)RouteData.Values["id"];
            var result = new Draw();
            foreach (var deck in deckdb.Where(e => e.deck_id == Guid.Parse(id)))
            {
                result.Id = Guid.NewGuid();
                result.deck_id = deck.deck_id;
                result.success = deck.success;
                result.cards = this.getCountCard(count);
                result.remaining = deck.remaining;
            }
            return result;
        }

        private  Card[] getCountCard(int count)
        {
             var card = new Card[count];
              for (var i = 0; i < count; i++)
            {
                Random rnd = new Random();
                int value = rnd.Next(0, 51);
                card[i] = drawDeckCard()[value];
               
            }
            return card;
        }


        private Card [] drawDeckCard()
        {
            var card= new Card[] {
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/8C.png",

                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/4C.png",
                },
                 new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/2S.png",
                },
                  new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/0H.png",
                },
                 new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/3S.png",
                },
                  new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/AS.png",
                },
                 new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/9H.png",
                },
                 new Card
                {
                Id=Guid.NewGuid(),
                image="https://deckofcardsapi.com/static/img/5C.png",
                },
                new Card
                {
                Id=Guid.NewGuid(),
                image="https://deckofcardsapi.com/static/img/KD.png",
                },
                new Card
                {
                Id=Guid.NewGuid(),
                image="https://deckofcardsapi.com/static/img/QS.png",
                },
                 new Card
                {
                Id=Guid.NewGuid(),
                image="https://deckofcardsapi.com/static/img/QC.png",
                },
                 new Card
                {
                Id=Guid.NewGuid(),
                image="https://deckofcardsapi.com/static/img/3C.png",
                },
                new Card
                {
                Id=Guid.NewGuid(),
                image="https://deckofcardsapi.com/static/img/QH.png",
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/9D.png",
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/QD.png",
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/JH.png",
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/9C.png",
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/4H.png",
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/3D.png",
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/KS.png",
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/5D.png",
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/6S.png",
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/JD.png",
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/7S.png",
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/9S.png",
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/5S.png",
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/7H.png",
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/5H.png",
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/AH.png",
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/6D.png",
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/KH.png",
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/JS.png",
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/6C.png",
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/8D.png",
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/7C.png",
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/2C.png",
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/KC.png",
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/0C.png",
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/AC.png",
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/4S.png",
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/3H.png",
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/JC.png",
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/8S.png",
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/0S.png",
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/4D.png",
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/7D.png",
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/8H.png",
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/aceDiamonds.png",
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/0D.png",
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/6H.png",
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/2D.png",
                },
            };
            return card;
        }
    }
}
