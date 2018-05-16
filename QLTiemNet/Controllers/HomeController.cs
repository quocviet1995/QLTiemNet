using QLTiemNet.DTO;
using QLTiemNet.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace QLTiemNet.Controllers
{
    public class HomeController : Controller
    {
        private QLTiemNetDBEntities db = new QLTiemNetDBEntities();

        // GET : Home/Login
        public ActionResult Login()
        {
            var userId = Session["UserId"];
            var nameUser = Session["NameUser"];
            var timeRemaining = Session["TimeRemaining"];
            if (userId != null && nameUser != null)
            {
                ViewBag.UserId = userId;
                ViewBag.NameUser = nameUser;
                ViewBag.TimeRemaining = timeRemaining;
            }
            return View();
        }

        // POST: Home/Login
        [HttpPost]
        public JsonResult Login(String userName, String password)
        {
            try
            {
                String RoleUser = "user";
                String RoleAdmin = "admin";
                var user = db.Users.Where(x => x.UserName.Equals(userName) && x.Role == RoleAdmin || x.Role == RoleUser ).FirstOrDefault();

                if (user == null)
                {
                    return Json(new { message = "Sai tên đăng nhập", error = 1 });
                }
                else
                {
                    if (user.Password == password)
                    {
                        Session.Add("UserId", user.Id);
                        Session.Add("NameUser", user.Name);
                        Session.Add("TimeRemaining", user.TimeRemaining);
                        return Json(new { message = "Đăng nhập thành công", error = 0 });
                    }
                    else
                    {
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
            var timeRemaining = Session["TimeRemaining"];
            if (userId != null && nameUser != null)
            {
                ViewBag.UserId = userId;
                ViewBag.NameUser = nameUser;
                ViewBag.TimeRemaining = timeRemaining;
            }
            return View();
        }

        //GET : Home/Logout
        public ActionResult Logout()
        {
            Session.Remove("UserId");
            Session.Remove("NameUser");
            Session.Remove("TimeRemaining");
            ViewBag.UserId = null;
            ViewBag.NameUser = null;
            ViewBag.TimeRemaining = null;
            return RedirectToAction("Index","Home");
        }

        //Get: Home/getAllComputer
        [HttpPost]
        public JsonResult getAllComputer( int status)
        {
            List<ComputerDTO> listComputer = new  List<ComputerDTO>();
            if (status > 0) {
                listComputer = (from n in db.Computers
                                where n.StatusId == status
                                select new ComputerDTO
                                {
                                    Id = n.Id,
                                    Name = n.Name,
                                    TimeStart = n.TimeStart,
                                    TimeEnd = n.TimeEnd,
                                    StatusId = n.StatusId,
                                    UserId = n.UserId
                                }).ToList();
            }
            else {
                listComputer = (from n in db.Computers
                                select new ComputerDTO
                                {
                                    Id = n.Id,
                                    Name = n.Name,
                                    TimeStart = n.TimeStart,
                                    TimeEnd = n.TimeEnd,
                                    StatusId = n.StatusId,
                                    UserId = n.UserId
                                }).ToList();
            }

            return Json(listComputer, JsonRequestBehavior.AllowGet);
        }

    }
}