using QLTiemNet.DTO;
using QLTiemNet.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;
using System.Web.Mvc;

namespace QLTiemNet.Controllers
{
    public class ToolController : Controller
    {
        private QLTiemNetDBEntities db = new QLTiemNetDBEntities();

       
        public ToolController() {
            Timer timer = new Timer();
            timer.Elapsed += new ElapsedEventHandler(_timer_elapsed);
            timer.Interval = 60 * 1000;
            timer.Enabled = true;
            timer.Start();
        }
        void _timer_elapsed(Object source, ElapsedEventArgs e)
        {
            UpdateComputerActiveTime();
        }
        private void UpdateComputerActiveTime()
        {
            List<Computer> computers = db.Computers.Where(x => x.UserId != null).ToList();
            foreach (Computer computer in computers) {
                var bill = db.Bills.Where(x => x.ComputerId == computer.Id && x.StatusId == 5 ).FirstOrDefault();
                long timeStartTick = bill.TimeStart.Ticks;
                long timeNow = DateTime.Now.Ticks;
                long time = (timeNow - timeStartTick)/10000000;
                string timeActive = time.ToString();
                computer.TimeActive = Int32.Parse(timeActive);
                db.SaveChanges();
            }
        }

        // GET: Tool
        public ActionResult Index()
        {
            return View();
        }

        //GET: Tool/getAllComputerStatusEmpty
        [HttpGet]
        public JsonResult getAllComputerStatusEmpty()
        {
            List<ComputerDTO> listCompuer = new List<ComputerDTO>();

            listCompuer = (from n in db.Computers 
                           where n.StatusId != 1
                           select new ComputerDTO {
                               Id = n.Id,
                               Name = n.Name
                           }
                           ).ToList();

            return Json(listCompuer,JsonRequestBehavior.AllowGet);
        }

        //POST: Tool/UserRegisterComputer
        [HttpPost]
        public JsonResult UserRegisterComputer( string userName, string password, int ComputerId)
        {
            try
            {
                var user = db.Users.Where(x => x.UserName.Equals(userName)).FirstOrDefault();

                if (user == null)
                {
                    return Json(new { message = "Account does not exist!", error = 1 });
                }
                else
                {
                    if (user.Password == password)
                    {
                        var computer = db.Computers.Where(x => x.UserId == user.Id).FirstOrDefault();

                        if (computer == null)
                        {
                            Computer computerRegister = db.Computers.Find(ComputerId);
                            DateTime timeStart = DateTime.Now;
                            long time = timeStart.Ticks;
                            Dictionary<String, int> timeCalculator = CaculatorTime(user.TimeRemaining);
                            long year = timeCalculator["Year"] * 60 * 60 * 24 * 365;
                            long month = timeCalculator["Month"] * 60 * 60 * 24 * 30;
                            long day = timeCalculator["Day"] * 60 * 60 * 24;
                            long hours = timeCalculator["Hours"] * 60 * 60;
                            long minutes = timeCalculator["Minutes"] * 60;
                            long second = timeCalculator["Second"];
                            long timeRemaining = 10000000 * (year + month + day + hours + minutes + second);
  
                            DateTime timeEnd = new DateTime().AddTicks(time + timeRemaining);
                            computerRegister.StatusId = 1;
                            computerRegister.UserId = user.Id;
                            computerRegister.TimeStart = timeStart;
                            computerRegister.TimeEnd = timeEnd;

                            Bill bill = new Bill();
                            bill.ComputerId = computerRegister.Id;
                            bill.UserId = user.Id;
                            bill.StatusId = 5;
                            bill.TimeStart = timeStart;
                            bill.TimeEnd = timeEnd;
                            db.Bills.Add(bill);
                            db.SaveChanges();
                            return Json(new { message = "Login successfully!", error = 0 });
                        }
                        else {
                            return Json(new { message = "User: "+user.UserName+" is active on another computer!", error = 1 });
                        }
                        
                    }
                    else
                    {
                        return Json(new { message = "Wrong password!", error = 1 });
                    }
                }
            }
            catch
            {
                return Json(new { message = "Login fail", error = 1 });
            }
        }

        //Get: Tool/GetUserComputerActive
        [HttpGet]
        public JsonResult GetUserComputerActive()
        {
            List<ComputerAllInformationDTO> listresult = new List<ComputerAllInformationDTO>();
            List<ComputerAllInformationDTO> listcomputerAllInformation = (from c in db.Computers
                                                                          join u in db.Users on c.UserId equals u.Id
                                                                          join cd in db.ComputerDetails on c.ComputerDetailId equals cd.Id
                                                                          join s in db.Status on c.StatusId equals s.Id
                                                                          where c.UserId != null
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
            foreach (ComputerAllInformationDTO s in listcomputerAllInformation) {
                s.TimeActiveString = ConvertIntTimeToString(s.TimeActive);
                listresult.Add(s);
            }
            return Json(listresult, JsonRequestBehavior.AllowGet);
        }

        //POST: Tool/UserLogoutComputer
        [HttpPost]
        public JsonResult UserLogoutComputer(int ComputerId, int UserId)
        {
            try {
                Computer computer = db.Computers.Find(ComputerId);
                computer.UserId = null;
                computer.StatusId = 2;
                computer.TimeStart = null;
                computer.TimeEnd = null;
                computer.TimeActive = 0;

                Bill bill = db.Bills.Where(x => x.StatusId == 5 && x.ComputerId == ComputerId && x.UserId == UserId).FirstOrDefault();
                DateTime timeEnd = DateTime.Now;
                bill.TimeEnd = timeEnd;
                bill.StatusId = 4;
                long timeStartNano = bill.TimeStart.Ticks;
                long timeEndNano = timeEnd.Ticks;
                long timeRemainingLoss = (timeEndNano - timeStartNano)/10000000;

                User user = db.Users.Find(UserId);
                string timeloss = timeRemainingLoss.ToString();
                user.TimeRemaining = user.TimeRemaining - Int32.Parse(timeloss);
                db.SaveChanges();
                return Json(new { message = "Logout successfully!", error = 0 });
            }
            catch
            {
                return Json(new { message = "Logout fail", error = 1 });
            }
        }

        private Dictionary<String,int> CaculatorTime( int time)
        {
            int hours = 0;
            int minutes = 0;
            int second = 0;
            int year = 0;
            int month = 0;
            int day = 0;
            int temp = time;

            if (temp > 0)
            {
                //calculator year
                if (temp >= (60 * 60 * 24 * 365))
                {
                    year = temp / (60 * 60 * 24 * 365);
                    temp = temp % (60 * 60 * 24 * 365);

                    //calculator month
                    if (temp >= (60 * 60 * 24 * 30))
                    {
                        month = temp / (60 * 60 * 24 * 30);
                        temp = temp % (60 * 60 * 24 * 30);

                        //calculator day
                        if (temp >= (60 * 60 * 24))
                        {
                            day = temp / (60 * 60 * 24);
                            temp = temp % (60 * 60 * 24);

                            //calculator hours
                            if (temp >= (60 * 60))
                            {
                                hours = time / (60 * 60);
                                temp = time % (60 * 60);

                                //calculator minutes
                                if (temp >= 60)
                                {

                                    minutes = temp / 60;
                                    //calculator second
                                    second = temp % 60;
                                }
                                else
                                {
                                    second = temp;
                                }
                            }
                            else
                            {
                                if (temp >= 60)
                                {

                                    minutes = temp / 60;
                                    //calculator second
                                    second = temp % 60;
                                }
                                else
                                {
                                    second = temp;
                                }
                            }
                        }
                        else
                        {
                            //calculator hours
                            if (temp >= (60 * 60))
                            {
                                hours = time / (60 * 60);
                                temp = time % (60 * 60);

                                //calculator minutes
                                if (temp >= 60)
                                {

                                    minutes = temp / 60;
                                    //calculator second
                                    second = temp % 60;
                                }
                                else
                                {
                                    second = temp;
                                }
                            }
                            else
                            {
                                if (temp >= 60)
                                {

                                    minutes = temp / 60;
                                    //calculator second
                                    second = temp % 60;
                                }
                                else
                                {
                                    second = temp;
                                }
                            }
                        }
                    }
                    else
                    {
                        //calculator day
                        if (temp >= (60 * 60 * 24))
                        {
                            day = temp / (60 * 60 * 24);
                            temp = temp % (60 * 60 * 24);

                            //calculator hours
                            if (temp >= (60 * 60))
                            {
                                hours = time / (60 * 60);
                                temp = time % (60 * 60);

                                //calculator minutes
                                if (temp >= 60)
                                {

                                    minutes = temp / 60;
                                    //calculator second
                                    second = temp % 60;
                                }
                                else
                                {
                                    second = temp;
                                }
                            }
                            else
                            {
                                if (temp >= 60)
                                {

                                    minutes = temp / 60;
                                    //calculator second
                                    second = temp % 60;
                                }
                                else
                                {
                                    second = temp;
                                }
                            }
                        }
                        else
                        {
                            //calculator hours
                            if (temp >= (60 * 60))
                            {
                                hours = time / (60 * 60);
                                temp = time % (60 * 60);

                                //calculator minutes
                                if (temp >= 60)
                                {

                                    minutes = temp / 60;
                                    //calculator second
                                    second = temp % 60;
                                }
                                else
                                {
                                    second = temp;
                                }
                            }
                            else
                            {
                                if (temp >= 60)
                                {

                                    minutes = temp / 60;
                                    //calculator second
                                    second = temp % 60;
                                }
                                else
                                {
                                    second = temp;
                                }
                            }
                        }
                    }
                }
                else
                {
                    //calculator month
                    if (temp >= (60 * 60 * 24 * 30))
                    {
                        month = temp / (60 * 60 * 24 * 30);
                        temp = temp % (60 * 60 * 24 * 30);

                        //calculator day
                        if (temp >= (60 * 60 * 24))
                        {
                            day = temp / (60 * 60 * 24);
                            temp = temp % (60 * 60 * 24);

                            //calculator hours
                            if (temp >= (60 * 60))
                            {
                                hours = time / (60 * 60);
                                temp = time % (60 * 60);

                                //calculator minutes
                                if (temp >= 60)
                                {

                                    minutes = temp / 60;
                                    //calculator second
                                    second = temp % 60;
                                }
                                else
                                {
                                    second = temp;
                                }
                            }
                            else
                            {
                                if (temp >= 60)
                                {

                                    minutes = temp / 60;
                                    //calculator second
                                    second = temp % 60;
                                }
                                else
                                {
                                    second = temp;
                                }
                            }
                        }
                        else
                        {
                            //calculator hours
                            if (temp >= (60 * 60))
                            {
                                hours = time / (60 * 60);
                                temp = time % (60 * 60);

                                //calculator minutes
                                if (temp >= 60)
                                {

                                    minutes = temp / 60;
                                    //calculator second
                                    second = temp % 60;
                                }
                                else
                                {
                                    second = temp;
                                }
                            }
                            else
                            {
                                if (temp >= 60)
                                {

                                    minutes = temp / 60;
                                    //calculator second
                                    second = temp % 60;
                                }
                                else
                                {
                                    second = temp;
                                }
                            }
                        }
                    }
                    else
                    {
                        //calculator day
                        if (temp >= (60 * 60 * 24))
                        {
                            day = temp / (60 * 60 * 24);
                            temp = temp % (60 * 60 * 24);

                            //calculator hours
                            if (temp >= (60 * 60))
                            {
                                hours = time / (60 * 60);
                                temp = time % (60 * 60);

                                //calculator minutes
                                if (temp >= 60)
                                {

                                    minutes = temp / 60;
                                    //calculator second
                                    second = temp % 60;
                                }
                                else
                                {
                                    second = temp;
                                }
                            }
                            else
                            {
                                if (temp >= 60)
                                {

                                    minutes = temp / 60;
                                    //calculator second
                                    second = temp % 60;
                                }
                                else
                                {
                                    second = temp;
                                }
                            }
                        }
                        else
                        {
                            //calculator hours
                            if (temp >= (60 * 60))
                            {
                                hours = time / (60 * 60);
                                temp = time % (60 * 60);

                                //calculator minutes
                                if (temp >= 60)
                                {

                                    minutes = temp / 60;
                                    //calculator second
                                    second = temp % 60;
                                }
                                else
                                {
                                    second = temp;
                                }
                            }
                            else
                            {
                                if (temp >= 60)
                                {

                                    minutes = temp / 60;
                                    //calculator second
                                    second = temp % 60;
                                }
                                else
                                {
                                    second = temp;
                                }
                            }
                        }
                    }
                }
            }

            Dictionary<String, int> result = new Dictionary<String, int>();
            result.Add("Year",year);
            result.Add("Month",month);
            result.Add("Day",day);
            result.Add("Hours",hours);
            result.Add("Minutes",minutes);
            result.Add("Second",second);
            return result;
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