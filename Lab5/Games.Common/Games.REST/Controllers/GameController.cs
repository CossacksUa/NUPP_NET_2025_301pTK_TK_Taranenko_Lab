using Games.Common;
using Games.Infrastructure;
using Games.Infrastructure.Repository;
using Games.REST.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Games.REST.Controllers
{
    [Authorize] // ✅ Захист усіх методів токеном
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly ICrudServiceAsync<Game> _service;

        public GamesController(ICrudServiceAsync<Game> service)
        {
            _service = service;
        }

        // 🔒 Тестовий endpoint, щоб перевірити токен
        [HttpGet("secret")]
        public IActionResult SecretData()
        {
            return Ok(new { message = "Це секретна інформація! 🔒" });
        }

        // CRUD далі ↓↓↓
        [HttpGet]
        public async Task<IEnumerable<GameModel>> GetAll()
        {
            var games = await _service.ReadAllAsync();
            return games.Select(g => new GameModel
            {
                Id = g.Id,
                Name = g.Name,
                Price = g.Price,
                Rating = g.Rating
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GameModel>> Get(Guid id)
        {
            var game = await _service.ReadAsync(id);
            if (game == null)
                return NotFound();

            return new GameModel
            {
                Id = game.Id,
                Name = game.Name,
                Price = game.Price,
                Rating = game.Rating
            };
        }

        [HttpPost]
        public async Task<ActionResult> Create(GameModel model)
        {
            var game = new Game
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Price = model.Price,
                Rating = model.Rating
            };

            var result = await _service.CreateAsync(game);
            if (!result) return BadRequest();

            return CreatedAtAction(nameof(Get), new { id = game.Id }, model);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, GameModel model)
        {
            var game = await _service.ReadAsync(id);
            if (game == null) return NotFound();

            game.Name = model.Name;
            game.Price = model.Price;
            game.Rating = model.Rating;

            var result = await _service.UpdateAsync(game);
            if (!result) return BadRequest();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var game = await _service.ReadAsync(id);
            if (game == null) return NotFound();

            var result = await _service.RemoveAsync(game);
            if (!result) return BadRequest();

            return NoContent();
        }
    }
}
