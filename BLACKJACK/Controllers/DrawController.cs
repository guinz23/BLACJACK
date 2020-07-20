using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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
        public async Task<ActionResult<Draw>> Draw([FromQuery] int count,Guid idExits)
        {
            var id = Guid.Parse((string)RouteData.Values["id"]);
            var deckdb = await _context.Decks.ToListAsync();
            var result = new Draw();
            if (DeckExists(id) == true)
            {
                
                foreach (var deck in deckdb.Where(e => e.deck_id == id))
                {
                    if (updateDeack(deck,count))
                    {
                        if (deck.shuffled == false)
                        {
                            result.Id = Guid.NewGuid();
                            result.deck_id = deck.deck_id;
                            result.success = deck.success;
                            result.cards = this.getCountCard(count, deck.shuffled);
                            result.remaining = deck.remaining;
                        }
                        else
                        {
                            result.Id = idExits == Guid.Empty ? Guid.NewGuid() : idExits;
                            result.deck_id = deck.deck_id;
                            result.success = deck.success;
                            result.cards = this.getCountCard(count, deck.shuffled);
                            result.remaining = deck.remaining;
                        }
                    }
                    else
                    {
                        return Json("no hay mas cartas");
                    }
                }
            }
            else
            {
                return Json("id invalido");
            }
            return result;
        }

        private Card[] getCountCard(int count,bool shuffle)
        {
            var card = new Card[count];
            if (shuffle==false)
            {
                 for (var i = 0; i < count; i++)
            {
                Random rnd = new Random();
                int value = rnd.Next(0, 51);
                card[i] = drawDeckCard()[value];

              }
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    Random rnd = new Random();
                    int j = rnd.Next(i, drawDeckCard().Length);
                    var temp = drawDeckCard()[j];
                    drawDeckCard()[j] = drawDeckCard()[i];
                    drawDeckCard()[i] = temp;
                    card[i] = temp;
                }
            }
           
            return card;
        }

        private bool DeckExists(Guid id)
        {
            return _context.Decks.Any(e => e.deck_id == id);
        }

        public bool updateDeack(Deck _deck,int count)
        {
            bool state = false;
            var result = _context.Decks.SingleOrDefault(b => b.deck_id == _deck.deck_id);
            if (result != null)
            {
                if (result.remaining <= 0)
                {
                    state = false;
                }
                else
                {
                    result.deck_id = _deck.deck_id;
                    result.success = _deck.success;
                    result.remaining = _deck.remaining - count;
                    result.shuffled = _deck.shuffled;
                    _context.SaveChanges();
                    state = true;
                }
            }
            return state;
        }

        private Card[] drawDeckCard()
        {
            var card = new Card[] {
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/8C.png",
                 code="8C",
                 suit="CLUBS",
                 value="8"

                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/4C.png",
                  code="AC",
                 suit="CLUBS",
                 value="4"

                },
                 new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/2S.png",
                  code="2S",
                 suit="SPADES",
                 value="2"

                },
                  new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/0H.png",
                  code="0H",
                 suit="HEARTS",
                 value="10"
                },
                 new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/3S.png",
                  code="3S",
                 suit="SPADES",
                 value="3"
                },
                  new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/AS.png",
                  code="AS",
                 suit="SPADES",
                 value="10"
                },
                 new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/9H.png",
                  code="9H",
                 suit="HEARTS",
                 value="9"
                },
                 new Card
                {
                Id=Guid.NewGuid(),
                image="https://deckofcardsapi.com/static/img/5C.png",
                 code="5C",
                 suit="CLUBS",
                 value="5"
                },
                new Card
                {
                Id=Guid.NewGuid(),
                image="https://deckofcardsapi.com/static/img/KD.png",
                 code="KD",
                 suit="DIAMONDS",
                 value="13"
                },
                new Card
                {
                Id=Guid.NewGuid(),
                image="https://deckofcardsapi.com/static/img/QS.png",
                 code="QS",
                 value="12",
                 suit="SPADES"

                },
                 new Card
                {
                Id=Guid.NewGuid(),
                image="https://deckofcardsapi.com/static/img/QC.png",
                 code="QC",
                 suit="QUEEN",
                 value="12"

                },
                 new Card
                {
                Id=Guid.NewGuid(),
                image="https://deckofcardsapi.com/static/img/3C.png",
                 code="3C",
                 suit="CLUBS",
                 value="3"
                },
                new Card
                {
                Id=Guid.NewGuid(),
                image="https://deckofcardsapi.com/static/img/QH.png",
                 code="QH",
                 suit="HEARTS",
                 value="12"

                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/9D.png",
                  code="9D",
                 suit="DIAMONDS",
                 value="9"

                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/QD.png",
                  code="QD",
                 suit="DIAMONDS",
                 value="12"

                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/JH.png",
                  code="JH",
                 suit="HEARTS",
                 value="13"

                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/9C.png",
                  code="9C",
                 suit="CLUBS",
                 value="9"

                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/4H.png",
                  code="4H",
                 suit="HEARTS",
                 value="4"

                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/3D.png",
                  code="3D",
                 suit="DIAMONDS",
                 value="3"

                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/KS.png",
                  code="KS",
                 suit="SPADES",
                 value="13"

                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/5D.png",
                  code="5D",
                  value="5",
                  suit="DIAMONDS"
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/6S.png",
                  code="6S",
                  value="6",
                  suit="SPADES"
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/JD.png",
                  code="JD",
                 suit="DIAMONDS",
                 value="11"
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/7S.png",
                  code="7S",
                 suit="SPADES",
                 value="7"

                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/9S.png",
                  code="9S",
                 suit="SPADES",
                 value="9"

                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/5S.png",
                  code="5S",
                  value="5",
                  suit="SPADES"


                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/7H.png",
                  code="7H",
                 suit="HEARTS",
                 value="7"
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/5H.png",
                  code="5H",
                 suit="HEARTS",
                 value="5"
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/AH.png",
                  code="AC",
                 suit="HEARTS",
                 value="10"
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/6D.png",
                  code="6D",
                 suit="DIAMONDS",
                 value="6"
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/KH.png",
                  code="KH",
                 suit="HEARTS",
                 value="13"
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/JS.png",
                  code="JS",
                 value= "11",
                  suit = "SPADES"
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/6C.png",
                  code="6C",
                  suit="CLUBS",
                  value="6"
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/8D.png",
                  code="8D",
                 suit="DIAMONDS",
                 value="8"
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/7C.png",
                  code="7C",
                 suit="CLUBS",
                 value="7"
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/2C.png",
                  code="2C",
                 suit="CLUBS",
                 value="2"
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/KC.png",
                  code="KC",
                 suit="CLUBS",
                 value="13"
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/0C.png",
                  code="0C",
                 suit="CLUBS",
                 value="10"
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/AC.png",
                  code="AC",
                 suit="CLUBS",
                 value="10"
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/4S.png",
                  code="4S",
                 suit="SPADES",
                 value="4"
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/3H.png",
                  code="3H",
                 suit="HEARTS",
                 value="3"
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/JC.png",
                  code="JC",
                 suit="CLUBS",
                 value="11"
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/8S.png",
                  code="8S",
                 suit="SPADES",
                 value="8"
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/0S.png",
                  code="0S",
                 suit="SPADES",
                 value="10"
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/4D.png",
                  code="4D",
                 suit="DIAMONDS",
                 value="4"
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/7D.png",
                  code="7D",
                 suit="DIAMONDS",
                 value="7"
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/8H.png",
                  code="8H",
                 suit="HEARTS",
                 value="8"
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/aceDiamonds.png",
                  code="AD",
                 suit="DIAMONDS",
                 value="10"
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/0D.png",
                 code="0D",
                 suit="DIAMONDS",
                 value="10"
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/6H.png",
                 code="6H",
                 suit="HEARTS",
                 value="6"
                },
                new Card
                {
                 Id=Guid.NewGuid(),
                 image="https://deckofcardsapi.com/static/img/2D.png",
                  code="2D",
                  value="2",
                  suit="DIAMONDS"
                },
            };
            return card;
        }
    }
}
