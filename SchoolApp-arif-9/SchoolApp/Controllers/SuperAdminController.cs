using System.IO;
using System.Reflection;
using iTextSharp.text.pdf;
using SchoolApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolApp.Models.DbGateway;
using SchoolApp.Models.View;

namespace SchoolApp.Controllers
{
    public class SuperAdminController : Controller
    {
        //
        // GET: /SuperAdmin/
        SchoolDbGateWay aSchoolDbGateWay = new SchoolDbGateWay();
        public ActionResult SuperAdmin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuperAdmin(SuperAdmin aSuperAdmin)
        {
            string path = Server.MapPath("../Teacherprofile/password.txt");
            using (StreamReader reader = new StreamReader(path))
            {
                string line = null;
                while (null != (line = reader.ReadLine()))
                {
                    string[] values = line.Split(',');
                    if (values[0] == aSuperAdmin.AdminName && values[1] == aSuperAdmin.Password)
                    {
                        Session["SuperAdmin@31"] = aSuperAdmin.AdminName;
                        return RedirectToAction("SchoolList", "SuperAdmin");
                    }
                }
                ViewBag.Error = "Username Or Password not correct";
                return View();
            }
        }

        public ActionResult SchoolList()
        {
            if (Session["SuperAdmin@31"] == null)
            {
                return RedirectToAction("SuperAdmin", "SuperAdmin");
            }
            else
            {
                List<SchoolList> aSchoolLists = new List<SchoolList>();
                aSchoolLists = aSchoolDbGateWay.GetAllSchoolList();
                ViewBag.SchoolList = aSchoolLists;
                return View();
            }
        }

        public ActionResult GotoSchoolPanle(int? schoolId, int? userid, int? userlabel)
        {
            Session["school_id2133"] = schoolId;
            Session["user_id2133"] = userid;
            Session["userlevel301"] = userlabel;
            return RedirectToAction("Home", "Main");
        }
    }
}