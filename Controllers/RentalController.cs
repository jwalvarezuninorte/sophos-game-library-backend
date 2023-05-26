using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SophosGameLibraryAPI.DBContext;
using SophosGameLibraryAPI.DTOs;
using SophosGameLibraryAPI.Models;

namespace SophosGameLibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RentalsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/rentals
        [HttpGet]

        public async Task<ActionResult<IEnumerable<RentalDto>>> GetRentals()
        {
            if (_context.Rentals == null)
            {
                return NotFound();
            }

            return await _context.Rentals
                .Select(x => new RentalDto()
                {
                    Id = x.Id,
                    DateEnd = x.DateEnd,
                    DateStart = x.DateStart,
                    Game = _context.Games.Where(g => g.Id == x.FkRentalGame).Select(g => new GameDto()
                    {
                        Id = g.Id,
                        Name = g.Name,
                        Description = g.Description,
                        RentalPrice = g.RentalPrice,
                        SellingPrice = g.SellingPrice,
                        DirectorName = g.DirectorName,
                        ProductorName = g.ProductorName,
                        LaunchDate = g.LaunchDate,
                        LeadCharacterName = g.LeadCharacterName,
                        GamePlatform = g.GamePlatform,
                    }).FirstOrDefault(),
                    User = _context.Users.Where(u => u.Id == x.FkRentalUser).Select(u => new UserDto()
                    {
                        Id = u.Id,
                        Name = u.Name,
                        Email = u.Email,
                        Phone = u.Phone,
                        Address = u.Address,
                        Birthday = u.Birthday,
                        Role = u.Role,
                    }).FirstOrDefault(),
                })
                .ToListAsync();
        }

        // GET: api/Rental/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RentalDto>> GetRental(int id)
        {
            if (_context.Rentals == null)
            {
                return NotFound();
            }
            var rental = await _context.Rentals.FindAsync(id);

            if (rental == null)
            {
                return NotFound();
            }

            return new RentalDto()
            {
                Id = rental.Id,
                DateEnd = rental.DateEnd,
                DateStart = rental.DateStart,
                Game = _context.Games.Where(g => g.Id == rental.FkRentalGame).Select(g => new GameDto()
                {
                    Id = g.Id,
                    Name = g.Name,
                    Description = g.Description,
                    RentalPrice = g.RentalPrice,
                    SellingPrice = g.SellingPrice,
                    DirectorName = g.DirectorName,
                    ProductorName = g.ProductorName,
                    LaunchDate = g.LaunchDate,
                    LeadCharacterName = g.LeadCharacterName,
                    GamePlatform = g.GamePlatform,
                }).FirstOrDefault(),
                User = _context.Users.Where(u => u.Id == rental.FkRentalUser).Select(u => new UserDto()
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Phone = u.Phone,
                    Address = u.Address,
                    Birthday = u.Birthday,
                    Role = u.Role,
                }).FirstOrDefault(),
            };
        }

        // PUT: api/Rental/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRental(int id, RentalDto rental)
        {
            if (id != rental.Id)
            {
                return BadRequest();
            }

            _context.Entry(rental).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentalExists(id))
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

        // POST: api/Rental
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Rental>> PostRental(Rental rental)
        {
            if (_context.Rentals == null)
            {
                return Problem("Entity set 'AppDbContext.Rentals'  is null.");
            }
            _context.Rentals.Add(rental);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RentalExists(rental.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRental", new { id = rental.Id }, rental);
        }

        // DELETE: api/Rental/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRental(int id)
        {
            if (_context.Rentals == null)
            {
                return NotFound();
            }
            var rental = await _context.Rentals.FindAsync(id);
            if (rental == null)
            {
                return NotFound();
            }

            _context.Rentals.Remove(rental);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("most-rented-games")]
        public async Task<ActionResult<IEnumerable<GameDto>>> GetMostRentedGames()
        {
            if (_context.Rentals == null)
            {
                return NotFound();
            }
            var rentals = await _context.Rentals.ToListAsync();
            var games = await _context.Games.ToListAsync();
            var mostRentedGames = rentals.GroupBy(r => r.FkRentalGame).OrderByDescending(g => g.Count()).Take(5).Select(g => g.Key).ToList();
            var mostRentedGamesDto = games.Where(g => mostRentedGames.Contains(g.Id)).Select(g => new GameDto()
            {
                Id = g.Id,
                Name = g.Name,
                Description = g.Description,
                RentalPrice = g.RentalPrice,
                SellingPrice = g.SellingPrice,
                DirectorName = g.DirectorName,
                ProductorName = g.ProductorName,
                LaunchDate = g.LaunchDate,
                LeadCharacterName = g.LeadCharacterName,
                GamePlatform = g.GamePlatform,
            }).ToList();

            return mostRentedGamesDto;
        }

        // Same as most frequency user
        [HttpGet("users-with-most-rented-games")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersWithMostRentedGames()
        {
            if (_context.Rentals == null)
            {
                return NotFound();
            }
            var rentals = await _context.Rentals.ToListAsync();
            var users = await _context.Users.ToListAsync();
            var usersWithMostRentedGames = rentals.GroupBy(r => r.FkRentalUser).OrderByDescending(g => g.Count()).Take(5).Select(g => g.Key).ToList();
            var usersWithMostRentedGamesDto = users.Where(u => usersWithMostRentedGames.Contains(u.Id)).Select(u => new UserDto()
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Phone = u.Phone,
                Address = u.Address,
                Birthday = u.Birthday,
                Role = u.Role,
            }).ToList();

            return usersWithMostRentedGamesDto;
        }
        private bool RentalExists(int id)
        {
            return (_context.Rentals?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
