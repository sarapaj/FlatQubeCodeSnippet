using FlatQubeCodeSnippet.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FlatQubeCodeSnippet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlatQubeController : Controller
    {
        private readonly string _liveApiUrl = "https://ton-swap-indexer.broxus.com/v1";
        private readonly string _testApiUrl = "https://ton-swap-indexer-test.broxus.com/v1";
        private readonly HttpClient _httpClient = new HttpClient();

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Get dex pools info.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("CmcDex")]
        public async Task<IActionResult> CmcDex()
        {
            string apiEndpoint = ConstructRightUrl(_liveApiUrl, $"cmc/dex");

            try
            {
                var response = Task.Run(() => _httpClient.GetAsync(apiEndpoint));
                response.Wait();

                if (response.Result.IsSuccessStatusCode)
                {
                    var responseStream = response.Result.Content.ReadAsStringAsync();
                    responseStream.Wait();

                    var result = JsonConvert.DeserializeObject<Dictionary<string, AdditionalPropertiesModel>>(responseStream.Result);
                    return Ok(result);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        /// <summary>
        /// Get farming pools info.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("CmcFarming")]
        public async Task<IActionResult> CmcFarming()
        {
            string apiEndpoint = ConstructRightUrl(_liveApiUrl, $"cmc/farming");

            try
            {
                var response = Task.Run(() => _httpClient.GetAsync(apiEndpoint));
                response.Wait();

                if (response.Result.IsSuccessStatusCode)
                {
                    var responseStream = response.Result.Content.ReadAsStringAsync();
                    responseStream.Wait();

                    var result = JsonConvert.DeserializeObject<FarmingModel>(responseStream.Result);
                    return Ok(result);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Get currency data info by token root address.
        /// </summary>
        /// <param name="currency">Currency address</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Currencies")]
        public async Task<IActionResult> Currencies(string currency)
        {
            try
            {
                string apiEndpoint = ConstructRightUrl(_liveApiUrl, $"currencies/" + currency);
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync(apiEndpoint, currency);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<CurrencyModel>(jsonResponse);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get currencies prices by token root address.
        /// </summary>
        /// <param name="currency_addresses">Currency address taken from body</param>
        /// <returns></returns>
        [HttpPost]
        [Route("CurrenciesUsdtPrices")]
        public async Task<IActionResult> CurrenciesUsdtPrices([FromBody]JsonElement currency_addresses)
        {
            try
            {
                string json = System.Text.Json.JsonSerializer.Serialize(currency_addresses);

                var data = new StringContent(json, Encoding.UTF8, "application/json");

                string apiEndpoint = ConstructRightUrl(_liveApiUrl, $"currencies_usdt_prices");
                HttpResponseMessage response = await _httpClient.PostAsync(apiEndpoint, data);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonResponse);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private string ConstructRightUrl(string environment, string apiPathSuffix)
        {
            return environment + "/" + apiPathSuffix;
        }

    }
}
