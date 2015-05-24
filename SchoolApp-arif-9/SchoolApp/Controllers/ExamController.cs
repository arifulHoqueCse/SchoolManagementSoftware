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
    public class ExamController : Controller
    {
        ExamDbGateway aExamDbGateway = new ExamDbGateway();
        ClassDbGateway aClassDbGateway = new ClassDbGateway();
        StudentDbGateway aStudentDbGateway = new StudentDbGateway();
        SubjectDbGateway aSubjectDbGateway = new SubjectDbGateway();
        //
        // GET: /Exam/
        public ActionResult Exam()
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                List<Exam> aExamList = new List<Exam>();
                aExamList = aExamDbGateway.GetAllExam(schoolId);

                List<Class> classList = new List<Class>();
                classList = aClassDbGateway.GetAllClass(schoolId);

                ViewBag.ClassList = classList;
                ViewBag.ExamList = aExamList;
                return View();
            }
        }

        [HttpPost]
        public ActionResult Exam(Exam aExam)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                if (aExam != null)
                {
                    aExam.SchoolId = Convert.ToInt32(Session["school_id2133"]);
                    int schoolId = aExam.SchoolId;
                    string successalert = aExamDbGateway.SaveExam(aExam);
                    List<Exam> aExamList = new List<Exam>();
                    aExamList = aExamDbGateway.GetAllExam(schoolId);
                    List<Class> classList = new List<Class>();
                    classList = aClassDbGateway.GetAllClass(schoolId);

                    ViewBag.ClassList = classList;

                    ViewBag.ExamList = aExamList;
                    ViewBag.SuccessAlert = "Book Added Succesfully";

                    return View();
                }
                return View();
            }
        }
        public ActionResult EditExam(int? id)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int ExampId = Convert.ToInt32(id);
                Exam aExam = new Exam();
                aExam = aExamDbGateway.GetPostInfo(ExampId);

                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                List<Class> classList = new List<Class>();
                classList = aClassDbGateway.GetAllClass(schoolId);

                //ViewBag.ClassList = classList;
                ViewBag.Examid = id;
                ViewBag.EditExamInfo = aExam;
                return View();
            }
        }

        [HttpPost]
        public ActionResult UpdateExam(Exam aExam)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                if (aExam != null)
                {
                    string successalert = aExamDbGateway.UpdateMyExam(aExam);
                    ViewBag.UpdateData = successalert;
                    return View();
                }
                return View();
            }
        }

        public ActionResult ExamGrade()
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                List<ExamGrade> examGradeList = new List<ExamGrade>();
                examGradeList = GetExamGradeList(schoolId);
                ViewBag.ExamGradeList = examGradeList;
                return View();
            }
        }

        private List<ExamGrade> GetExamGradeList(int schoolId)
        {
            List<ExamGrade> aExamGradeList = aExamDbGateway.GetExamGradeList(schoolId);
            return aExamGradeList;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExamGrade(ExamGrade aExamGrade)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                aExamGrade.SchoolId = schoolId;
                string success = aExamDbGateway.SaveExamGrade(aExamGrade);
                List<ExamGrade> examGradeList = new List<ExamGrade>();
                examGradeList = GetExamGradeList(schoolId);
                ViewBag.ExamGradeList = examGradeList;
                ViewBag.SaveAlert = success;
                return View();
            }
        }

        public ActionResult MangeMark()
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                List<Class> aClassList = new List<Class>();
                aClassList = aClassDbGateway.GetAllClass(schoolId);
                ViewBag.ClassList = aClassList;
                return View();
            }
        }

        public ActionResult ManageClassMark(int? classid, int? success, int? stuid)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int classId = Convert.ToInt32(classid);
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                
                List<Subject> aSubjectList = new List<Subject>();
                List<Exam> aExamList = new List<Exam>(); 
                aSubjectList = aSubjectDbGateway.GetSubjectOfClass(classid, schoolId);
                
                aExamList = aExamDbGateway.GetAllExam(schoolId);
                ViewBag.ExamList = aExamList;
                ViewBag.SubjectList = aSubjectList;
                ViewBag.StudentList = stuid;
                ViewBag.ClassId = classId;
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageClassMark(Mark aMark)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                aMark.SchoolId = schoolId;
                string saveSuccess = aExamDbGateway.ManageClassMark(aMark);
                ViewBag.Successalert = saveSuccess;
                return RedirectToAction("ManageClassMark", "Exam", new { classid=aMark.ClassId, success=1});
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateManageClassMark(Mark aMark)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                aMark.SchoolId = schoolId;
                string saveSuccess = aExamDbGateway.UpdateManageClassMark(aMark);
                ViewBag.Successalert = saveSuccess;
                return RedirectToAction("EditResult", "Exam", new { classid = aMark.ClassId, stuid=aMark.StudentId, success = 1 });
            }
        }
        public ActionResult ClassStudent(int? classid)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int classId = Convert.ToInt32(classid);
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                List<Student> aStudentList = new List<Student>();
                aStudentList = aStudentDbGateway.GetTheClassStudent(classId, schoolId);
                ViewBag.StudentList = aStudentList;
                return View();
            }
        }
        public ActionResult ViewResult(int? classid)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int classId = Convert.ToInt32(classid);
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                List<Student> aStudentList = new List<Student>();
                List<Subject> aSubjectList = new List<Subject>();
                List<Exam> aExamList = new List<Exam>();
                aSubjectList = aSubjectDbGateway.GetSubjectOfClass(classid, schoolId);
                aStudentList = aStudentDbGateway.GetTheClassStudent(classId, schoolId);
                aExamList = aExamDbGateway.GetAllExam(schoolId);
                ViewBag.ExamList = aExamList;
                ViewBag.SubjectList = aSubjectList;
                ViewBag.StudentList = aStudentList;
                ViewBag.ClassId = classId;
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ViewResult(int? classid, Mark aMark)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int classId = Convert.ToInt32(classid);
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                List<Student> aStudentList = new List<Student>();
                List<Subject> aSubjectList = new List<Subject>();
                List<Exam> aExamList = new List<Exam>();
                List<ExamResultView> aExamResultList = new List<ExamResultView>();
                aMark.SchoolId = schoolId;
                aExamResultList = aExamDbGateway.GetTheStudentResult(aMark);
                aSubjectList = aSubjectDbGateway.GetSubjectOfClass(classid, schoolId);
                aStudentList = aStudentDbGateway.GetTheClassStudent(classId, schoolId);
                aExamList = aExamDbGateway.GetAllExam(schoolId);
                ViewBag.ExamList = aExamList;
                ViewBag.SubjectList = aSubjectList;
                ViewBag.StudentList = aStudentList;
                ViewBag.ClassId = classId;
                ViewBag.StudentResult = aExamResultList;
                return View();
            }
        }

        public ActionResult EditResult(int? classid, int? stuid)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int classId = Convert.ToInt32(classid);
                int schoolId = Convert.ToInt32(Session["school_id2133"]);

                List<Subject> aSubjectList = new List<Subject>();
                List<Exam> aExamList = new List<Exam>();
                aSubjectList = aSubjectDbGateway.GetSubjectOfClass(classid, schoolId);

                aExamList = aExamDbGateway.GetAllExam(schoolId);
                ViewBag.ExamList = aExamList;
                ViewBag.SubjectList = aSubjectList;
                ViewBag.StudentList = stuid;
                ViewBag.ClassId = classId;
                return View();
            }
        }

        public ActionResult MangeMarkView()
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                int studentId = Convert.ToInt32(Session["user_id2133"]);
                Student aStudent = new Student();
                aStudent = aStudentDbGateway.GetSpecificStudent(studentId);

                List<Exam> aExamList = new List<Exam>();
                aExamList = aExamDbGateway.GetAllExam(schoolId);
                ViewBag.StudentId = aStudent.StudentId;
                ViewBag.ClassId = aStudent.ClassId;
                ViewBag.ExamList = aExamList;
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MangeMarkView(Mark aMark)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                int studentId = Convert.ToInt32(Session["user_id2133"]);
                Student aStudent = new Student();
                aStudent = aStudentDbGateway.GetSpecificStudent(studentId);
                aMark.SchoolId = schoolId;
                List<ExamResultView> aExamResultList = new List<ExamResultView>();
                aExamResultList = aExamDbGateway.GetTheStudentResult(aMark);
                List<Exam> aExamList = new List<Exam>();
                aExamList = aExamDbGateway.GetAllExam(schoolId);
                ViewBag.StudentId = aStudent.StudentId;
                ViewBag.ResultList = aExamResultList;
                ViewBag.ClassId = aStudent.ClassId;
                ViewBag.ExamList = aExamList;
                return View();
            }
        }

        public ActionResult ExamView()
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int schoolId = Convert.ToInt32(Session["school_id2133"]);

                List<Exam> aExamList = new List<Exam>();
                aExamList = aExamDbGateway.GetAllExam(schoolId);

                List<Class> classList = new List<Class>();
                classList = aStudentDbGateway.GetAllClass(schoolId);

                ViewBag.ClassList = classList;
                ViewBag.ExamList = aExamList;
                return View();
            }
        }

        public ActionResult ExamRoutine()
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int schoolId = Convert.ToInt32(Session["school_id2133"]);

                List<Class> classList = new List<Class>();
                classList = aStudentDbGateway.GetAllClass(schoolId);
                
                List<Exam> aExamList = new List<Exam>();
                aExamList = aExamDbGateway.GetAllExam(schoolId);

                List<ExamRoutineView> aExamRoutines = new List<ExamRoutineView>();
                aExamRoutines = aExamDbGateway.GetAllExamRoutine(schoolId);

                ViewBag.ExamRoutine = aExamRoutines;
                ViewBag.ExamList = aExamList;
                ViewBag.ClassList = classList;
               
                return View();
            }
        }
        [HttpPost]
        public ActionResult ExamRoutine(ExamRoutine aExamRoutine)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                List<Class> classList = new List<Class>();
                classList = aStudentDbGateway.GetAllClass(schoolId);

                aExamRoutine.SchoolId = Convert.ToInt32(Session["school_id2133"]);
                
                string Success = aExamDbGateway.SaveExamRoutine(aExamRoutine);
                List<Exam> aExamList = new List<Exam>();
                aExamList = aExamDbGateway.GetAllExam(schoolId);
                ViewBag.ExamList = aExamList;
                ViewBag.Success = Success;
                ViewBag.ClassList = classList;

                return View();
            }
        }

        public ActionResult EditExamRoutine(int? id)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                List<Class> classList = new List<Class>();
                classList = aStudentDbGateway.GetAllClass(schoolId);

                List<Exam> aExamList = new List<Exam>();
                aExamList = aExamDbGateway.GetAllExam(schoolId);

                int Id = Convert.ToInt32(id);
                ExamRoutine aExamRoutine = new ExamRoutine();
                aExamRoutine = aExamDbGateway.GetExamRoutineInfo(id);

                ViewBag.ExamRoutineInfo = aExamRoutine;
                ViewBag.ExamList = aExamList;
                ViewBag.ClassList = classList;

                return View();
            }

        }

        public ActionResult UpdateExamRoutine(ExamRoutine aExamRoutine)
        {
            string update = aExamDbGateway.UpdateExamRoutine(aExamRoutine);

            ViewBag.UpdateSuccess = update;
            return View();
        }

        public ActionResult ExamRoutineView()
        {
            int schoolId = Convert.ToInt32(Session["school_id2133"]);
            int studentId = Convert.ToInt32(Session["user_id2133"]);
            Student aStudent = new Student();
            aStudent = aStudentDbGateway.GetSpecificStudent(studentId);
            int clssId = aStudent.ClassId;
            List<ExamRoutine> aExamRoutine = new List<ExamRoutine>();
            aExamRoutine = aExamDbGateway.GetExamRoutineInfo(schoolId, clssId);

            ExamRoutine aRoutineTitle = new ExamRoutine();
            aRoutineTitle = aExamDbGateway.GetExamRoutineTitle(schoolId, clssId);

            ViewBag.ExamRoutineTitle = aRoutineTitle;
            ViewBag.ExamRoutine = aExamRoutine;

            return View();
        }

        public ActionResult DeleteExamRoutine(int? id)
        {
            string success = aExamDbGateway.DeleteExamRoutine(id);
            return RedirectToAction("ExamRoutine", "Exam");

        }
    }
}