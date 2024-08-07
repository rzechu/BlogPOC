namespace BlogPOC.Application.Services;

public class UserService : IUserService
{
    private readonly IHttpClientFactory _httpClient;

    public UserService(IHttpClientFactory httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        return await _httpClient.CreateClient(Core.Constants.USERAPI)
            .GetFromJsonAsync<User>($"http://blogpoc.userapi/api/users/{id}");
    }
}