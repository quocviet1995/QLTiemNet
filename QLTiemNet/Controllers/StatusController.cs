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
    public class StatusController : Controller
    {
        private QLTiemNetDBEntities db = new QLTiemNetDBEntities();

        // GET: Status
        public ActionResult Index()
        {
            var userId = Session["UserId"];
            var nameUser = Session["NameUser"];
            if (userId != null && nameUser != null)
            {
                ViewBag.UserId_Session = userId;
                ViewBag.NameUser_Session = nameUser;
            }
            return View(db.Status.ToList());
        }

        // GET: Status/Details/5
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
            Status status = db.Status.Find(id);
            if (status == null)
            {
                return HttpNotFound();
            }
            return View(status);
        }

        // GET: Status/Create
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

        // POST: Status/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Status status)
        {
            if (ModelState.IsValid)
            {
                db.Status.Add(status);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(status);
        }

        // GET: Status/Edit/5
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
            Status status = db.Status.Find(id);
            if (status == null)
            {
                return HttpNotFound();
            }
            return View(status);
        }

        // POST: Status/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Status status)
        {
            if (ModelState.IsValid)
            {
                db.Entry(status).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(status);
        }

        // GET: Status/Delete/5
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
            Status status = db.Status.Find(id);
            if (status == null)
            {
                return HttpNotFound();
            }
            return View(status);
        }

        // POST: Status/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Status status = db.Status.Find(id);
            db.Status.Remove(status);
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
