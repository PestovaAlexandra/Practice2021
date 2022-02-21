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
    public class EquipmentsController : Controller
    {
        private EventContext db = new EventContext();

        // GET: Equipments
        //[HttpGet]
        public ActionResult Index(int ? idG)
        {

            var inf = (db.SearchCampaigns.Include(s => s.Set)).Include(s => s.MissingPerson).Include(s => s.Group);
            ViewBag.Groups = new SelectList(db.Groups, "GroupID", "GroupID");
            ViewBag.Equip = new SelectList(db.Equipments, "InventoryNumber", "InventoryNumber");
            if (idG != null)
            {
                var items = db.SearchCampaigns.Where(s => s.GroupOfVolunteer == idG).ToList();

                int idS = items[0].SetOfEquipment;

                var sq = db.Sets.Where(s => s.SetID == idS).Include(m => m.Equipments);
                ViewBag.Items = sq;
                ViewBag.Heading = "forsearch";
            }
            else
            {
                ViewBag.Items = db.Equipments;
                ViewBag.Heading = "forall";
            }
                        return View("Index");
        }
        [HttpPost]
        public ActionResult Index(string submitButton, string selectedGroupID, string selectedEqID)
        {
            switch(submitButton)
            {
                //case "Добавить":
                //    {
                //        int idG = Convert.ToInt32(selectedGroupID);
                //        int idE = Convert.ToInt32(selectedEqID);
                //        int idS = 0;

                //        var sql = db.SearchCampaigns.Where(s => s.GroupOfVolunteer == idG);
                //        foreach (var b in sql)
                //        {
                //            idS = b.SetOfEquipment;
                //        }
                //        Equipment eq = db.Equipments.Find(idE);

                //        try
                //        {
                //            db.Sets.Find(idS).Equipments.Add(eq);
                //            db.SaveChanges();
                //            ViewBag.Ok = true;
                            
                //            ViewBag.Heading = "forsearch";
                //        }
                //        catch
                //        {
                //            ViewBag.Ok = false;
                //        }
                //        break;
                //    }
                case "Открыть":
                    {
                        ViewBag.Items = db.Equipments;
                        ViewBag.Heading = "forall";
                        break;
                    }
            }
            
            ViewBag.Groups = new SelectList(db.Groups, "GroupID", "GroupID");
            ViewBag.Equip = new SelectList(db.Equipments, "InventoryNumber", "InventoryNumber");
            //ViewBag.Items = db.Equipments;
           
            return View("Index");
        }
        [HttpGet]

        public string GetName(int? id)
        {
            IEnumerable<Equipment> eq = db.Equipments.Where(s => s.InventoryNumber == id);
            int a = eq.Count();
            string str = "";
            foreach (var item in eq)
            {
                str = $@"{item.Name}";
            }
            return str;
        }
        [HttpPost]
        public ActionResult ResultOfAddingNew(string newEquip)
        {
            Equipment eq = new Equipment
            {
                Name = newEquip.Trim()
            };
            db.Equipments.Add(eq);
            try
            {
                db.SaveChanges();
                ViewBag.Ok = true;
            }
            catch
            {
                ViewBag.Ok = false;
            }

            return PartialView();
        }
        [HttpPost]
        public ActionResult ResultOfAddingInSet(string selectedGroupID, string selectedEqID)
        {
            int idG = Convert.ToInt32(selectedGroupID);
            int idE = Convert.ToInt32(selectedEqID);
            int idS = 0;

            var sql=db.SearchCampaigns.Where(s => s.GroupOfVolunteer == idG);
            foreach(var b in sql)
            {
                idS = b.SetOfEquipment;
            }
            Equipment eq = db.Equipments.Find(idE);
            
            try
            {
                db.Sets.Find(idS).Equipments.Add(eq);
                db.SaveChanges();
                ViewBag.Ok = true;
            }
            catch
            {
                ViewBag.Ok = false;
            }

            return View();
        }
        [HttpPost]
        public ActionResult ResultOfDelete(string selectedEqIDForDel)
        {
            int idE = Convert.ToInt32(selectedEqIDForDel);

            Equipment eq = db.Equipments.Find(idE);

            try
            {
                if (eq != null)
                {
                    db.Equipments.Remove(eq);
                    db.SaveChanges();
                }
                ViewBag.Equip = new SelectList(db.Equipments, "InventoryNumber", "InventoryNumber");
                ViewBag.Groups = new SelectList(db.Groups, "GroupID", "GroupID");
                ViewBag.Ok = true;
            }
            catch
            {
                ViewBag.Ok = false;
            }

            return PartialView();
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
