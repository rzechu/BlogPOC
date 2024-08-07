using BlogPOC.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BlogPOC.UserAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private static readonly List<User> Users = new List<User>
        {
            new User { Id = 1, Name = "John Doe", Email = "john.doe@example.com" },
            new User { Id = 2, Name = "Jane Doe", Email = "jane.doe@example.com" }
        };

    [HttpGet]
    public ActionResult<IEnumerable<User>> GetUsers()
    {
        return Ok(Users);
    }

    [HttpGet("{id}")]
    public ActionResult<User> GetUser(int id)
    {
        var user = Users.Find(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPost]
    public ActionResult<User> PostUser(User user)
    {
        user.Id = Users.Count + 1;
        Users.Add(user);
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public IActionResult PutUser(int id, User user)
    {
        var existingUser = Users.Find(u => u.Id == id);
        if (existingUser == null)
        {
            return NotFound();
        }
        existingUser.Name = user.Name;
        existingUser.Email = user.Email;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        var user = Users.Find(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        Users.Remove(user);
        return NoContent();
    }
}