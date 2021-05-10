using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRate.Data.ViewModels
{
    public class TransactionVM
    {
        public int UserId { get; set; }

        public float Monto { get; set; }

        public float Exchange_rate { get; set; }

        public string Iso_Code { get; set; }

        public DateTime Purchased_Date { get; set; }
    }
}
