using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SophosGameLibraryAPI.DBContext;
using SophosGameLibraryAPI.DTOs;
using SophosGameLibraryAPI.Models;

namespace SophosGameLibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            return await _context.Users.Select(user => new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Address = user.Address,
                Phone = user.Phone,
                Role = user.Role,
            }).ToListAsync();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            List<GameDto> games = new List<GameDto>();
            foreach (var rental in _context.Rentals)
            {
                if (rental.FkRentalUser == user.Id)
                {
                    Game game = _context.Games.Find(rental.FkRentalGame)!;
                    games.Add(new GameDto
                    {
                        Id = game.Id!,
                        Name = game.Name,
                        Description = game.Description,
                        RentalPrice = game.RentalPrice,
                        SellingPrice = game.SellingPrice,
                        DirectorName = game.DirectorName,
                        ProductorName = game.ProductorName,
                        LaunchDate = game.LaunchDate,
                        LeadCharacterName = game.LeadCharacterName,
                        GamePlatform = game.GamePlatform,
                    });
                }
            }

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Address = user.Address,
                Phone = user.Phone,
                Role = user.Role,
                Birthday = user.Birthday,
                Games = games,
            };
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserDto user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(UserDto user)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'AppDbContext.Users'  is null.");
            }
            _context.Users.Add(
                new User
                {
                    Name = user.Name!,
                    Email = user.Email,
                    Address = user.Address,
                    Phone = user.Phone,
                    Role = user.Role,
                    Birthday = user.Birthday,
                }
            );
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
