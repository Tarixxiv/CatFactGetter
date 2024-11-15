namespace CatFactGetter.Services;

public interface IApiService
{
    public Task<string> GetFact();
}