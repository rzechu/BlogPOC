using Microsoft.EntityFrameworkCore;
using Users.Core.Entities;

namespace Users.UserAPI.Persistence;

public class UserContext(DbContextOptions<UserContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
}