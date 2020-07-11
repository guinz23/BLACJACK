using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLACKJACK.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BLACKJACK.Controllers
{
    public class UserController : Controller
    {
        public readonly DataBaseContext _context;


        public UserController(DataBaseContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("api/user")]
        public async Task<ActionResult<User>> PostVehicle([FromBody]User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

    }
}
