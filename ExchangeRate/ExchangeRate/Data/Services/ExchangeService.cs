using ExchangeRate.Data.Models;
using ExchangeRate.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRate.Data.Services
{
    public class ExchangeService
    {
        private AppDbContext _context;

        public ExchangeService()
        {
        }

        public ExchangeService(AppDbContext context)
        {
            _context = context;
        }

        public void AddExchangeRate(ExchangeVM exchange)
        {
            var _exchange = new CurrencyExchange()
            {
                ISO_Code = exchange.ISO_Code,
                Purchase = exchange.Purchase,
                Sale = exchange.Sale,
                Today_Date = DateTime.Now
            };

            _context.CurrenciesExchange.Add(_exchange);
            _context.SaveChanges();
        }

        public List<CurrencyExchange> GetAllCurrenciesExchanges() => _context.CurrenciesExchange.ToList();

        public CurrencyExchange GetCurrencyExchange(string iso_Code)
        {
            try
            {
                 var currency = _context.CurrenciesExchange.FirstOrDefault(n => n.ISO_Code == iso_Code);

                return currency;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public CurrencyExchange UpdateCurrencyExchange(string iso_Code, ExchangeVM exchange)
        {
            var _currency = _context.CurrenciesExchange.FirstOrDefault(n => n.ISO_Code == iso_Code);

            if(_currency != null)
            {
                _currency.ISO_Code = exchange.ISO_Code;
                _currency.Purchase = exchange.Purchase;
                _currency.Sale = exchange.Sale;
                _currency.Today_Date = exchange.Today_Date;

                _context.SaveChanges();
            }

            return _currency;
        }
    }
}
