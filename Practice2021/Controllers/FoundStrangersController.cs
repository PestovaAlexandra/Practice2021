using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Practice2021;

namespace Practice2021.Controllers
{
    public class FoundStrangersController : Controller
    {
        private EventContext db = new EventContext();

        // GET: FoundStrangers
        public ActionResult Index()
        {
            var foundStrangers = db.FoundStrangers.Include(f => f.Gender1);
            return View(foundStrangers.ToList());
        }

        // GET: FoundStrangers/Details/5
        

        // GET: FoundStrangers/Create
        [HttpGet]
        
        public ActionResult Create()
        {
            
            FoundStranger m = new FoundStranger();
            
            ViewBag.Gender = new SelectList(db.Genders, "GenderID", "NameOfGender");
            
            return View();
        }

        // POST: FoundStrangers/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(FoundStranger foundStranger, string selectedGenderID, HttpPostedFileBase uploadImage)
        {
                foundStranger.Gender = Convert.ToInt32(selectedGenderID);
            byte[] imageData = null;

            // считываем переданный файл в массив байтов
            if (uploadImage != null)
            {
                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                }
                // установка массива байтов
                foundStranger.Image = imageData;
            }
            else
            {

            }
            db.FoundStrangers.Add(foundStranger);
                try
                {

                    db.SaveChanges();
                    return View("ResultOfAddingF", true);
                }
                catch (DbEntityValidationException ex)
                {
                    return View("ResultOfAddingF", false);
                }
        }

        public ActionResult Details(int id)
        {
            FoundStranger person = db.FoundStrangers.Find(id);
            if (person != null)
            {
                return PartialView("Details", person);
            }
            return View("SearchInFound", "Home");
        }
        //Освобождение управляемых и неуправляемых ресурсов
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
