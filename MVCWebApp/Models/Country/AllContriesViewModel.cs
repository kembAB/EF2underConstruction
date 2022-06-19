using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebApp.Models.Country
{
    public class AllContriesViewModel
    {
        

        public IEnumerable<Country> CountryList { get; set; }

        public CountryViewModel CreateViewModel { get; set; }
    }
}

