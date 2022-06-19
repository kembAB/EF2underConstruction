using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebApp.Models.Country
{
    public class CountryViewModel
    {
        
            [Required]
            [StringLength(40, MinimumLength = 2)]
            [Display(Name = "Country Name")]
            public string CountryName { get; set; }

            public CountryViewModel() { }
        
    }
}
