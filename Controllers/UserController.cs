using Microsoft.AspNetCore.Mvc;
using videogames.Data;
using videogames.Models;

namespace videogames.Controllers;

[ApiController]
[Route("User")]
public class UserController : ControllerBase
{
  private readonly DataContext _context;
  private static List<User>Users=new List<User>{};
  public UserController (DataContext context){
    _context = context;
}
//Obtengo todos los usuarios
[HttpGet]
[Route("GetAllUsers")]
public ActionResult<List<User>>Get(){
    return Ok(_context.Users);
}
}