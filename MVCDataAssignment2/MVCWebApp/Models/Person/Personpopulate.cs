using MVCWebApp.Models.Person.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebApp.Models.Person
{
    public class Personpopulate:Iperson
    {
        private static int idCounter = 0;
        private static List<personproperties> PersonList = new List<personproperties>();

        public Personpopulate()
        {
            personproperties p1 = new personproperties { Name = "Abel Haile", City = "Addis Ababa", PhoneNumber = "123456789", ID = GetNewID() };
            personproperties p2 = new personproperties { Name = "Billy Walters ", City = "Las Vegas", PhoneNumber = "345678904", ID = GetNewID() };

            PersonList.Add(p1);
            PersonList.Add(p2);
        }

        public List<personproperties> GetAllPersons()
        {
            return PersonList;
        }

        public personproperties GetPerson(int id)
        {
            return PersonList.SingleOrDefault(c => c.ID == id);
        }

        public List<personproperties> Search(string searchTerm, bool caseSensitive)
        {
            List<personproperties> searchList = new List<personproperties>();

            if (searchTerm != null)
            {
                foreach (personproperties item in PersonList)
                {
                    if (caseSensitive)
                    {
                        if (item.Name.Contains(searchTerm) || item.City.Contains(searchTerm))
                        {
                            searchList.Add(item);
                        }
                    }
                    else
                    {
                        if (item.Name.ToLowerInvariant().Contains(searchTerm.ToLowerInvariant()) ||
                            item.City.ToLowerInvariant().Contains(searchTerm.ToLowerInvariant()))
                        {
                            searchList.Add(item);
                        }
                    }

                }
            }
            return searchList;
        }

        public List<personproperties> Sort(PersonReorderVIewModel sortOptions, string sortType)
        {
            
            List<personproperties> sortedList = PersonList.OrderBy(p => p.ID).ToList();

            if (sortType == "city")
            {
                sortedList = PersonList.OrderBy(p => p.City).ToList();
            }
            else if (sortType == "name")
            {
                sortedList = PersonList.OrderBy(p => p.Name).ToList();
            }

            if (sortOptions.ReverseAplhabeticalOrder == true)
            {
                sortedList.Reverse();
            }

            return sortedList;
        }

        public personproperties Add(CreatePersonViewModel createPersonViewModel)
        {
            personproperties person = new personproperties();
            person.Name = createPersonViewModel.Name;
            person.City = createPersonViewModel.City;
            person.PhoneNumber = createPersonViewModel.PhoneNumber;
            person.ID = GetNewID();

            PersonList.Add(person);

            return person;
        }

        public bool Delete(int id)
        {
            if (id > 0)
            {
                personproperties personToRemove = null;

                foreach (personproperties item in PersonList)
                {
                    if (item.ID == id)
                    {
                        personToRemove = item;
                    }
                }

                if (personToRemove != null)
                {
                    PersonList.Remove(personToRemove);
                    return true;
                }
            }

            return false;
        }

        private int GetNewID()
        {
            idCounter++;
            return idCounter;
        }

    }
}
