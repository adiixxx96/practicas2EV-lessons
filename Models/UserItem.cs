namespace videogames.Models;
    public class User
{
    public string Username{ get; set; }
    public string Password { get; set; }
    public bool Role { get; set; }
    public DateTime SignUpDate{ get; set; }
    public int Id{ get; set; }
    public User()
    {
    }
}