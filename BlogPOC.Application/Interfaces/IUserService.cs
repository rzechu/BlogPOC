namespace BlogPOC.Application.Interfaces;

public interface IUserService
{
    Task<User> GetUserByIdAsync(int id);
}