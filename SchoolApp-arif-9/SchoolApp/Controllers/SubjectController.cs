using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolApp.Models;
using SchoolApp.Models.DbGateway;
using SchoolApp.Models.View;

namespace SchoolApp.Controllers
{
    public class SubjectController : Controller
    {
        //
        // GET: /Subject/
        ClassDbGateway aClassDbGateway = new ClassDbGateway();
        SubjectDbGateway aSubjectDbGateway = new SubjectDbGateway();
        public ActionResult SubjectView()
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null && Session["school_id2133"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                return View();
            }
           
        }

        public ActionResult AddSubject()
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
               int schoolId = Convert.ToInt32(Session["school_id2133"]);
                List<Class> classList = new List<Class>();
                classList = aClassDbGateway.GetAllClass(schoolId);

              
                List<Subject> aSubjectList = new List<Subject>();
                aSubjectList = aSubjectDbGateway.GetAllSubject(schoolId);

                ViewBag.SubjectList = aSubjectList;
                ViewBag.ClassList = classList;
               
                return View();

            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSubject(Subject aSubject)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                aSubject.SchoolId = Convert.ToInt32(Session["school_id2133"]);
                string successalert = aSubjectDbGateway.SaveSubject(aSubject);

                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                List<Class> classList = new List<Class>();
                classList = aClassDbGateway.GetAllClass(schoolId);

                List<Subject> aSubjectList = new List<Subject>();
                aSubjectList = aSubjectDbGateway.GetAllSubject(schoolId);

                ViewBag.SubjectList = aSubjectList;

                ViewBag.ClassList = classList;
                ViewBag.SuccessAlert = successalert;
                return View();

            }
        }

        public ActionResult EditSubject(int? id)
        {
            int schoolId = Convert.ToInt32(Session["school_id2133"]);
            int SubjectId = Convert.ToInt32(id);
            Subject aSubject = new Subject();
            aSubject = aSubjectDbGateway.GetPostInfo(SubjectId);

            List<Class> classList = new List<Class>();
            classList = aClassDbGateway.GetAllClass(schoolId);
            ViewBag.ClassList = classList;

            ViewBag.EditSubjectInfo = aSubject;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateSubjectData(Subject aSubject)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                if (aSubject != null)
                {
                    aSubject.SchoolId = Convert.ToInt32(Session["school_id2133"]);

                    string successalert = aSubjectDbGateway.UpdateSubject(aSubject);

                    int schoolId = Convert.ToInt32(Session["school_id2133"]);
                    List<Class> classList = new List<Class>();
                    classList = aClassDbGateway.GetAllClass(schoolId);
                    ViewBag.ClassList = classList;
                    ViewBag.SuccessAlert = successalert;
                    return View();
                }

                return View();
            }
        }



    }
}