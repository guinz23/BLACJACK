using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLACKJACK.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BLACKJACK.Controllers
{
    public class DeckController : Controller
    {

        private readonly DataBaseContext _context;

        public DeckController(DataBaseContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("api/deck")]
        public async Task<ActionResult<IEnumerable<Deck>>> GetCard([FromQuery]int deck_count)
        {
            Console.WriteLine(deck_count);
            var deck = new Deck();
            deck.deck_id=Guid.NewGuid();
            deck.success = true;
            deck.remaining = 52;
            deck.shuffled = false;
            _context.Decks.Add(deck);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetDeck", new { id = deck.deck_id }, deck);
        }
        [HttpPut]
        [Route("api/deck/{id}/shuffle")]
        public async Task<ActionResult<IEnumerable<Deck>>> Shuffled([FromBody]Deck deck)
        {
            var id = Guid.Parse((string)RouteData.Values["id"]);
            var result =  _context.Decks.SingleOrDefault(b => b.deck_id == id);
            if (result!=null)
            {
                result.shuffled = deck.shuffled;
                await _context.SaveChangesAsync();
            }
            else
            {
                return Json("No se pudieron revolver las cartas");
            }

            return CreatedAtAction("Cartas revueltas", new { id = result.deck_id }, result);
        }
    }
}
