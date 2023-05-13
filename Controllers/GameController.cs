using Microsoft.AspNetCore.Mvc;
using videogames.Data;
using videogames.Models;

namespace videogames.Controllers;

[ApiController]
[Route("Game")]
public class GameController : ControllerBase
{
    private readonly DataContext _context;
    private static List<Game> Games = new List<Game> { };
    public GameController(DataContext context)
    {
        _context = context;
    }
    //Obtengo todos los videojuegos
    [HttpGet]
    public ActionResult<List<Game>> Get()
    {
        return Ok(_context.Games);
    }

    //Obtengo un videojuego
    [HttpGet]
    [Route("{Id}")]
    public ActionResult<List<Game>>GetGameById(int Id)
    {
        var game = _context.Games.Find(Id);
        return game==null ? NotFound() : Ok(game);
    }

    //Añadir un juego
    [HttpPost]
    public ActionResult New(Game game)
    {
        var existingGameId = Games.Find(x => x.Id == game.Id);
        var existingGameName = Games.Find(x => x.Name == game.Name);
        if (existingGameId != null)
        {
            return Conflict("This videogame id already exists");

        }
        else if (existingGameName != null)
        {
            return Conflict("This videogame name already exists");

        }
        else
        {
            _context.Games.Add(game);
            _context.SaveChanges();
            var resourceUrl = Request.Path.ToString() + "/" + game.Id;
            return Created(resourceUrl, game);
        }
    }
    //Borrar juego
    [HttpDelete]
    [Route("{Id}")]
    public ActionResult<Game> DeleteGame(int Id)
    {
        var existingGame = _context.Games.Find(Id);
        if (existingGame == null)
        {
            return NotFound();
        }
        else
        {
            //Primero borramos los alquileres de ese juego
            var userGames = _context.UserGames.Where(x => x.IdGame == existingGame.Id).ToList();
            if (userGames != null)
            {
                foreach (var userGame in userGames)
                {
                    _context.UserGames.Remove(userGame);
                    _context.SaveChanges();
                }
            }
            _context.Games.Remove(existingGame);
            _context.SaveChanges();
            return NoContent();
        }
    }

    //Editar juego
    [HttpPut]
    public ActionResult UpdateUser(Game gameItem)
    {
        var existingGame = _context.Games.Find(gameItem.Id);
        if (existingGame == null)
        {
            return NotFound("This videogame id does not exist");
        }
        else
        {
            if (gameItem.Name != "string")
            {
                existingGame.Name = gameItem.Name;
            }
            if (gameItem.Description != "string")
            {
                existingGame.Description = gameItem.Description;
            }
             if (gameItem.Quantity != 0)
            {
                existingGame.Quantity = gameItem.Quantity;
            }
             if (gameItem.Price != 0)
            {
                existingGame.Price = gameItem.Price;
            }
            _context.SaveChanges();
            return Ok();
        }
    }


}
