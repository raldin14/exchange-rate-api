using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRate.Data.ViewModels
{
    public class ExchangeVM
    {
        public string ISO_Code { get; set; }

        public float Purchase { get; set; }

        public float Sale { get; set; }

        public DateTime Today_Date { get; set; }
    }
}
