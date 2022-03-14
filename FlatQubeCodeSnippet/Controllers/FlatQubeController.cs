using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
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

                    var result = JsonConvert.DeserializeObject<Dictionary<string, AdditionalProp>>(responseStream.Result);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return View();
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

                    var result = JsonConvert.DeserializeObject<Farming>(responseStream.Result);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return View();
        }

        private string ConstructRightUrl(string environment, string apiPathSuffix)
        {
            return environment + "/" + apiPathSuffix;
        }


        /*         RESPONSE MODELS         */
        private class AdditionalProp
        {
            public string base_id { get; set; }
            public string base_name { get; set; }
            public string base_symbol { get; set; }
            public string base_volume { get; set; }
            public string last_price { get; set; }
            public string quote_id { get; set; }
            public string quote_name { get; set; }
            public string quote_symbol { get; set; }
            public string quote_volume { get; set; }
        }

        private class Farming
        {
            public List<Links> links { get; set; }
            public List<Pools> pools { get; set; }  
            public string provider { get; set; }
            public string provider_URL { get; set; }
            public string provider_logo { get; set; }
        }

        private class Links
        {
            public string link { get; set; }
            public string title { get; set; }
        }

        private class Pools
        {
            public string apr { get; set; }
            public string logo { get; set; }
            public string name { get; set; }
            public string pair { get; set; }
            public string pairLink { get; set; }
            public List<string> poolRevards { get; set; }
            public string totalStake { get; set; }
        }
    }
}
