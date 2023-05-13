using Microsoft.AspNetCore.Mvc;
using videogames.Data;
using videogames.Models;

namespace videogames.Controllers;

[ApiController]
[Route("UserGame")]
public class UserGameController : ControllerBase
{
    private readonly DataContext _context;
    private static List<UserGame> UserGames = new List<UserGame> { };
    public UserGameController(DataContext context)
    {
        _context = context;
    }


    //Obtengo todos los juegos de todos los usuarios
    [HttpGet]
    public ActionResult<List<UserGame>> Get()
    {
        return Ok(_context.UserGames);
    }

    //Obtengo todos los juegos de un usuario
    [HttpGet]
    [Route("User/{IdUser}")]
    public ActionResult<List<UserGame>> GetByUserId(int IdUser)
    {
        var userGames = _context.UserGames.Where(x => x.IdUser == IdUser).ToList();
        var games = new List<Game>();
        foreach (var item in userGames)
        {
            var game = _context.Games.Find(item.IdGame);
            if (game != null)
            {
                games.Add(game);
            }
        }
        return games.Count == 0 ? NotFound("El usuario no ha comprado ningún juego") : Ok(games);
    }

    //Obtengo todos los usuarios que han alquilado un juego
    [HttpGet]
    [Route("Game/{IdGame}")]
    public ActionResult<List<UserGame>> GetByGameId(int IdGame)
    {
        var userGames = _context.UserGames.Where(x => x.IdGame == IdGame).ToList();
        var users = new List<User>();
        foreach (var item in userGames)
        {
            var user = _context.Users.Find(item.IdUser);
            if (user != null)
            {
                users.Add(user);
            }
        }
        return users.Count == 0 ? NotFound("El juego no ha sido comprado por ningún usuario") : Ok(users);
    }

    //Un usuario alquila un juego
    [HttpPost]
    [Route("{IdUser}/{IdGame}")]
    public ActionResult NewUserGame(int IdUser, int IdGame)
    {
        UserGame userGame = new UserGame();
        userGame.IdUser = IdUser;
        userGame.IdGame = IdGame;
        _context.UserGames.Add(userGame);
        _context.SaveChanges();
        var resourceUrl = Request.Path.ToString() + "/" + userGame.Id;
        return Created(resourceUrl, userGame);
    }

    //Un usuario devuelve un juego
    [HttpDelete]
    [Route("{IdUser}/{IdGame}")]
    public ActionResult DeleteUserGame(int IdUser, int IdGame)
    {

        UserGame userGame = _context.UserGames.SingleOrDefault(x => x.IdUser == IdUser && x.IdGame == IdGame);

        if (userGame == null)
        {
            return NotFound();
        }
        else
        {
            _context.UserGames.Remove(userGame);
            _context.SaveChanges();
            return NoContent();
        }
    }
}


