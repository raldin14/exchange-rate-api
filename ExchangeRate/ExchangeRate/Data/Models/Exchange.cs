using ExchangeRate.Data.Services;
using ExchangeRate.Data.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ExchangeRate.Data.Models
{
    public class Exchange
    {
        float purchase = 0, sale = 0;
        string iso_Code = "";
        public ExchangeService _exchangeService;
        ExchangeVM exchangeVM = new ExchangeVM();

        public Exchange(ExchangeService exchangeService)
        {
            _exchangeService = exchangeService;
        }

        public Exchange()
        {
            /*Purchase = purchase;
            Sale = sale;
            ISO_Code = iso_Code;*/
        }

        public float Purchase { 
            get { return purchase; } 
            set { purchase = value; } 
        }

        public float Sale
        {
            get { return sale; }
            set { sale = value; }
        }

        public string ISO_Code
        {
            get { return iso_Code; }
            set { iso_Code = value; }
        }

        public async void GetCurrencies()
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

            foreach (var exchange in currencyResponse)
            {
                if (purchase == 0)
                {
                    purchase = float.Parse(exchange);
                }
                else if (sale == 0)
                {
                    sale = float.Parse(exchange);
                }
            }

            /*exchangeVM.ISO_Code = ISO_Code;
            exchangeVM.Purchase = Purchase;
            exchangeVM.Sale = Sale;

            _exchangeService.AddExchangeRate(exchangeVM);

            GetCurrencyFromUSD("BRL", Purchase, Sale);*/
        }

        public void GetCurrencyFromUSD(string iso_code, float purchase, float sale)
        {
            if (iso_code == "BRL")
            {
                exchangeVM.ISO_Code = iso_code;
                exchangeVM.Purchase = purchase/4;
                exchangeVM.Sale = sale/4;
            }

            _exchangeService.AddExchangeRate(exchangeVM);
        }
    }
}
