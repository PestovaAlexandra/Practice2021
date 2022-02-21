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
using ClosedXML.Excel;
using Practice2021;

namespace Practice2021.Controllers
{
    public class MissingPersonsController : Controller
    {
        private EventContext db = new EventContext();

        // GET: MissingPersons
        public ActionResult Index()
        {
            var missingPersons = db.MissingPersons.Include(m => m.Gender1);
            return View(missingPersons.ToList());
        }
        // GET: MissingPersons/Create
        [HttpGet]
        public ActionResult Create()
        {
            List<string> list = new List<string>();
            list.Add("худощавое");
            list.Add("нормальное");
            list.Add("плотное");
            list.Add("крупное");

            MissingPerson m = new MissingPerson();
            ViewBag.Body = new SelectList(list);
            ViewBag.Gender = new SelectList(db.Genders, "GenderID", "NameOfGender");
            //return View(m);
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(MissingPerson missingPerson, string selectedGenderID, string selectedBodyType, HttpPostedFileBase uploadImage)
        {
            missingPerson.Gender = Convert.ToInt32(selectedGenderID);
            var str = selectedBodyType;
            missingPerson.BodyType = str;
            missingPerson.New = true;

            byte[] imageData = null;

            // считываем переданный файл в массив байтов

            using (var binaryReader = new BinaryReader(uploadImage.InputStream))
            {
                imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
            }
            // установка массива байтов
            missingPerson.Image = imageData;
            db.MissingPersons.Add(missingPerson);
            try
            {

                db.SaveChanges();
                return View("ResultOfAddingMis", true);
            }
            catch (DbEntityValidationException ex)
            {
                return View("ResultOfAddingMis", false);
            }
           
        }
        public ActionResult Details(int id)
        {
            MissingPerson person = db.MissingPersons.Find(id);
            if (person != null)
            {
                return PartialView("Details", person);
            }
            return View("SearchInMis", "Home");
        }
        [HttpGet]
        public ActionResult MakeDateFound()
        {
            ViewBag.People = db.MissingPersons.Where(s => s.DateFound == null);
            ViewBag.Heading = "notfound";
            ViewBag.Search = "yes";
            return View();
        }
        [HttpPost]
        public ActionResult MakeDateFound(string submitButton, string inputSur, string inputName, string inputPat, string inputID, DateTime? inputDate = null)
        {

            switch (submitButton)
            {
                case "Показать найденных":
                    {
                            ViewBag.Heading = "found";
                        var list = db.MissingPersons.Where(s => s.DateFound != null);
                        ViewBag.People = list;
                        ViewBag.Add = "no";
                        return View();
                           
                    }
                case "Показать не найденных":
                    {
                        ViewBag.Heading = "notfound";
                        var list = db.MissingPersons.Where(s => s.DateFound == null);
                        ViewBag.People = list;
                        ViewBag.Add = "yes";
                        return View();

                    }
                case "Найти":
                    {
                        IEnumerable<MissingPerson> people = null;
                        if (inputID != "")
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
                                people = db.MissingPersons.Where(s => s.Surname == inputSur&&s.DateFound==null);
                            }
                            if (inputSur != "" && inputName != "" && inputPat == "" && inputDate == null)
                            {
                                people = db.MissingPersons.Where(s => s.Surname == inputSur && s.Name == inputName && s.DateFound == null);

                            }
                            if (inputSur != "" && inputName == "" && inputPat != "" && inputDate == null)
                            {
                                people = db.MissingPersons.Where(s => s.Surname == inputSur && s.Patronymic == inputPat && s.DateFound == null);

                            }
                            if (inputSur != "" && inputName == "" && inputPat == "" && inputDate != null)
                            {
                                people = db.MissingPersons.Where(s => s.Surname == inputSur && s.DateOfBirth == inputDate && s.DateFound == null);

                            }
                            if (inputSur != "" && inputName != "" && inputPat == "" && inputDate != null)
                            {
                                people = db.MissingPersons.Where(s => s.Surname == inputSur && s.Name == inputName & s.DateOfBirth == inputDate && s.DateFound == null);

                            }
                            if (inputSur != "" && inputName != "" && inputPat != "" && inputDate == null)
                            {
                                people = db.MissingPersons.Where(s => s.Surname == inputSur && s.Name == inputName && s.Patronymic == inputPat && s.DateFound == null);

                            }
                            if (inputSur != "" && inputName != "" && inputPat != "" && inputDate != null)
                            {
                                people = db.MissingPersons.Where(s => s.Surname == inputSur && s.Name == inputName && s.Patronymic == inputPat && s.DateOfBirth == inputDate && s.DateFound == null);
                            }
                            ViewBag.People = people;
                            if (people.Count() > 0)
                            {
                                ViewBag.Haeding = "search";
                                ViewBag.Add = "yes";
                            }
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
        
        public ActionResult Add(int id)
        {
            MissingPerson new_mp = db.MissingPersons.Find(id);

            if (new_mp != null)
            {
                return PartialView("Add", new_mp);
            }
            return View("MakeDateFound");
          
        }
        [HttpPost]
        public ActionResult Add(MissingPerson mp, DateTime DateFound)
        {
            MissingPerson new_mp = db.MissingPersons.Find(mp.MissingPersonID);
            new_mp.DateFound = DateFound;
            new_mp.PhoneNumber = new_mp.PhoneNumber.Trim();
            new_mp.Email = new_mp.Email.Trim();

            db.SaveChanges();
            ViewBag.Result = true;
            return RedirectToAction("MakeDateFound");
        }

        public ActionResult ExportNF()
        {
            var a = db.MissingPersons.Where(s => s.DateFound == null);
            using (XLWorkbook workbook = new XLWorkbook(XLEventTracking.Disabled))
            {
                var worksheet = workbook.Worksheets.Add("Не найденные люди");

                worksheet.Cell("A1").Value = "Фамилия";
                worksheet.Cell("B1").Value = "Имя";
                worksheet.Cell("C1").Value = "Отчество";
                worksheet.Cell("D1").Value = "Дата рождения";
                worksheet.Row(1).Style.Font.Bold = true;

                //нумерация строк/столбцов начинается с индекса 1 (не 0)
                int i = 0;
                foreach(var item in a)
                {
                    worksheet.Cell(i + 2, 1).Value = item.Surname;
                    worksheet.Cell(i + 2, 2).Value = item.Name;
                    worksheet.Cell(i + 2, 3).Value = item.Patronymic;
                    worksheet.Cell(i + 2, 4).Value = item.DateOfBirth.ToShortDateString();
                    i++;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Flush();

                    return new FileContentResult(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = $"not_found_people_{DateTime.UtcNow.ToShortDateString()}.xlsx"
                    };
                }
            }

        }
        public ActionResult ExportF()
        {
            var a = db.MissingPersons.Where(s => s.DateFound != null);
            using (XLWorkbook workbook = new XLWorkbook(XLEventTracking.Disabled))
            {
                var worksheet = workbook.Worksheets.Add("Не найденные люди");

                worksheet.Cell("A1").Value = "Фамилия";
                worksheet.Cell("B1").Value = "Имя";
                worksheet.Cell("C1").Value = "Отчество";
                worksheet.Cell("D1").Value = "Дата рождения";
                worksheet.Row(1).Style.Font.Bold = true;

                //нумерация строк/столбцов начинается с индекса 1 (не 0)
                int i = 0;
                foreach (var item in a)
                {
                    worksheet.Cell(i + 2, 1).Value = item.Surname;
                    worksheet.Cell(i + 2, 2).Value = item.Name;
                    worksheet.Cell(i + 2, 3).Value = item.Patronymic;
                    worksheet.Cell(i + 2, 4).Value = item.DateOfBirth.ToShortDateString();
                    i++;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Flush();

                    return new FileContentResult(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = $"found_people_{DateTime.UtcNow.ToShortDateString()}.xlsx"
                    };
                }
            }
        }








        // GET: MissingPersons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MissingPerson missingPerson = db.MissingPersons.Find(id);
            if (missingPerson == null)
            {
                return HttpNotFound();
            }
            ViewBag.Gender = new SelectList(db.Genders, "GenderID", "NameOfGender", missingPerson.Gender);
            return View(missingPerson);
        }

        // POST: MissingPersons/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MissingPersonID,Surname,Name,Patronymic,DateOfBirth,Gender,Growth,BodyType,Appearance,ClothingDescription,SpecialThings,DateLastSeen,PlaceLastSeen,PossibleLocation,DateFound,FullApplicant,PhoneNumber,Email,New,Image")] MissingPerson missingPerson)
        {
            if (ModelState.IsValid)
            {
                db.Entry(missingPerson).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Gender = new SelectList(db.Genders, "GenderID", "NameOfGender", missingPerson.Gender);
            return View(missingPerson);
        }

        

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
