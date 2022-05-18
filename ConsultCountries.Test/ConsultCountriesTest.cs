using ConsultCountries.Domain;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace ConsultCountries.Test
{
    public class ConsultCountriesTest
    {
        private readonly WebApplicationFactory<Program>? _server;

        public ConsultCountriesTest()
        {
            _server = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => { builder.ConfigureServices(services => { }); });
            HttpClient? _client = _server?.CreateClient();
        }

        [Fact]
        public async Task GivenWrongHttpResponseMessageItShouldReturnNotFound()
        {
            HttpResponseMessage? response = await new HttpClient().GetAsync("https://restcountries.com/v3.1/name/");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GivenCorrectHttpResponseMessageItShouldReturnOk()
        {
            HttpResponseMessage? response = await new HttpClient().GetAsync("https://restcountries.com/v3.1/all");

            string? responseData = await response.Content.ReadAsStringAsync();

            var obj = JsonConvert.DeserializeObject<List<Country>>(responseData);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(obj);

        }

    }
}