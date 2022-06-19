using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCWebApp.Models.City;
using MVCWebApp.EFwk;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCWebApp.Models.Country;
using MVCWebApp.Models.Person;
namespace MVCWebApp.Controllers
{
    public class CityController : Controller
    {
        public readonly personSeedDbContext _context;

        public CityController(personSeedDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            AllCitiesViewModel model = new AllCitiesViewModel();
            model.CityList = _context.Cities.OrderBy(c => c.Country).ToList();
            model.CountryList = new SelectList(_context.Countries, "CountryName", "CountryName");

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CreateCityViewModel CreateCityViewModel)
        {
            if (ModelState.IsValid)
            {
                if (_context.Countries.Find(CreateCityViewModel.CountryName).Cities.Find(c => c.CityName == CreateCityViewModel.CityName) == null)
                {
                    City city = new City();
                    city.CityName = CreateCityViewModel.CityName;
                    Country country = _context.Countries.Find(CreateCityViewModel.CountryName);

                    city.Country = country;
                    country.Cities.Add(city);

                    _context.Update(country);
                    _context.Add(city);
                    _context.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    //error message-> change to partial view?
                    return Content("City already exists");
                }
            }

            AllCitiesViewModel model = new AllCitiesViewModel();
            model.CityList = _context.Cities.OrderBy(c => c.Country).ToList();
            model.CountryList = new SelectList(_context.Countries, "CountryName", "CountryName");

            return View(nameof(Index), model);
        }

        public IActionResult Delete(int id)
        {
            City cityToDelete = _context.Cities.Find(id);

            if (cityToDelete != null)
            {
                foreach (personproperties person in cityToDelete.People)
                {
                    _context.People.Remove(person);
                }

                _context.Cities.Remove(cityToDelete);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
