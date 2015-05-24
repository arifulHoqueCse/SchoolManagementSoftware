using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Web;
using System.Web.Mvc;
using SchoolApp.Models;
using SchoolApp.Models.DbGateway;
using SchoolApp.Models.View;

namespace SchoolApp.Controllers
{
    public class MainController : Controller
    {
        LoginDbGateway aGateway = new LoginDbGateway();
        //
        // GET: /Main/
        public ActionResult Index()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Admin aAdmin)
        {
            AdminLoginView adminLoginView;
            adminLoginView = aGateway.AdminLogin(aAdmin);
            Session["school_id2133"] = adminLoginView.SchoolId;
            Session["user_id2133"] = adminLoginView.UserId;
            Session["userlevel301"] = adminLoginView.UserLevel;
            if (adminLoginView.UserLevel == 1)
            {
                return RedirectToAction("Home","Main");
            }
            else if (adminLoginView.UserLevel == 2)
            {
                return RedirectToAction("TeacherProfile", "Teacher");
            }
            else if (adminLoginView.UserLevel == 3)
            {
                return RedirectToAction("StudentProfile", "Student");
            }

            ViewBag.errorMSg = "Email or Password not correct";

            return View();
        }

        public ActionResult Student()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Student(Student aStudent)
        {
            return View();
        }

        public ActionResult Home()
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                AdminLoginView adminLogin = new AdminLoginView();
                adminLogin = aGateway.GetSchoolInformation(schoolId);
                Session["school_Code23133"] = adminLogin.SchoolCode;
                ViewBag.LoginInfomation = adminLogin;
                return View();
            }
        }
        [HttpGet]
        public ActionResult SchoolSettigs(int? schoolid)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int SchoolId = Convert.ToInt32(schoolid);
                School aSchool = new School();
                aSchool = aGateway.GetSchoolInfo(SchoolId);
                ViewBag.SchoolAbout = aSchool;
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SchoolSettigs(School aSchool)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                string saveAlert = aGateway.UpdateSchoolData(aSchool);
                return RedirectToAction("Home", "Main");
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Main");
        }


        public ActionResult PlayNationSong()
        {
            string s = "D:\\School-Soft-Sounds/Alarm09.wav";
            SoundPlayer mplayer = new SoundPlayer(); mplayer.SoundLocation = s; mplayer.Play();
             return RedirectToAction("Home", "Main");

        }

        public ActionResult PlayBell()
        {
            string s = "D:\\School-Soft-Sounds/PlayBell.wav";
            SoundPlayer mplayer = new SoundPlayer(); mplayer.SoundLocation = s; mplayer.Play();
            return RedirectToAction("Home", "Main");

        }

       
    }
}