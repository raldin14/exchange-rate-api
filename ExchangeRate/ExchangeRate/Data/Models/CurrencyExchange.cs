using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRate.Data.Models
{
    public class CurrencyExchange
    {
        public int Id { get; set; }

        public string ISO_Code { get; set; }

        public float Purchase { get; set; }

        public float Sale { get; set; }

        public DateTime Today_Date { get; set; }

        //Navigation Properties
        //public List<Transaction> Transactions { get; set; }
    }
}
