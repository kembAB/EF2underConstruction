using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebApp.Models.City
{
    public class CreateCityViewModel
    {
        
            [Required]
            [StringLength(40, MinimumLength = 2)]
            [Display(Name = "City Name")]
            public string CityName { get; set; }
            [Required]
            [Display(Name = "Country Name")]
            public string CountryName { get; set; }
        
    }
}
