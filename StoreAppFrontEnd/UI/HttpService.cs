using Models;
using System.Text.Json;
using System.Text;

namespace UI;
public class HttpService
{
    private readonly string _apiBaseURL = "https://localhost:7265/api/";
    private HttpClient client = new HttpClient();

    public HttpService()
    {
        client.BaseAddress = new Uri(_apiBaseURL);
    }


    public async Task<User> GetUserAsync(string username)
    {
        try
        {
            HttpResponseMessage response = await client.GetAsync($"User/{username}");
            response.EnsureSuccessStatusCode();
            string responseString = await response.Content.ReadAsStringAsync();
            if (responseString != null && responseString.Length > 0)
                return JsonSerializer.Deserialize<User>(responseString) ?? new User();
            else
                return null!;
        }
        catch (HttpRequestException)
        {
            throw;
        }
    }
}