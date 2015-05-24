using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolApp.Models;
using SchoolApp.Models.DbGateway;

namespace SchoolApp.Controllers
{
    public class AccountsController : Controller
    {
        ExamDbGateway aExamDbGateway = new ExamDbGateway();
        AccountsDbGateway accountsDbGateway = new AccountsDbGateway();
        StudentDbGateway aStudentDbGateway = new StudentDbGateway();
        //
        // GET: /Accounts/
        public ActionResult StudentAccountsList()
        {
            int schoolId = Convert.ToInt32(Session["school_id2133"]);
            List<Student> aStudentList = new List<Student>();
            aStudentList = aStudentDbGateway.GetAllStudent(schoolId);
            ViewBag.StudentList = aStudentList;
            return View();
        }

        
        public ActionResult StudentFeeSubmission(int? stuid)
        {
            int StudentId = Convert.ToInt32(stuid);
            Student aStudent = new Student();
            aStudent = aStudentDbGateway.GetSpecificStudent(StudentId);

            int schoolId = Convert.ToInt32(Session["school_id2133"]);
            List<Exam> aExamList = new List<Exam>();
            aExamList = aExamDbGateway.GetAllExam(schoolId);

            ViewBag.ExamList = aExamList;
            ViewBag.StudentInfo = aStudent;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitStudentFee(AccountsFee aAccountsFee)
        {
            aAccountsFee.PaymentDate = DateTime.Now;
            String success = accountsDbGateway.SubmitFee(aAccountsFee);

            ViewBag.SubmitFee = success;
            return View();
        }


    }
}