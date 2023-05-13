using Microsoft.AspNetCore.Mvc;
using videogames.Data;
using videogames.Models;

namespace videogames.Controllers;

[ApiController]
[Route("Users")]
public class UserController : ControllerBase
{
    private readonly DataContext _context;
    private static List<User> Users = new List<User> { };
    public UserController(DataContext context)
    {
        _context = context;
    }

    //Obtengo todos los usuarios
    [HttpGet]
    public ActionResult<List<User>> Get()
    {
        return Ok(_context.Users);
    }

    //Registrar nuevo usuario
    [HttpPost]
    public ActionResult Post(User user)
    {
        var existingUserUsername = _context.Users.FirstOrDefault(x => x.Username == user.Username);
         if (existingUserUsername != null)
        {
            return Conflict("Ya existe un usuario con ese username");

        }
        else
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            var resourceUrl = Request.Path.ToString() + "/" + user.Id;
            return Created(resourceUrl, user);
        }
    }

    //Loguear usuario
    [HttpPost]
    [Route("{Username}/{Password}")]
    public ActionResult<User> Login(string Username, string Password)
    {
        User user = _context.Users.SingleOrDefault(u => u.Username == Username && u.Password == Password);
        return user == null ? NotFound(null) : Ok(user);
    }

    //Borrar usuario
    [HttpDelete]
    [Route("{Id}")]
    public ActionResult<User> DeleteUser(int Id)
    {
        var existingUser = _context.Users.Find(Id);
        if (existingUser == null)
        {
            return NotFound();
        }
        else
        {
            //Primero borramos sus datos de alquiler
            var userGames = _context.UserGames.Where(x => x.IdUser == existingUser.Id).ToList();
            if (userGames != null)
            {
                foreach (var userGame in userGames)
                {
                    _context.UserGames.Remove(userGame);
                    _context.SaveChanges();
                }
            }

            _context.Users.Remove(existingUser);
            _context.SaveChanges();
            return NoContent();
        }
    }

    //Editar usuario
    [HttpPut]
    public ActionResult UpdateUser(User userItem)
    {
        var existingUser = _context.Users.Find(userItem.Id);
        if (existingUser == null)
        {
            return NotFound("No se ha encontrado un usuario con ese id");
        }
        else
        {
            if (userItem.Username != "string")
            {
                existingUser.Username = userItem.Username;
            }
            if (userItem.Password != "string")
            {
                existingUser.Password = userItem.Password;
            }
            _context.SaveChanges();
            return Ok();
        }
    }
}

