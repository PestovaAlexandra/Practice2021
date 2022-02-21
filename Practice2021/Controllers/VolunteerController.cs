using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Practice2021;


namespace Practice2021.Controllers
{
    public class VolunteerController : Controller
    {
        private EventContext db = new EventContext();

        // GET: Volunteer
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Items = db.SearchCampaigns.Include(c => c.MissingPerson).Where(s => s.DateAndTime > DateTime.Now);
            ViewBag.Heading = "all";
            return View("Index");
        }
        [HttpGet]
        public ActionResult Information(int? idG)
        {

            var inf = db.StaffVolunteers.Where(s=>s.SuperUser==0);
            var a = inf.Count();
            ViewBag.Volunteers = new SelectList(db.StaffVolunteers.Where(s => s.SuperUser == 0), "VolunteerID", "VolunteerID");
            ViewBag.One = inf.First();
            return View("Information");
        }
        [HttpGet]
        public string Details(int ? id)
        {
            IEnumerable<StaffVolunteer> inf = db.StaffVolunteers.Where(c => c.VolunteerID == id);
            int a = inf.Count();
            string str = "";
            foreach (var item in inf)
            {
                str = $@" ФИО: {item.Surname} {item.Name} {item.Patronymic}
        Дата рождения: {item.DateOfBirth.ToShortDateString().Trim()}
        Номер телефона: {item.PhoneNumber}
        Email: {item.Email}";

            }
            return str;
        }

        [HttpGet]
        public ActionResult Auth()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult Auth(StaffVolunteer SV)
        {
            StaffVolunteer sv = db.StaffVolunteers.FirstOrDefault(x => x.Email == SV.Email && x.Password == SV.Password);
            if (sv != null)
            {
                ViewBag.Person = sv;
                
                if (sv.SuperUser == 1) 
                    return View("AdminPanel");
                else return View("Index", sv);
            }
            return View("Error", sv);

        }
        public ActionResult AdminPanel()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Reg()
        {
            
            ViewBag.Gender = new SelectList(db.Genders, "GenderID", "NameOfGender");
            
            return View();

        }
        [HttpPost]
        public ActionResult Reg(StaffVolunteer sv)
        {
            //int superInt = 0;
            if (Request.Form["IsSuperUser"] == "SuperYes")
                sv.SuperUser = 1;
            else
                sv.SuperUser = 0;
            sv.BeReady = true;
            
                StaffVolunteer volun = new StaffVolunteer
                {
                    Surname = sv.Surname,
                    Name = sv.Name,
                    Patronymic = sv.Patronymic,
                    DateOfBirth = sv.DateOfBirth,
                    Gender =sv.Gender ,
                    PhoneNumber = sv.PhoneNumber,
                    Email = sv.Email,
                    Password = sv.Password,
                    BeReady = sv.BeReady,
                    SuperUser = sv.SuperUser
                };
                db.StaffVolunteers.Add(volun);

                db.SaveChanges();
                //ViewBag.IsRegistration = true;

                return View("Auth");
           
        }
        [HttpPost]
        public ActionResult VolunSearch (StaffVolunteer sv, string selectedGenderID)
        {
            sv.Gender = Convert.ToInt32(selectedGenderID);
            var anyVolun = db.StaffVolunteers.Any(x => string.Compare(x.Email, sv.Email) == 0);
            var some = db.StaffVolunteers.FirstOrDefault(x => x.Email == sv.Email);
            if (some != null)
            {
                ViewBag.Ok = false;
                
            }
            else
            {
                ViewBag.Ok = true;
                Reg(sv);
            }
            return PartialView(some);

        }

        //// GET: Volunteer/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    StaffVolunteer staffVolunteer = db.StaffVolunteers.Find(id);
        //    if (staffVolunteer == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(staffVolunteer);
        //}

        // GET: Volunteer/Create
        //public ActionResult Create()
        //{
        //    ViewBag.Gender = new SelectList(db.Genders, "GenderID", "NameOfGender");
        //    return View();
        //}

        //// POST: Volunteer/Create
        //// Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        //// Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "VolunteerID,Surname,Name,Patronymic,DateOfBirth,Gender,PhoneNumber,Email,Password,SuperUser,BeReady")] StaffVolunteer staffVolunteer)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.StaffVolunteers.Add(staffVolunteer);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.Gender = new SelectList(db.Genders, "GenderID", "NameOfGender", staffVolunteer.Gender);
        //    return View(staffVolunteer);
        //}

        // GET: Volunteer/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    StaffVolunteer staffVolunteer = db.StaffVolunteers.Find(id);
        //    if (staffVolunteer == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.Gender = new SelectList(db.Genders, "GenderID", "NameOfGender", staffVolunteer.Gender);
        //    return View(staffVolunteer);
        //}

        // POST: Volunteer/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "VolunteerID,Surname,Name,Patronymic,DateOfBirth,Gender,PhoneNumber,Email,Password,SuperUser,BeReady")] StaffVolunteer staffVolunteer)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(staffVolunteer).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.Gender = new SelectList(db.Genders, "GenderID", "NameOfGender", staffVolunteer.Gender);
        //    return View(staffVolunteer);
        //}

        // GET: Volunteer/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    StaffVolunteer staffVolunteer = db.StaffVolunteers.Find(id);
        //    if (staffVolunteer == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(staffVolunteer);
        //}

        //// POST: Volunteer/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    StaffVolunteer staffVolunteer = db.StaffVolunteers.Find(id);
        //    db.StaffVolunteers.Remove(staffVolunteer);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
