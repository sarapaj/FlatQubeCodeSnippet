﻿using FlatQubeCodeSnippet.Models;
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

        #region Cmc

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
        #endregion

        #region Currencies
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
        #endregion

        #region Pairs

        /// <summary>
        /// Get all Pairs data.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("pairs")]
        public async Task<IActionResult> Pairs([FromBody]JsonElement body)
        {
            try
            {
                string json = System.Text.Json.JsonSerializer.Serialize(body);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                string apiEndpoint = ConstructRightUrl(_liveApiUrl, $"pairs");
                HttpResponseMessage response = await _httpClient.PostAsync(apiEndpoint, data);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<PairModel>(jsonResponse);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get pair data info by lp address.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("pairs/address/{address}")]
        public async Task<IActionResult> PairsAddress_Address(string address)
        {
            try
            {
                string apiEndpoint = ConstructRightUrl(_liveApiUrl, $"pairs/address/{address}");
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync(apiEndpoint, address);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<PairModel>(jsonResponse);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get cross pairs data.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("pairs/cross_pairs")]
        public async Task<IActionResult> PairsCrossPairs([FromBody] JsonElement body)
        {
            try
            {
                string json = System.Text.Json.JsonSerializer.Serialize(body);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                string apiEndpoint = ConstructRightUrl(_liveApiUrl, $"pairs/cross_pairs");
                HttpResponseMessage response = await _httpClient.PostAsync(apiEndpoint, data);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<PairModel>(jsonResponse);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get pair data info by token root addresses. 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("pairs/left/{left}/right/{right}")]
        public async Task<IActionResult> PairsLeftRight(string left, string right)
        {
            try
            {
                string apiEndpoint = ConstructRightUrl(_liveApiUrl, $"pairs/left/{left}/right/{right}");
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync(apiEndpoint, left);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Pair>(jsonResponse);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Get ohlcv pair data info by token root addresses.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("pairs/left/{left}/right/{right}/ohlcv")]
        public async Task<IActionResult> PairsLeftRight_Ohlcv(string left, string right, [FromBody] JsonElement body)
        {
            try
            {
                string json = System.Text.Json.JsonSerializer.Serialize(body);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                string apiEndpoint = ConstructRightUrl(_liveApiUrl, $"pairs/left/{left}/right/{right}/ohlcv");
                HttpResponseMessage response = await _httpClient.PostAsync(apiEndpoint, data);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<PairOhlcvModel>>(jsonResponse);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get ohlcv pair data info by lp address.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("pairs/address/{address}/ohlcv")]
        public async Task<IActionResult> PairsAddress_Ohlcv(string address, [FromBody] JsonElement body)
        {
            try
            {
                string json = System.Text.Json.JsonSerializer.Serialize(body);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                string apiEndpoint = ConstructRightUrl(_liveApiUrl, $"pairs/address/{address}/ohlcv");
                HttpResponseMessage response = await _httpClient.PostAsync(apiEndpoint, data);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<PairOhlcvModel>>(jsonResponse);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get pair volume data info.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("pairs/address/{address}/volume")]
        public async Task<IActionResult> PairsAddress_Volume(string address, [FromBody] JsonElement body)
        {
            try
            {
                string json = System.Text.Json.JsonSerializer.Serialize(body);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                string apiEndpoint = ConstructRightUrl(_liveApiUrl, $"pairs/address/{address}/volume");
                HttpResponseMessage response = await _httpClient.PostAsync(apiEndpoint, data);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<PairDataModel>>(jsonResponse);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Get pair tvl data info.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("pairs/address/{address}/tvl")]
        public async Task<IActionResult> PairsAddress_Tvl(string address, [FromBody] JsonElement body)
        {
            try
            {
                string json = System.Text.Json.JsonSerializer.Serialize(body);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                string apiEndpoint = ConstructRightUrl(_liveApiUrl, $"pairs/address/{address}/tvl");
                HttpResponseMessage response = await _httpClient.PostAsync(apiEndpoint, data);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<PairDataModel>>(jsonResponse);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion

        private string ConstructRightUrl(string environment, string apiPathSuffix)
        {
            return environment + "/" + apiPathSuffix;
        }

    }
}
