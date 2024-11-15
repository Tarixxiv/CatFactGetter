using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Linq;

namespace CatFactGetter.Services;

public class ApiService(IConfiguration configuration, HttpClient client): IApiService
{
    public async Task<string> GetFact()
    {
        try
        {
            var response = await client.GetAsync(configuration["ApiUrl"]);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            ValidateFact(content);
            return content;
        }
        catch (Exception e) when (e is ValidationException or HttpRequestException)
        {
            throw;
        }
        catch (Exception e)
        {
            throw new HttpRequestException("Could not reach the API.", e);
        }
    }

    private static void ValidateFact(string fact)
    {
        try
        {
            var json = JObject.Parse(fact);
            if (json["fact"]!.ToString().Length != json["length"]!.Value<int>())
                throw new ValidationException();
        }
        catch (Exception e)
        {
            throw new ValidationException("Invalid response",e);
        }
    }
}