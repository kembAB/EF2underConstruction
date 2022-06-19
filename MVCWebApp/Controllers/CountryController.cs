using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCWebApp.Models.Person;
using MVCWebApp.Models.City;
using MVCWebApp.EFwk;
using MVCWebApp.Models.Country;
namespace MVCWebApp.Controllers
{
    public class CountryController : Controller
    {
        public readonly personSeedDbContext _context;

        public CountryController(personSeedDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            AllContriesViewModel model = new AllContriesViewModel();
            model.CountryList = _context.Countries.ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CountryViewModel CreateViewModel)
        {
            if (ModelState.IsValid)
            {
                if (_context.Countries.Find(CreateViewModel.CountryName) == null)
                {
                    Country country = new Country();
                    country.CountryName = CreateViewModel.CountryName;

                    _context.Countries.Add(country);
                    _context.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                   
                    return Content("Country already exists");
                }
            }

            AllContriesViewModel model = new AllContriesViewModel();
            model.CountryList = _context.Countries.ToList();

            return View(nameof(Index), model);
        }

        public IActionResult Delete(string id)
        {
            Country countryToDelete = _context.Countries.Find(id);

            if (countryToDelete != null)
            {
                foreach (City city in countryToDelete.Cities)
                {
                    foreach (personproperties person in city.People)
                    {
                        _context.People.Remove(person);
                    }

                    _context.Cities.Remove(city);
                }

                _context.Countries.Remove(countryToDelete);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
