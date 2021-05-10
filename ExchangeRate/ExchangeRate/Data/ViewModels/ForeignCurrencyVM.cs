using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRate.Data.ViewModels
{
    public class ForeignCurrencyVM
    {
        public int UserId { get; set; }

        public float Monthly_Amount { get; set; }

        public string Iso_Code { get; set; }

        public DateTime Date { get; set; }
    }
}
