using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebApp.Models.Country
{
    public class Country
    {
        [Key]
        public string CountryName { get; set; }
        public virtual List<City.City> Cities { get; set; }

        public Country()
        {
            Cities = new List<City.City>();
        }
    }
}
