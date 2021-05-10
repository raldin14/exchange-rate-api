using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRate.Data.Models
{
    public class ForeignCurrency
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        public float Monthly_Amount { get; set; }

        public string Iso_Code { get; set; }

        public DateTime Date { get; set; }
    }
}
