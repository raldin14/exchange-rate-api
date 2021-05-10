using ExchangeRate.Data.Models;
using ExchangeRate.Data.Services;
using ExchangeRate.Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ExchangeRate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        public ExchangeService _exchangeService;

        public CurrencyController(ExchangeService exchangeService)
        {
            _exchangeService = exchangeService;
        }

        public CurrencyController()
        {

        }

        [HttpGet("get-all-currencies")]
        public async Task<IActionResult> GetCurrencies()
        {
            var allCurrencies = _exchangeService.GetAllCurrenciesExchanges();
            bool is_Udated = true;
            ExchangeVM exchangeVM = new ExchangeVM();

            if (allCurrencies.Count > 0)
            {
                foreach (var exchange in allCurrencies)
                {
                    if (exchange.Today_Date != DateTime.Now)
                    {
                        is_Udated = false;
                        break;
                    }
                }
            }                

            if (allCurrencies.Count == 0 || is_Udated == false)
            {
                List<String> currencyResponse = new List<string>();
                using (var httpClient = new HttpClient())
                {
                    exchangeVM.ISO_Code = "USD";
                    using (var response = await httpClient.GetAsync("https://www.bancoprovincia.com.ar/Principal/Dolar"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        currencyResponse = JsonConvert.DeserializeObject<List<String>>(apiResponse);
                    }
                }

                foreach (var exchange in currencyResponse)
                {
                    if (exchangeVM.Purchase == 0)
                    {
                        exchangeVM.Purchase = float.Parse(exchange);
                    }
                    else if (exchangeVM.Sale == 0)
                    {
                        exchangeVM.Sale = float.Parse(exchange);
                    }
                }

                if (!is_Udated)
                {
                    exchangeVM.Today_Date = DateTime.Now;
                    UpdateCurrencyByIsoCode(exchangeVM.ISO_Code, exchangeVM);
                }
                else
                {
                    AddExchangeRate(exchangeVM);
                }

                exchangeVM.ISO_Code = "BRL";
                exchangeVM.Purchase = exchangeVM.Purchase / 4;
                exchangeVM.Sale = exchangeVM.Sale / 4;

                if (!is_Udated)
                {
                    exchangeVM.Today_Date = DateTime.Now;
                    UpdateCurrencyByIsoCode(exchangeVM.ISO_Code, exchangeVM);
                }
                else
                {
                    AddExchangeRate(exchangeVM);
                }

                allCurrencies = _exchangeService.GetAllCurrenciesExchanges();
            }

            return Ok(allCurrencies);
        }        

        [HttpGet("get-currency-by-iso-code/{iso_code}")]
        public IActionResult GetCurrencyByIsoCode(string iso_code)
        {
            try
            {
                var currency = _exchangeService.GetCurrencyExchange(iso_code);

                if(currency != null)
                {
                    return Ok(currency);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost("add-exchange")]
        public IActionResult AddExchangeRate([FromBody] ExchangeVM exchange)
        {
            _exchangeService.AddExchangeRate(exchange);
            return Ok();
        }

        [HttpPut("update-by-iso-code/{iso_code}")]
        public IActionResult UpdateCurrencyByIsoCode(string iso_code, [FromBody] ExchangeVM exchange)
        {
            var currency = _exchangeService.UpdateCurrencyExchange(iso_code, exchange);
            return Ok(currency);
        }
    }
}
