﻿using System;
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
            var userId = Session["UserId"];
            var nameUser = Session["NameUser"];
            if (userId != null && nameUser != null)
            {
                ViewBag.UserId_Session = userId;
                ViewBag.NameUser_Session = nameUser;
            }
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
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
            var userId = Session["UserId"];
            var nameUser = Session["NameUser"];
            if (userId != null && nameUser != null)
            {
                ViewBag.UserId_Session = userId;
                ViewBag.NameUser_Session = nameUser;
            }
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "Name");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,UserName,Password,TimeRemaining,RoleId")] User user)
        {
            if (ModelState.IsValid)
            {
                user.TimeRemaining = ChangeMoneytoTime(user.TimeRemaining, 5000);
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "Name");
            return View(user);
        }

        // GET: Users/Edit/5
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
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "Name");
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,UserName,Password,TimeRemaining,RoleId")] User user)
        {
            int timeMaining_Old = GetTimeMainingOld(user.Id);
            //if (ModelState.IsValid)
            //{
                //db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();

                User user2 = db.Users.Find(user.Id);
                int timeMaining = user2.TimeRemaining;
                user2.TimeRemaining = ChangeMoneytoTime(timeMaining, 5000) + timeMaining_Old ;
                db.SaveChanges();
                return RedirectToAction("Index");
            //}
            //ViewBag.RoleId = new SelectList(db.Roles, "Id", "Name");
            //return View(user);
        }
        private int GetTimeMainingOld(int Id) {
            User users = db.Users.Find(Id);
            int time = users.TimeRemaining;
            return time;
        }

        // GET: Users/Delete/5
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

        private int ChangeMoneytoTime(int money, int moneyOneHours)
        {
            int hours = 0;
            int minutes = 0;
            int second = 0;
            int temp = 0;

            if (money > 0)
            {
                if (money >= moneyOneHours)
                {
                    hours = money / moneyOneHours;
                    temp = money % moneyOneHours;
                    if (temp > (moneyOneHours / 60))
                    {
                        minutes = temp / (moneyOneHours / 60);
                        temp = temp % (moneyOneHours / 60);
                        if (temp > (moneyOneHours / (60 * 60)))
                        {
                            second = temp / (moneyOneHours / (60 * 60));
                        }
                        else
                        {
                            second = 0;
                        }
                    }
                    else
                    {
                        if (temp > (moneyOneHours / (60 * 60)))
                        {
                            second = temp / (moneyOneHours / (60 * 60));
                        }
                        else
                        {
                            second = 0;
                        }
                    }
                }
                else
                {
                    if (temp > (moneyOneHours / 60))
                    {
                        minutes = temp / (moneyOneHours / 60);
                        temp = temp % (moneyOneHours / 60);
                        if (temp > (moneyOneHours / (60 * 60)))
                        {
                            second = temp / (moneyOneHours / (60 * 60));
                        }
                        else
                        {
                            second = 0;
                        }
                    }
                }
            }
            int time = (hours * 60 * 60) + (minutes * 60) + second;
            return time;
        }
    }
}
