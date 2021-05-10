using ExchangeRate.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ExchangeRate.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            Exchange exchange = new Exchange();

            float purchase = 0, sale = 0;
            string iso_Code = "";

            //GetCurrencies();

            async void GetCurrencies()
            {
                List<String> currencyResponse = new List<string>();
                using (var httpClient = new HttpClient())
                {

                    using (var response = await httpClient.GetAsync("https://www.bancoprovincia.com.ar/Principal/Dolar"))
                    {
                        iso_Code = "USD";

                        string apiResponse = await response.Content.ReadAsStringAsync();
                        currencyResponse = JsonConvert.DeserializeObject<List<String>>(apiResponse);
                    }
                }
                
                foreach(var exchange in currencyResponse)
                {
                    if(purchase == 0)
                    {
                        purchase = float.Parse(exchange);
                    }else if(sale == 0)
                    {
                        sale = float.Parse(exchange);
                    }
                }

                using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                    if (!context.CurrenciesExchange.Any())
                    {

                        context.CurrenciesExchange.AddRange(new CurrencyExchange()
                        {
                            ISO_Code = iso_Code,
                            Purchase = purchase,
                            Sale = sale,
                            Today_Date = DateTime.Now
                        });

                        if (purchase != 0 && sale != 0 && iso_Code != "")
                        {
                            context.SaveChanges();
                        }
                    }
                }
            }
        }
    }
}
