using QLTiemNet.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLTiemNet.Controllers
{
    public class AdminController : Controller
    {
        private QLTiemNetDBEntities db = new QLTiemNetDBEntities();

        // GET : Admin/Login
        public ActionResult Login()
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

        // POST: Admin/Login
        [HttpPost]
        public JsonResult Login(String userName, String password)
        {
            try
            {
                ///RoleId = 1 as Admin
                var user = db.Users.Where(x => x.UserName.Equals(userName) && x.RoleId == 1).FirstOrDefault();

                if (user == null)
                {
                    return Json(new { message = "Account does not exist!", error = 1 });
                }
                else
                {
                    if (user.Password == password)
                    {
                        Session.Add("UserId", user.Id) ;
                        Session.Add("NameUser", user.Name);
                        return Json(new { message = "Login successfully!", error = 0 });
                    }
                    else {
                        return Json(new { message = "Wrong password!", error = 1 });
                    }
                } 
            }
            catch
            {
                return Json(new { message = "Login fail", error = 1 });
            }
        }

        //GET : Admin/Logout
        public ActionResult Logout()
        {
            Session.Remove("UserId");
            Session.Remove("NameUser");
            ViewBag.UserId_Session = null;
            ViewBag.NameUser_Session = null;
            return RedirectToAction("Login", "Admin");
        }
        // GET: Admin
        public ActionResult Index()
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

    }
}
