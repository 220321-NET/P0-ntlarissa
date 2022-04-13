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

    public async Task<User> createNewUser(User userToCreate)
    {
        string serializedUser = JsonSerializer.Serialize(userToCreate);
        StringContent content = new StringContent(serializedUser, Encoding.UTF8, "application/json");
        try
        {
            HttpResponseMessage response = await client.PostAsync("User", content);
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
    public async Task<List<Product>> GetAllProductAsync()
    {
        try
        {
            HttpResponseMessage response = await client.GetAsync("Product");
            response.EnsureSuccessStatusCode();
            string responseString = await response.Content.ReadAsStringAsync();
            if (responseString != null && responseString.Length > 0)
                return JsonSerializer.Deserialize<List<Product>>(responseString) ?? new List<Product>();
            else
                return null!;
        }
        catch (HttpRequestException)
        {
            throw;
        }
    }
}