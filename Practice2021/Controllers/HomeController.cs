using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;




namespace Practice2021.Controllers
{
    public class HomeController : Controller
    {
        EventContext db = new EventContext();
        Dictionary<MissingPerson, string> people = new Dictionary<MissingPerson, string>();

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.People = null;
             return View();
            
        }
        [HttpPost]
        public ActionResult Index(string submitButton, MissingPerson person)
        {
            switch (submitButton)
            {
                case "Искать в пропавших":
                    {
                        MissingPerson currntPerson = person;
                        IEnumerable<MissingPerson> all = db.MissingPersons.Where(s => s.Surname == currntPerson.Surname &&
                        s.Name == currntPerson.Name && s.Patronymic == currntPerson.Patronymic && s.DateOfBirth == currntPerson.DateOfBirth);
                       
                        foreach (var p in all)
                        {
                            if (p.DateFound == null)
                                people.Add(p, "Не найден");
                            else people.Add(p, "Найден");
                        }
                        ViewBag.People = people;
                        
                        if (all.Count() == 0)
                            ViewBag.Search = false;
                        else ViewBag.Search = true;
                       
                        return View("SearchInMis", person);
                    }
                case "Искать в найденных":
                    {
                        FoundStranger currntPerson= new FoundStranger();
                        currntPerson.Surname = person.Surname;
                        currntPerson.Name = person.Name;
                        currntPerson.Patronymic = person.Patronymic;
                        currntPerson.DateOfBirth = person.DateOfBirth;
                        IEnumerable<FoundStranger> all = db.FoundStrangers.Where(s => s.Surname == currntPerson.Surname &&
                        s.Name == currntPerson.Name && s.Patronymic == currntPerson.Patronymic && s.DateOfBirth == currntPerson.DateOfBirth);

                        ViewBag.People = all;
                        if (all.Count() == 0)
                            ViewBag.Search = false;
                        else ViewBag.Search = true;
                        
                        return View("SearchInFound", currntPerson);
                    }
                default:
                    // If they've submitted the form without a submitButton, 
                    // just return the view again.
                    return View("Index");
            }
        }
        [HttpGet]
        public ActionResult Index2()
        {
            ViewBag.People = null;
            return View();
        }
        [HttpPost]
        public ActionResult Index2(string submitButton, MissingPerson person)
        {
            switch (submitButton)
            {
                case "Искать в пропавших":
                    {
                        MissingPerson currntPerson = person;
                        IEnumerable<MissingPerson> all = db.MissingPersons.Where(s => s.Surname == currntPerson.Surname &&
                        s.Name == currntPerson.Name && s.Patronymic == currntPerson.Patronymic && s.DateOfBirth == currntPerson.DateOfBirth);

                        foreach (var p in all)
                        {
                            if (p.DateFound == null)
                                people.Add(p, "Не найден");
                            else people.Add(p, "Найден");
                        }
                        ViewBag.People = people;

                        if (all.Count() == 0)
                            ViewBag.Search = false;
                        else ViewBag.Search = true;

                        return View("SearchInMis", person);
                    }
                case "Искать в найденных":
                    {
                        FoundStranger currntPerson = new FoundStranger();
                        currntPerson.Surname = person.Surname;
                        currntPerson.Name = person.Name;
                        currntPerson.Patronymic = person.Patronymic;
                        currntPerson.DateOfBirth = person.DateOfBirth;
                        IEnumerable<FoundStranger> all = db.FoundStrangers.Where(s => s.Surname == currntPerson.Surname &&
                        s.Name == currntPerson.Name && s.Patronymic == currntPerson.Patronymic && s.DateOfBirth == currntPerson.DateOfBirth);

                        ViewBag.People = all;
                        if (all.Count() == 0)
                            ViewBag.Search = false;
                        else ViewBag.Search = true;

                        return View("SearchInFound", currntPerson);
                    }
                default:
                    // If they've submitted the form without a submitButton, 
                    // just return the view again.
                    return View("Index");
            }
        }
        //public string GetData()
        //{
        //    return JsonConvert.SerializeObject(people);
        //}

        public ActionResult Details(int id)
        {
            MissingPerson person = db.MissingPersons.Find(id);
            if (person != null)
            {
                return PartialView("Details", person);
            }
            return View("SearchInMis", "Home");
        }
    }
   
}