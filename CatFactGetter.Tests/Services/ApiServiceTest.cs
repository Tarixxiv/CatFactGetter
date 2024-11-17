using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CatFactGetter.Services;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace CatFactGetter.Tests.Services;

[TestSubject(typeof(ApiService))]
public class ApiServiceTest
{
    [Fact]
    public async Task GetFactReturnsFactAndLength()
    {
        //arrange
        var inMemorySettings = new Dictionary<string, string>
        {
            { "ApiUrl", "https://catfact.ninja/fact" }
        };
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();
        var controller = new ApiService(configuration, new HttpClient());
        
        //act
        var fact = await controller.GetFact();
        
        //assert
        Assert.NotNull(fact);
    }

    [Fact]
    public async Task GetFactWithIncorrectApiUrlThrowsHttpRequestException()
    {
        //arrange
        var inMemorySettings = new Dictionary<string, string>
        {
            { "ApiUrl", "https://invalidApi.ninja/fact" }
        };
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();
        var wrongConfigController = new ApiService(configuration, new HttpClient());
        
        //act
        async Task Act() => await wrongConfigController.GetFact();

        //assert
        await Assert.ThrowsAsync<HttpRequestException>(Act);
    }
}