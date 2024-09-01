namespace Blog.Application.Interfaces;

public interface IUserService
{
    Task<User> GetUserByIdAsync(int id);
}