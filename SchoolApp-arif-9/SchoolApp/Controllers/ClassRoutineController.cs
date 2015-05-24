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
    public class ClassRoutineController : Controller
    {
        ClassDbGateway aClassDbGateway = new ClassDbGateway();
        SubjectDbGateway aSubjectDbGateway = new SubjectDbGateway();
        //
        // GET: /ClassRoutine/
        public ActionResult Routine()
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                List<Class> classList = new List<Class>();
                List<Subject> subjectList = new List<Subject>();
                subjectList = GetTheListOfSubject(schoolId);
                classList = aClassDbGateway.GetAllClass(schoolId);
                ViewBag.ClassList = classList;
                ViewBag.SubjectList = subjectList;
                return View();
            }
        }

        private List<Subject> GetTheListOfSubject(int schoolId)
        {
            List<Subject> aSubjectList = new List<Subject>();
            aSubjectList = aSubjectDbGateway.GetTheListOfSubject(schoolId);
            return aSubjectList;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Routine(ClassRoutine aRoutine)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                aRoutine.FullStartTime = aRoutine.TimeStart + " " + aRoutine.StartAmPm;
                aRoutine.FullEndTime = aRoutine.TimeEnd + " " + aRoutine.EndAmPm;
                aRoutine.SchoolId = schoolId;
                string successSave = aClassDbGateway.SaveClassRoutine(aRoutine);
                List<Class> classList = new List<Class>();
                List<Subject> subjectList = new List<Subject>();
                subjectList = GetTheListOfSubject(schoolId);
                classList = aClassDbGateway.GetAllClass(schoolId);
                ViewBag.SaveSuccessRoutine = successSave;
                ViewBag.ClassList = classList;
                ViewBag.SubjectList = subjectList;
                return View();
            }
        }
        [HttpGet]
        public ActionResult EditRoutine(int? id, int? classid)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                int routineId = Convert.ToInt32(id);
                int classId = Convert.ToInt32(classid);

                List<Class> classList = new List<Class>();
                List<Subject> subjectList = new List<Subject>();
                subjectList = GetTheListOfSubject(schoolId);
                classList = aClassDbGateway.GetAllClass(schoolId);

                List<EditRoutine> aRoutineList = new List<EditRoutine>();
                aRoutineList = aClassDbGateway.GetTheRoutine(routineId, classId, schoolId);

                ViewBag.RoutineId = routineId;
                ViewBag.ClassList = classList;
                ViewBag.SubjectList = subjectList;
                ViewBag.EditRoutine = aRoutineList;
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRoutine(ClassRoutine aRoutine, int routineid)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                aRoutine.FullStartTime = aRoutine.TimeStart + " " + aRoutine.StartAmPm;
                aRoutine.FullEndTime = aRoutine.TimeEnd + " " + aRoutine.EndAmPm;
                aRoutine.SchoolId = schoolId;
                int routineId = Convert.ToInt32(routineid);
                string updateRoutine = aClassDbGateway.UpdateRoutine(aRoutine, routineId);

                List<Class> classList = new List<Class>();
                List<Subject> subjectList = new List<Subject>();
                subjectList = GetTheListOfSubject(schoolId);
                classList = aClassDbGateway.GetAllClass(schoolId);

                ViewBag.RoutineId = routineId;
                ViewBag.ClassList = classList;
                ViewBag.SubjectList = subjectList;
                ViewBag.UpdateSuccses = updateRoutine;
                return View();
            }
        }

        public ActionResult RoutineView()
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null && Session["school_id2133"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int studentId = Convert.ToInt32(Session["user_id2133"]);
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                Class aClass = new Class();
                aClass = aClassDbGateway.GetTheStudentClassName(studentId, schoolId);
                ViewBag.StudentClass = aClass;
                return View();
            }
        }
    }
}