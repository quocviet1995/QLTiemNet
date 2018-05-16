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
            return View();
        }

        // POST: Admin/Login
        [HttpPost]
        public JsonResult Login(String user, String password)
        {
            try
            {
                String Role = "admin";
                var login = db.Users.Where(x => x.UserName.Equals(user) && x.Role == Role).FirstOrDefault();

                if (login == null)
                {
                    return Json(new { message = "Sai tên đăng nhập", error = 1 });
                }
                else
                {
                    if (login.Password == password)
                    {
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
            return View();
        }

    }
}
