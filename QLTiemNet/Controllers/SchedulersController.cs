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
    public class SchedulersController : Controller
    {
        private QLTiemNetDBEntities db = new QLTiemNetDBEntities();

        // GET: Schedulers
        public ActionResult Index()
        {
            var userId = Session["UserId"];
            var nameUser = Session["NameUser"];
            if (userId != null && nameUser != null)
            {
                ViewBag.UserId_Session = userId;
                ViewBag.NameUser_Session = nameUser;
            }
            var schedulers = db.Schedulers.Include(s => s.Computer).Include(s => s.Status).Include(s => s.User);
            return View(schedulers.ToList());
        }

        // GET: Schedulers/Details/5
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
            Scheduler scheduler = db.Schedulers.Find(id);
            if (scheduler == null)
            {
                return HttpNotFound();
            }
            return View(scheduler);
        }

        // GET: Schedulers/Create
        public ActionResult Create()
        {
            var userId = Session["UserId"];
            var nameUser = Session["NameUser"];
            if (userId != null && nameUser != null)
            {
                ViewBag.UserId_Session = userId;
                ViewBag.NameUser_Session = nameUser;
            }
            ViewBag.ComputerId = new SelectList(db.Computers, "Id", "Name");
            ViewBag.StatusId = new SelectList(db.Status, "Id", "Name");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        // POST: Schedulers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Time,UserId,ComputerId,StatusId")] Scheduler scheduler)
        {
            if (ModelState.IsValid)
            {
                db.Schedulers.Add(scheduler);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ComputerId = new SelectList(db.Computers, "Id", "Name", scheduler.ComputerId);
            ViewBag.StatusId = new SelectList(db.Status, "Id", "Name", scheduler.StatusId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", scheduler.UserId);
            return View(scheduler);
        }

        // GET: Schedulers/Edit/5
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
            Scheduler scheduler = db.Schedulers.Find(id);
            if (scheduler == null)
            {
                return HttpNotFound();
            }
            ViewBag.ComputerId = new SelectList(db.Computers, "Id", "Name", scheduler.ComputerId);
            ViewBag.StatusId = new SelectList(db.Status, "Id", "Name", scheduler.StatusId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", scheduler.UserId);
            return View(scheduler);
        }

        // POST: Schedulers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Time,UserId,ComputerId,StatusId")] Scheduler scheduler)
        {
            if (ModelState.IsValid)
            {
                db.Entry(scheduler).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ComputerId = new SelectList(db.Computers, "Id", "Name", scheduler.ComputerId);
            ViewBag.StatusId = new SelectList(db.Status, "Id", "Name", scheduler.StatusId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", scheduler.UserId);
            return View(scheduler);
        }

        // GET: Schedulers/Delete/5
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
            Scheduler scheduler = db.Schedulers.Find(id);
            if (scheduler == null)
            {
                return HttpNotFound();
            }
            return View(scheduler);
        }

        // POST: Schedulers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Scheduler scheduler = db.Schedulers.Find(id);
            db.Schedulers.Remove(scheduler);
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
