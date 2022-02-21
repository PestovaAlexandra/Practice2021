using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Practice2021;

namespace Practice2021.Controllers
{
    public class SearchCampaignsController : Controller
    {
        public class Way
        {
            public int id { get; set; }
            public string name { get; set; }
        }
       
        private EventContext db = new EventContext();

        // GET: SearchCampaigns

        //public ActionResult 
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string submitButton, string inputSur, string inputName, string inputPat, string inputID, DateTime? inputDate = null)
        {
            switch(submitButton)
            {
                case "Показать":
                    {
                        if (Request.Form["WayOfShow"] == "NewMis")
                        {
                            ViewBag.Heading = "new";
                            return (OpenNew());
                        }
                        else
                        {
                            ViewBag.Heading = "all";
                            return (OpenAll());
                        }
                    }
                case "Найти":
                    {
                        IEnumerable<MissingPerson> people = null ;
                        if (inputID!="")
                        {
                            try
                            {
                                int id = Convert.ToInt32(inputID);
                                people = db.MissingPersons.Where(s => s.MissingPersonID == id);
                                ViewBag.People = people;
                                return View();
                            }
                            catch
                            {
                                return View();
                            }
                        }
                        else
                        {

                            if (inputSur != "" && inputName == "" && inputPat == "" && inputDate == null)
                            {
                                people = db.MissingPersons.Where(s => s.Surname == inputSur);
                            }
                            if (inputSur != "" && inputName != "" && inputPat == "" && inputDate == null)
                            {
                                people = db.MissingPersons.Where(s => s.Surname == inputSur && s.Name == inputName);

                            }
                            if (inputSur != "" && inputName == "" && inputPat != "" && inputDate == null)
                            {
                                people = db.MissingPersons.Where(s => s.Surname == inputSur && s.Patronymic == inputPat);

                            }
                            if (inputSur != "" && inputName == "" && inputPat == "" && inputDate != null)
                            {
                                people = db.MissingPersons.Where(s => s.Surname == inputSur && s.DateOfBirth == inputDate);

                            }
                            if (inputSur != "" && inputName != "" && inputPat == "" && inputDate != null)
                            {
                                people = db.MissingPersons.Where(s => s.Surname == inputSur && s.Name == inputName & s.DateOfBirth == inputDate);

                            }
                            if (inputSur != "" && inputName != "" && inputPat != "" && inputDate == null)
                            {
                                people = db.MissingPersons.Where(s => s.Surname == inputSur && s.Name == inputName && s.Patronymic == inputPat);

                            }
                            if (inputSur != "" && inputName != "" && inputPat != "" && inputDate != null)
                            {
                                people = db.MissingPersons.Where(s => s.Surname == inputSur && s.Name == inputName && s.Patronymic == inputPat && s.DateOfBirth == inputDate);
                            }
                            ViewBag.People = people;
                            if (people.Count() > 0)
                                ViewBag.Haeding = "search";
                            else ViewBag.Haeding = "no";
                            return View();
                        }
                        //IEnumerable<MissingPerson> people = db.MissingPersons.Where(s => s.New == input);
                        //
                        //
                    }
                default: return View();

            }
            
            
        }
        public ActionResult Start()
        {
            var searchCampaigns = db.SearchCampaigns.Include(s => s.Group).Include(s => s.MissingPerson).Include(s => s.Set);
            IEnumerable<MissingPerson> people = db.MissingPersons;
            ViewBag.People = people;
            ViewBag.Haeding = "all";
            return View();
        }
        private ActionResult OpenAll()
        {
            IEnumerable<MissingPerson> people = db.MissingPersons;
            ViewBag.People = people;
            return View();
        }

        private ActionResult OpenNew()
        {
            IEnumerable<MissingPerson> people = db.MissingPersons.Where(s => s.New == true);
            ViewBag.People = people;
            return View();
        }

        [HttpGet]
        public ActionResult Add(int ? id)
        {
            SearchCampaign one = new SearchCampaign();
            one.MissingPersonID = (int)id;
            return PartialView("Add", one);
        }
       
        [HttpPost]
        public ActionResult Add (SearchCampaign eventt , DateTime DateFound, int id)
        {
           
                eventt.DateAndTime = DateFound;
            

            Group newGroup = new Group();
            var countG = db.Groups.OrderByDescending(l=>l.GroupID).First().GroupID;
            newGroup.Name = "#"+(countG+1).ToString();
            db.Groups.Add(newGroup);

            Set newSet = new Set();
            var countS = db.Sets.OrderByDescending(l => l.SetID).First().SetID;
            newSet.Name = "#" + (countS + 1).ToString();
            db.Sets.Add(newSet);

            db.SaveChanges();
            eventt.MissingPersonID = id;
            eventt.GroupOfVolunteer = countG+1;
            eventt.SetOfEquipment = countS+1;
            

            db.SearchCampaigns.Add(eventt);

            //var person=db.MissingPersons.Where(u=>u.MissingPersonID==eventt.MissingPersonID&&u.New==true).FirstOrDefault();
            //person.New = false;
            //person.PhoneNumber=person.PhoneNumber.Trim();
            //person.Email = person.Email.Trim();

            db.SaveChanges();
            
            return View("Result");
        }
        public ActionResult SearchForEachMis(int id)
        {
            IEnumerable<SearchCampaign> all = db.SearchCampaigns.Where(s => s.MissingPersonID == id);
            ViewBag.Companies = all;
            return View("SearchForEachMis");
        }

        // GET: SearchCampaigns/Edit/5
        [HttpGet]
        public ActionResult Edit()
        {
            IEnumerable<SearchCampaign> searchCampaign = db.SearchCampaigns.Include(c => c.MissingPerson);
            ViewBag.People = searchCampaign;
            int count = searchCampaign.Count();
            if (searchCampaign == null)
            {
                return HttpNotFound();
            }
            List<string> list1 = new List<string>();
            list1.Add("ID мероприятия");
            list1.Add("ID пропавшего");

            ViewBag.List1 = list1;
            ViewBag.List2 = new SelectList(db.SearchCampaigns, "SearchCampaignID", "SearchCampaignID");

            ViewBag.Heading = "all";
            return View(searchCampaign);
        }
        
        // POST: SearchCampaigns/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(string submitButton, string selectedWay, string selectedID)
        {
            IEnumerable<SearchCampaign> a;
            //IEnumerable<Way> list1=null;
            List < string > list1= new List<string>();
            list1.Add("ID мероприятия");
            list1.Add("ID пропавшего");

           
            ViewBag.List1 = list1;
            ViewBag.List2 = new SelectList(db.SearchCampaigns, "SearchCampaignID", "SearchCampaignID");

            int id = Convert.ToInt32(selectedID);
            switch (submitButton)
            {
                case "Показать все мероприятия":
                    {
                        a= db.SearchCampaigns.Include(s => s.MissingPerson);
                        ViewBag.Heading = "all";
                        ViewBag.People = a;
                        return View(a);
                    }
                case "Найти по ID":
                    {
                        //try
                        //{
                       
                        if (selectedWay == "ID мероприятия")
                        {
                            a = db.SearchCampaigns.Include(s => s.MissingPerson).Where(c => c.SearchCampaignID == id);
                            ViewBag.Heading = "search";
                            ViewBag.People = a;
                        }
                        else
                        {
                           a = db.SearchCampaigns.Include(s => s.MissingPerson).Where(c => c.MissingPerson.MissingPersonID ==id);
                            ViewBag.Heading = "search";
                            ViewBag.People = a;
                        }
                        //if (a.Count() > 0)
                        //    ViewBag.Heading = "search";
                        //else
                        //    ViewBag.Heading = "no";
                        return View(a);
                        //}
                        //catch
                        //{

                        //}
                    }
                default:
                    {
                        return View();
                    }

            }
        }
        [HttpGet]
        public ActionResult GetItems(string  id)
        {
   
            List<int> list = new List<int>();

            if (id == "ID мероприятия")
                
                return PartialView(db.SearchCampaigns.ToList());
           
            else
                return PartialView(db.MissingPersons.ToList());

           
        }

        // Редактирование
        [HttpGet]
        public ActionResult Change(int ? id)
        {
            SearchCampaign camp = db.SearchCampaigns.Find(id);
            if (camp != null)
            {
                return PartialView("Change", camp);
            }
            return View()/*("Edit")*/;
        }
        [HttpPost]
        
        public ActionResult Change(SearchCampaign camp, DateTime DateFound)
        {
            var one =db.SearchCampaigns.Find(camp.SearchCampaignID);
            one.MeetingLocation = camp.MeetingLocation;
            one.DateAndTime = DateFound;
            
            db.SaveChanges();
            return View ("Change", one);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
