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
    public class AttendenceController : Controller
    {
        //
        // GET: /Attendence/
        StudentDbGateway aStudentDbGateway = new StudentDbGateway();
        ClassDbGateway aClassDbGateway = new ClassDbGateway();
        AttendenceDbGateway aDbGateway= new AttendenceDbGateway();
        public ActionResult Attendence()
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                List<Class> aClasseList = new List<Class>();
                aClasseList = aClassDbGateway.GetAllClass(schoolId);
                ViewBag.ClassList = aClasseList;
                return View();
            }
            
        }

        [HttpGet]
        public ActionResult AttendenceShit(int? classid, int?sectionid)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int classId = Convert.ToInt32(classid);
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                int sectionId = Convert.ToInt32(sectionid);
                List<Student> studentList = new List<Student>();
                studentList = aStudentDbGateway.GetTheClassStudent(classId, schoolId, sectionId);
                ViewBag.ClassID = classId;
                ViewBag.sectionId = sectionid;
                ViewBag.StudentList = studentList;
                return View();
            }
        }

        public JsonResult SaveAttendence(int studentid, int status, string attendate, string shiftimes, int classid, int sectionid)
        {
            int schoolId = Convert.ToInt32(Session["school_id2133"]);
            int teacherId = Convert.ToInt32(Session["user_id2133"]);
            Attendance aAttendance = new Attendance();
            aAttendance.AttendanceStatus = status;
            aAttendance.AttendanceDate = Convert.ToDateTime(attendate);
            aAttendance.ShiftTime = shiftimes;
            aAttendance.StudentId = studentid;
            aAttendance.SchoolId = schoolId;
            aAttendance.TeacherId = teacherId;
            aAttendance.ClassId = classid;
            aAttendance.SectionId = sectionid;
            bool isSave = aDbGateway.SaveAttendence(aAttendance);
            if (isSave)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ReportAttendence()
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                List<Class> aClasseList = new List<Class>();
                aClasseList = aClassDbGateway.GetAllClass(schoolId);
                ViewBag.ClassList = aClasseList;
                return View();
            }
        }

        public ActionResult ClassReport(int? classid, int? sectionid)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                List<Student> studentList = new List<Student>();
                studentList = aDbGateway.GetTheClassStudent(classid, sectionid, schoolId);
                ViewBag.StudentList = studentList;
                return View();
            }
        }

        public ActionResult AttendenceDetails(int? stuid, int? classid, int? sectionid)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                List<AttendenceView> attendenceList = new List<AttendenceView>();
                attendenceList = aDbGateway.GetTheStudentAttendence(stuid, classid, schoolId, sectionid);
                ViewBag.StudentAttendence = attendenceList;
                return View();             
            }
        }
        public ActionResult AttendenceView(int? stuid, int? classid)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                int StudentId = Convert.ToInt32(Session["user_id2133"]);
                Student aStudent = new Student();
                aStudent = aStudentDbGateway.GetSpecificStudent(StudentId);
                int countAttendance = aDbGateway.CountPresentStatus(StudentId, aStudent.ClassId);
                ViewBag.StudentInfo = aStudent;
                ViewBag.AttendentCount = countAttendance;
                return View();
            }
        }
        public ActionResult AttendenceViewDetail(int? stuid, int? classid)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                List<AttendenceView> attendenceList = new List<AttendenceView>();
                attendenceList = aDbGateway.GetTheStudentAttendence(stuid, classid, schoolId);
                ViewBag.StudentAttendence = attendenceList;
                return View();
            }
        }
	}
}