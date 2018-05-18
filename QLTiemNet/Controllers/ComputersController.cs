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
    public class ComputersController : Controller
    {
        private QLTiemNetDBEntities db = new QLTiemNetDBEntities();

        // GET: Computers
        public ActionResult Index()
        {
            var userId = Session["UserId"];
            var nameUser = Session["NameUser"];
            if (userId != null && nameUser != null)
            {
                ViewBag.UserId_Session = userId;
                ViewBag.NameUser_Session = nameUser;
            }
            var computers = db.Computers.Include(c => c.ComputerDetail).Include(c => c.Status).Include(c => c.User);
            return View(computers.ToList());
        }

        // GET: Computers/Details/5
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
            Computer computer = db.Computers.Find(id);
            if (computer == null)
            {
                return HttpNotFound();
            }
            return View(computer);
        }

        // GET: Computers/Create
        public ActionResult Create()
        {
            var userId = Session["UserId"];
            var nameUser = Session["NameUser"];
            if (userId != null && nameUser != null)
            {
                ViewBag.UserId_Session = userId;
                ViewBag.NameUser_Session = nameUser;
            }
            ViewBag.ComputerDetailId = new SelectList(db.ComputerDetails, "Id", "Name");
            ViewBag.StatusId = new SelectList(db.Status, "Id", "Name");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        // POST: Computers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,TimeStart,TimeEnd,TimeActive,UserId,ComputerDetailId,StatusId")] Computer computer)
        {
            if (ModelState.IsValid)
            {
                db.Computers.Add(computer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ComputerDetailId = new SelectList(db.ComputerDetails, "Id", "Name", computer.ComputerDetailId);
            ViewBag.StatusId = new SelectList(db.Status, "Id", "Name", computer.StatusId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", computer.UserId);
            return View(computer);
        }

        // GET: Computers/Edit/5
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
            Computer computer = db.Computers.Find(id);
            if (computer == null)
            {
                return HttpNotFound();
            }
            ViewBag.ComputerDetailId = new SelectList(db.ComputerDetails, "Id", "Name", computer.ComputerDetailId);
            ViewBag.StatusId = new SelectList(db.Status, "Id", "Name", computer.StatusId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", computer.UserId);
            return View(computer);
        }

        // POST: Computers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,TimeStart,TimeEnd,TimeActive,UserId,ComputerDetailId,StatusId")] Computer computer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(computer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ComputerDetailId = new SelectList(db.ComputerDetails, "Id", "Name", computer.ComputerDetailId);
            ViewBag.StatusId = new SelectList(db.Status, "Id", "Name", computer.StatusId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", computer.UserId);
            return View(computer);
        }

        // GET: Computers/Delete/5
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
            Computer computer = db.Computers.Find(id);
            if (computer == null)
            {
                return HttpNotFound();
            }
            return View(computer);
        }

        // POST: Computers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Computer computer = db.Computers.Find(id);
            db.Computers.Remove(computer);
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
