using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLTiemNet.Models;

namespace QLTiemNet.Controllers
{
    public class ComputerDetailsController : Controller
    {
        private QLTiemNetDBEntities db = new QLTiemNetDBEntities();

        // GET: ComputerDetails
        public ActionResult Index()
        {
            var userId = Session["UserId"];
            var nameUser = Session["NameUser"];
            if (userId != null && nameUser != null)
            {
                ViewBag.UserId_Session = userId;
                ViewBag.NameUser_Session = nameUser;
            }
            return View(db.ComputerDetails.ToList());
        }

        // GET: ComputerDetails/Details/5
        public ActionResult Details(int? id)
        {
            var userId = Session["UserId"];
            var nameUser = Session["NameUser"];
            if (userId != null && nameUser != null)
            {
                ViewBag.UserId_Session = userId;
                ViewBag.NameUser_Session = nameUser;
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComputerDetail computerDetail = db.ComputerDetails.Find(id);
            if (computerDetail == null)
            {
                return HttpNotFound();
            }
            return View(computerDetail);
        }

        // GET: ComputerDetails/Create
        public ActionResult Create()
        {
            var userId = Session["UserId"];
            var nameUser = Session["NameUser"];
            if (userId != null && nameUser != null)
            {
                ViewBag.UserId_Session = userId;
                ViewBag.NameUser_Session = nameUser;
            }
            return View();
        }

        // POST: ComputerDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Cpu,Ram,HardDisk,Graphic,Monitor")] ComputerDetail computerDetail)
        {
            if (ModelState.IsValid)
            {
                db.ComputerDetails.Add(computerDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(computerDetail);
        }

        // GET: ComputerDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            var userId = Session["UserId"];
            var nameUser = Session["NameUser"];
            if (userId != null && nameUser != null)
            {
                ViewBag.UserId_Session = userId;
                ViewBag.NameUser_Session = nameUser;
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComputerDetail computerDetail = db.ComputerDetails.Find(id);
            if (computerDetail == null)
            {
                return HttpNotFound();
            }
            return View(computerDetail);
        }

        // POST: ComputerDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Cpu,Ram,HardDisk,Graphic,Monitor")] ComputerDetail computerDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(computerDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(computerDetail);
        }

        // GET: ComputerDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            var userId = Session["UserId"];
            var nameUser = Session["NameUser"];
            if (userId != null && nameUser != null)
            {
                ViewBag.UserId_Session = userId;
                ViewBag.NameUser_Session = nameUser;
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComputerDetail computerDetail = db.ComputerDetails.Find(id);
            if (computerDetail == null)
            {
                return HttpNotFound();
            }
            return View(computerDetail);
        }

        // POST: ComputerDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ComputerDetail computerDetail = db.ComputerDetails.Find(id);
            db.ComputerDetails.Remove(computerDetail);
            db.SaveChanges();
            return RedirectToAction("Index");
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
