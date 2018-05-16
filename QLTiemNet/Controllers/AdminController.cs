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
                ViewBag.UserId = userId;
                ViewBag.NameUser = nameUser;
            }
            return View();
        }

        // POST: Admin/Login
        [HttpPost]
        public JsonResult Login(String userName, String password)
        {
            try
            {
                String Role = "admin";
                var user = db.Users.Where(x => x.UserName.Equals(userName) && x.Role == Role).FirstOrDefault();

                if (user == null)
                {
                    return Json(new { message = "Sai tên đăng nhập", error = 1 });
                }
                else
                {
                    if (user.Password == password)
                    {
                        Session.Add("UserId", user.Id) ;
                        Session.Add("NameUser", user.Name);
                        return Json(new { message = "Đăng nhập thành công", error = 0 });
                    }
                    else {
                        return Json(new { message = "Sai mật khẩu đăng nhập", error = 1 });
                    }
                } 
            }
            catch
            {
                return Json(new { message = "Login fail", error = 1 });
            }
        }
        // GET: Admin
        public ActionResult Index()
        {
            var userId = Session["UserId"];
            var nameUser = Session["NameUser"];
            if (userId != null && nameUser != null)
            {
                ViewBag.UserId = userId;
                ViewBag.NameUser = nameUser;
            }
            return View();
        }

    }
}
