using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRate.Data.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        //Navigation Properties
        //public List<Transaction> Transactions { get; set; }
    }
}
