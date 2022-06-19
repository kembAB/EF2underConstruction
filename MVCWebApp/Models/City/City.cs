using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MVCWebApp.Models.Country;
using MVCWebApp.Models.City;
namespace MVCWebApp.Models.City
{
    public class City
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string CityName { get; set; }
        [Required]
        public virtual Country.Country Country { get; set; }

        public virtual List<Person.personproperties> People { get; set; }

        public City()
        {
            People = new List<Person.personproperties>();
        }
    }
}
