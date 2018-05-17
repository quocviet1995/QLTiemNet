using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLTiemNet.DTO;
using QLTiemNet.Models;

namespace QLTiemNet.Controllers
{
    public class UsersController : Controller
    {
        private QLTiemNetDBEntities db = new QLTiemNetDBEntities();

        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,UserName,Password,TimeRemaining,Role")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,UserName,Password,TimeRemaining,Role")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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

        //GET: Users/getTimeRemaining
        [HttpPost]
        public JsonResult getTimeRemaining(int Id) {

            User user = db.Users.Find(Id);
            int TimeRemaining = user.TimeRemaining;

            int? UserId = Id;
            ComputerDTO listComputer = (from n in db.Computers
                                        where n.UserId == UserId
                                        select new ComputerDTO
                                        {
                                            TimeActive = n.TimeActive
                                        }).FirstOrDefault();
            if (listComputer != null) {
                TimeRemaining = TimeRemaining - listComputer.TimeActive;
            }
            int hours = 0;
            int minutes = 0;
            int second = 0;
            int temp = 0;

            if (TimeRemaining > 0) {
                if (TimeRemaining >= 60 * 60)
                {
                    hours = TimeRemaining / (60 * 60);
                    temp = TimeRemaining % (60 * 60);
                    if (temp >= 60)
                    {
                        minutes = temp / 60;
                        second = temp % 60;
                    }
                    else
                    {
                        second = temp;
                    }
                }
                else
                {
                    minutes = TimeRemaining / 60;
                    second = TimeRemaining % 60;
                }

            }
            string time = hours + "h " + minutes + "mm " + second + "ss";
            return Json(time, JsonRequestBehavior.AllowGet);
        }
    }
}
