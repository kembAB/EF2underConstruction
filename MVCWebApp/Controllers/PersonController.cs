using Microsoft.AspNetCore.Mvc;
using MVCWebApp.Models.Person;
using MVCWebApp.Models.Person.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCWebApp.EFwk;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVCWebApp.Controllers
{
    public class PersonController : Controller
    {
        private readonly Iperson _personRepository;
        public readonly personSeedDbContext _context;

        public PersonController(personSeedDbContext context, Iperson personRepository)
        {
            _context = context;
            _personRepository = personRepository;
        }

        public IActionResult Index()
        {
            AllPersonViewModel model = new AllPersonViewModel();
            model.PersonList = _personRepository.GetAllPersons();
            model.CityList = new SelectList(_context.Cities.OrderBy(c => c.CityName), "ID", "CityName");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreatePersonViewModel CreateViewModel)
        {
            if (ModelState.IsValid)
            {

                _personRepository.Add(CreateViewModel);

                return RedirectToAction(nameof(Index));
            }

            AllPersonViewModel model = new AllPersonViewModel();
            model.PersonList = _personRepository.GetAllPersons();
            model.CityList = new SelectList(_context.Cities, "ID", "CityName");

            return View(nameof(Index), model);
        }

        public IActionResult Delete(int id)
        {
            _personRepository.Delete(id);

            AllPersonViewModel model = new AllPersonViewModel();
            model.PersonList = _personRepository.GetAllPersons();
            model.CityList = new SelectList(_context.Cities, "CityName", "CityName");

            return View(nameof(Index), model);
        }

        [HttpGet]
        public IActionResult Search(PersonSearchViewModel searchOptions)
        {

            AllPersonViewModel model = new AllPersonViewModel();
            model.PersonList = _personRepository.Search(searchOptions.SearchTerm, searchOptions.CaseSensitive);
            model.CityList = new SelectList(_context.Cities, "CityName", "CityName");

            return View(nameof(Index), model);
        }

        [HttpGet]
        public IActionResult SortByCity(PersonReorderVIewModel sortOptions)
        {
            AllPersonViewModel model = new AllPersonViewModel();

            model.PersonList = _personRepository.Sort(sortOptions, "city");
            model.CityList = new SelectList(_context.Cities, "CityName", "CityName");

            return View(nameof(Index), model);
        }

        [HttpGet]
        public IActionResult SortByName(PersonReorderVIewModel sortOptions)
        {
            AllPersonViewModel model = new AllPersonViewModel();

            model.PersonList = model.PersonList = _personRepository.Sort(sortOptions, "name");
            model.CityList = new SelectList(_context.Cities, "CityName", "CityName");

            return View(nameof(Index), model);
        }
    }
}
