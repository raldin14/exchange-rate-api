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
    public class ForeignCurrencyController : ControllerBase
    {
        public ForeignCurrencyService _foreignCurrencyService;

        public ForeignCurrencyController(ForeignCurrencyService foreignCurrencyService)
        {
            _foreignCurrencyService = foreignCurrencyService;
        }

        [HttpGet("get-all-foreign-currencies")]
        public IActionResult GetAllForeignCurrencies()
        {
            var allForeignCurrencies = _foreignCurrencyService.GetAllForeignCurrencies();
           
            return Ok(allForeignCurrencies);
        }        

        [HttpGet("get-all-transactions-by-user-and-iso_code/{id}/{iso_code}")]
        public IActionResult GetCurrencyByIsoCode(int id, string iso_code)
        {
            var allForeignCurrenciesByUser = _foreignCurrencyService.GetAllForeignCurrenciesByUserAndIso(id, iso_code);
            return Ok(allForeignCurrenciesByUser);
        }

        [HttpPost("add-foreign-currencies")]
        public IActionResult AddForeignCurrencies([FromBody] ForeignCurrencyVM foreignCurrency)
        {
            try
            {
                if (_foreignCurrencyService.AddForeignCurrency(foreignCurrency))
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Could not insert: either the "+ foreignCurrency.Iso_Code + " does not exist or user has already a value with this currency");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
