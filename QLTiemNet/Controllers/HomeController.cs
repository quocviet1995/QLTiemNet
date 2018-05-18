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
                ///RoleId = 1 as Admin, 2 as User
                var user = db.Users.Where(x => x.UserName.Equals(userName) && x.RoleId == 1 || x.RoleId == 2 ).FirstOrDefault();

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
            return View(db.Status.ToList());
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

        //Get : Home/getAllInformationComputer
        [HttpPost]
        public JsonResult getAllInformationComputer(int Id, int? UserId)
        {
            List<ComputerAllInformationDTO> computerAllInformation = new List<ComputerAllInformationDTO>();

            if (UserId != null)
            {
                computerAllInformation = (from c in db.Computers
                                          join u in db.Users on c.UserId equals u.Id
                                          join cd in db.ComputerDetails on c.ComputerDetailId equals cd.Id
                                          join s in db.Status on c.StatusId equals s.Id
                                          where c.Id == Id
                                          select new ComputerAllInformationDTO
                                          {
                                              Id = c.Id,
                                              Name = c.Name,
                                              TimeStart = c.TimeStart,
                                              TimeEnd = c.TimeEnd,
                                              TimeActive = c.TimeActive,
                                              UserId = c.UserId,
                                              UserName = u.UserName,
                                              ComputerDetailId = c.ComputerDetailId,
                                              StatusId = c.StatusId,
                                              StatusName = s.Name,
                                              ComputerDetailName = cd.Name,
                                              Cpu = cd.Cpu,
                                              Ram = cd.Ram,
                                              HardDisk = cd.HardDisk,
                                              Graphic = cd.Graphic,
                                              Monitor = cd.Monitor
                                          }).ToList();
            }
            else {
                computerAllInformation = (from c in db.Computers
                                          join cd in db.ComputerDetails on c.ComputerDetailId equals cd.Id
                                          join s in db.Status on c.StatusId equals s.Id
                                          where c.Id == Id
                                          select new ComputerAllInformationDTO
                                          {
                                              Id = c.Id,
                                              Name = c.Name,
                                              TimeStart = c.TimeStart,
                                              TimeEnd = c.TimeEnd,
                                              TimeActive = c.TimeActive,
                                              UserId = c.UserId,
                                              ComputerDetailId = c.ComputerDetailId,
                                              StatusId = c.StatusId,
                                              StatusName = s.Name,
                                              ComputerDetailName = cd.Name,
                                              Cpu = cd.Cpu,
                                              Ram = cd.Ram,
                                              HardDisk = cd.HardDisk,
                                              Graphic = cd.Graphic,
                                              Monitor = cd.Monitor
                                          }).ToList();
            }
            List<ComputerAllInformationDTO> listresult = new List<ComputerAllInformationDTO>();
            foreach (ComputerAllInformationDTO s in computerAllInformation)
            {
                s.TimeActiveString = ConvertIntTimeToString(s.TimeActive);
                listresult.Add(s);
            }
            return Json(listresult, JsonRequestBehavior.AllowGet);
        }

        //Get :Home/getStatus
        [HttpGet]
        public JsonResult getStatus()
        {
            List<StatusDTO> listStatus = new List<StatusDTO>();

            listStatus = (from n in db.Status
                          select new StatusDTO {
                              Id = n.Id,
                              Name = n.Name
                          }).ToList();
            return Json(listStatus, JsonRequestBehavior.AllowGet);
        }

        private String ConvertIntTimeToString(int time)
        {
            int hours = 0;
            int minutes = 0;
            int second = 0;
            int temp = 0;

            if (time > 0)
            {
                if (time >= 60 * 60)
                {
                    hours = time / (60 * 60);
                    temp = time % (60 * 60);
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
                    minutes = time / 60;
                    second = time % 60;
                }

            }
            string result = hours + "h " + minutes + "mm " + second + "ss";
            return result;
        }
    }
}