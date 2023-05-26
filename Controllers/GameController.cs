using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SophosGameLibraryAPI.DBContext;
using SophosGameLibraryAPI.DTOs;
using SophosGameLibraryAPI.Models;

namespace SophosGameLibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GameController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Game
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameDto>>> GetGames()
        {
            if (_context.Games == null)
            {
                return NotFound();
            }

            return await _context.Games.Select(game => new GameDto
            {
                Id = game.Id,
                Name = game.Name,
                Description = game.Description,
                RentalPrice = game.RentalPrice,
                SellingPrice = game.SellingPrice,
                DirectorName = game.DirectorName,
                ProductorName = game.ProductorName,
                LaunchDate = game.LaunchDate,
                LeadCharacterName = game.LeadCharacterName,
                GamePlatform = game.GamePlatform,
            }).ToListAsync();
        }

        // GET: api/Game/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameDto>> GetGame(int id)
        {
            if (_context.Games == null)
            {
                return NotFound();
            }
            var game = await _context.Games.FindAsync(id);

            if (game == null)
            {
                return NotFound();
            }

            return new GameDto
            {
                Id = game.Id,
                Name = game.Name,
                Description = game.Description,
                RentalPrice = game.RentalPrice,
                SellingPrice = game.SellingPrice,
                DirectorName = game.DirectorName,
                ProductorName = game.ProductorName,
                LaunchDate = game.LaunchDate,
                LeadCharacterName = game.LeadCharacterName,
                GamePlatform = game.GamePlatform,
            };
        }

        // PUT: api/Game/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(int id, GameDto game)
        {
            if (id != game.Id)
            {
                return BadRequest();
            }

            _context.Entry(game).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(id))
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

        // POST: api/Game
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GameDto>> PostGame(GameDto game)
        {
            if (_context.Games == null)
            {
                return Problem("Entity set 'AppDbContext.Games'  is null.");
            }
            _context.Games.Add(
                new Game
                {
                    Id = game.Id,
                    Name = game.Name!,
                    Description = game.Description,
                    RentalPrice = game.RentalPrice,
                    SellingPrice = game.SellingPrice,
                    DirectorName = game.DirectorName,
                    ProductorName = game.ProductorName,
                    LaunchDate = game.LaunchDate,
                    LeadCharacterName = game.LeadCharacterName,
                    GamePlatform = game.GamePlatform,
                }
            );
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (GameExists(game.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetGame", new { id = game.Id }, game);
        }

        // DELETE: api/Game/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            if (_context.Games == null)
            {
                return NotFound();
            }
            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            _context.Games.Remove(game);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GameExists(int id)
        {
            return (_context.Games?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
