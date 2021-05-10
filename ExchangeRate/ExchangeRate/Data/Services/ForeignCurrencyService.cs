using ExchangeRate.Data.Models;
using ExchangeRate.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRate.Data.Services
{
    public class ForeignCurrencyService
    {
        private AppDbContext _context;

        public ForeignCurrencyService(AppDbContext context)
        {
            _context = context;
        }

        public Boolean AddForeignCurrency(ForeignCurrencyVM foreignCurrency)
        {           

            try
            {
                var checkCurrency = GetCurrencyExchange(foreignCurrency.Iso_Code.ToString());

                if (checkCurrency != null)
                {
                    var checkIfExist = GetAllForeignCurrenciesByUserAndIso(foreignCurrency.UserId, foreignCurrency.Iso_Code);

                    if (checkIfExist == null)
                    {
                        if (string.IsNullOrEmpty(foreignCurrency.UserId.ToString()))
                        {
                            foreignCurrency.UserId = 0;
                        }

                        var _foreignCurrency = new ForeignCurrency()
                        {
                            UserId = foreignCurrency.UserId,
                            Monthly_Amount = foreignCurrency.Monthly_Amount,
                            Iso_Code = foreignCurrency.Iso_Code,
                            Date = DateTime.Now.Date
                        };

                        _context.ForeignCurrencies.Add(_foreignCurrency);
                        _context.SaveChanges();

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }               
            }
            catch (Exception)
            {
                throw;
            }            
        }

        public CurrencyExchange GetCurrencyExchange(string iso_Code) => _context.CurrenciesExchange.FirstOrDefault(n => n.ISO_Code == iso_Code);

        public List<ForeignCurrency> GetAllForeignCurrencies() => _context.ForeignCurrencies.ToList();

        public ForeignCurrency GetAllForeignCurrenciesByUserAndIso(int id, string iso_code)
        {
            var getAllForeignCurrencies = _context.ForeignCurrencies.Where(n => n.Iso_Code == iso_code);

            if(getAllForeignCurrencies.Any())
            {
                var getForeignCurrencies = getAllForeignCurrencies.Where(n => n.UserId == id);

                if (!getForeignCurrencies.Any())
                {
                    getAllForeignCurrencies = getAllForeignCurrencies.Where(n => n.UserId == 0);
                }
                else
                {
                    return getForeignCurrencies.FirstOrDefault();
                }

                return getAllForeignCurrencies.FirstOrDefault();
            }

            return getAllForeignCurrencies.FirstOrDefault();
        }
    }
}
