using Models;
using System.Text.Json;
using System.Text;

namespace UI;
public class HttpService
{
    private readonly string _apiBaseURL = "https://localhost:7223/api/";
    private HttpClient client = new HttpClient();

    public HttpService()
    {
        client.BaseAddress = new Uri(_apiBaseURL);
    }
    

    public async Task<User> GetUserAsync(User userToGet)
    {
        string serializedUser = JsonSerializer.Serialize(userToGet);
        StringContent content = new StringContent(serializedUser, Encoding.UTF8, "application/json");
        try
        {
            HttpResponseMessage response = await client.GetAsync(userToGet.UserName);
            response.EnsureSuccessStatusCode();
            return await JsonSerializer.DeserializeAsync<User>(await response.Content.ReadAsStreamAsync()) ?? new User();
        }
        catch(HttpRequestException)
        {
            throw;
        }
    }
}