using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using ConsultCountries.Domain;

namespace ConsultCountries.Controllers
{
    [Route("api/[controller]")]
    public class CountriesController : ControllerBase
    {
        private readonly IMemoryCache MemoryCache;
        private const string COUNTRIES_KEY = "countries";
        private const string restCountriesUrl = "https://restcountries.com/v3.1/all";

        public CountriesController(IMemoryCache memoryCache) => MemoryCache = memoryCache;

        [HttpGet()]
        public async Task<IActionResult> GetCountries()
        {
            if (MemoryCache.TryGetValue(COUNTRIES_KEY, out object countriesObject))
            {
                return Ok(countriesObject);
            }
            else
            {
                HttpResponseMessage? response = await new HttpClient().GetAsync(restCountriesUrl);

                string? responseData = await response.Content.ReadAsStringAsync();

                List<Country>? countries = JsonConvert.DeserializeObject<List<Country>>(responseData);

                var memoryCacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3600),
                    SlidingExpiration = TimeSpan.FromSeconds(1200)
                };

                MemoryCache.Set(COUNTRIES_KEY, countries, memoryCacheEntryOptions);

                return Ok(countries);
            }

        }
    }
}
