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
    public class GroupsController : Controller
    {
        private EventContext db = new EventContext();

        // GET: Groups
        public ActionResult Index()
        {
            return View(db.Groups.ToList());
        }

        // GET: Groups/Details/5
        [HttpGet]
        public string Details(int? id)
        {
            IEnumerable <SearchCampaign> inf = db.SearchCampaigns.Include(c => c.MissingPerson)
                .Include(c => c.Set).Where(c=>c.GroupOfVolunteer==id);
            int a = inf.Count();
            string str="";
            foreach (var item in inf)
            {
               str = $@" Набор оборудования  ID:{item.SetOfEquipment} 
        для поисковой группы номер {item.GroupOfVolunteer.ToString()}.
        ID мероприятия: {item.SearchCampaignID} дата и время сбора: {item.DateAndTime.ToShortDateString().Trim()},
        место сбора: {item.MeetingLocation}
        ID пропавшего: {item.MissingPersonID.ToString()}.{item.MissingPerson.Surname} 
        {item.MissingPerson.Name} {item.MissingPerson.Patronymic} {item.MissingPerson.DateOfBirth} года рождения.";

            }
            return str;
        }

        // GET: Groups/Create
        
        public ActionResult Create()
        {
            return View();
        }

        // POST: Groups/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "GroupID,Name")] Group group)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Groups.Add(group);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(group);
        //}

        //// GET: Groups/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Group group = db.Groups.Find(id);
        //    if (group == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(group);
        //}

        //// POST: Groups/Edit/5
        //// Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        //// Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "GroupID,Name")] Group group)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(group).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(group);
        //}

        //// GET: Groups/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Group group = db.Groups.Find(id);
        //    if (group == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(group);
        //}

        //// POST: Groups/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Group group = db.Groups.Find(id);
        //    db.Groups.Remove(group);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
