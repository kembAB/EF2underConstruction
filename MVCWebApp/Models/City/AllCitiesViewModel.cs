using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebApp.Models.City
{
    public class AllCitiesViewModel
    {
        public IEnumerable<City> CityList { get; set; }
        public CreateCityViewModel CreateCityViewModel { get; set; }
        public SelectList CountryList { get; set; }
    }
}
