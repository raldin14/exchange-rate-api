using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRate.Data.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        //public User User { get; set; }

        public float Monto { get; set; }

        public float Exchange_rate { get; set; }

        public string Iso_Code { get; set; }

        //public int ExchangeId { get; set; }

        public DateTime Purchased_Date { get; set; }
    }
}
