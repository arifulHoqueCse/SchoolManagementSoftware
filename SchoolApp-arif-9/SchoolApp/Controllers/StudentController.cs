using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SchoolApp.Models;
using SchoolApp.Models.DbGateway;

namespace SchoolApp.Controllers
{
    public class StudentController : Controller
    {
        EmailDbGateway aEmailDbGateway = new EmailDbGateway();
        TeacherDbGateway aTeacherDbGateway = new TeacherDbGateway();
        StudentDbGateway aStudentDbGateway = new StudentDbGateway();
        NoticeBoardDbGateway aNoticeBoardDbGateway = new NoticeBoardDbGateway();
        //
        // GET: /Student/
        public ActionResult Index()
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null && Session["school_id2133"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                List<Class> classList = new List<Class>();
                classList = aStudentDbGateway.GetAllClass(schoolId);
                List<Student> studentList = new List<Student>();
                studentList = aStudentDbGateway.GetAllStudent(schoolId);
                ViewBag.StudentList = studentList;
                ViewBag.ClassList = classList;
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Student aStudent, HttpPostedFileBase file)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null && Session["school_id2133"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                string schoolCode = Convert.ToString(Session["school_Code23133"]);
                if (aStudent != null)
                {

                    if (file != null)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var myFileName = aStudent.StudentId + fileName;
                        var imagePath = Path.Combine(Server.MapPath("/Studentpicture"), myFileName);
                        file.SaveAs(imagePath);
                        aStudent.ImagePath = myFileName;
                        aStudent.SchoolId = Convert.ToInt32(Session["school_id2133"]);
                        string successalert = aStudentDbGateway.SaveStudent(aStudent, schoolId, schoolCode);
                        List<Student> aStudentList = new List<Student>();
                        aStudentList = aStudentDbGateway.GetAllStudent(schoolId);
                        ViewBag.StudentList = aStudentList;
                        List<Class> classList = new List<Class>();
                        classList = aStudentDbGateway.GetAllClass(schoolId);
                        ViewBag.ClassList = classList;
                        ViewBag.SuccessAlert = "Student Added Succesfully";
                        return View();
                    }
                    else
                    {
                        aStudent.SchoolId = Convert.ToInt32(Session["school_id2133"]);
                        aStudent.ImagePath = null;
                        string successalert = aStudentDbGateway.SaveStudent(aStudent, schoolId, schoolCode);
                        List<Student> aStudentList = new List<Student>();
                        aStudentList = aStudentDbGateway.GetAllStudent(schoolId);
                        ViewBag.StudentList = aStudentList;
                        List<Class> classList = new List<Class>();
                        classList = aStudentDbGateway.GetAllClass(schoolId);
                        ViewBag.ClassList = classList;
                        ViewBag.SuccessAlert = "Student Added Succesfully";
                        return View(); 
                    }
                }

                return View();
            }
        }

        public ActionResult StudentProfile()
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                Student aStudent = new Student();
                int StudentId = Convert.ToInt32(Session["user_id2133"]);//17;
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                aStudent = aStudentDbGateway.GetSpecificStudent(StudentId);
                List<NoticeBoard> noticeBoardList = new List<NoticeBoard>();
                noticeBoardList = GetAllNotice(schoolId);
                ViewBag.NoticeList = noticeBoardList;
                ViewBag.SpecificStudent = aStudent;
                return View();
            }
            
        }

        public ActionResult MyProfile()
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                Student aStudent = new Student();
                int StudentId = Convert.ToInt32(Session["user_id2133"]);//17;
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                aStudent = aStudentDbGateway.GetSpecificStudent(StudentId);
                List<NoticeBoard> noticeBoardList = new List<NoticeBoard>();
                noticeBoardList = GetAllNotice(schoolId);
                ViewBag.NoticeList = noticeBoardList;
                ViewBag.SpecificStudent = aStudent;
                return View();
            }

        }
        private List<NoticeBoard> GetAllNotice(int schoolId)
        {
            List<NoticeBoard> aNoticeBoardList = aNoticeBoardDbGateway.GetAllNotice(schoolId);
            return aNoticeBoardList;
        }

        public ActionResult EditStudentProfile(int? studentid)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                Student aStudent = new Student();
                int StudentId = Convert.ToInt32(Session["user_id2133"]);//17;
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                aStudent = aStudentDbGateway.GetSpecificStudent(StudentId);
                List<NoticeBoard> noticeBoardList = new List<NoticeBoard>();
                noticeBoardList = GetAllNotice(schoolId);
                ViewBag.NoticeList = noticeBoardList;
                ViewBag.SpecificStudent = aStudent;
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditStudentProfile(Student aStudent)
        {
            string saveSuccess = aStudentDbGateway.UpdateStudentProfile(aStudent);
            return RedirectToAction("StudentProfile", "Student");
        }

        [HttpGet]
        public ActionResult EditStudent(int? stuid)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null && Session["school_id2133"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                int studentId = Convert.ToInt32(stuid);
                List<Class> classList = new List<Class>();
                classList = aStudentDbGateway.GetAllClass(schoolId);
                Student aStudent = new Student();
                aStudent = aStudentDbGateway.GetSpecificStudent(studentId);
                ViewBag.Student = aStudent;
                ViewBag.ClassList = classList;
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateStudent(Student aStudent, HttpPostedFileBase file)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null && Session["school_id2133"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                string schoolCode = Convert.ToString(Session["school_Code23133"]);
                if (aStudent != null)
                {

                    if (file != null)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var myFileName = aStudent.RollNumber + fileName;
                        var imagePath = Path.Combine(Server.MapPath("/Studentpicture"), myFileName);
                        file.SaveAs(imagePath);
                        aStudent.ImagePath = myFileName;
                        aStudent.SchoolId = Convert.ToInt32(Session["school_id2133"]);
                        string successalert = aStudentDbGateway.UpdateStudent(aStudent, schoolId);
                        List<Class> classList = new List<Class>();
                        classList = aStudentDbGateway.GetAllClass(schoolId);
                        ViewBag.ClassList = classList;
                        ViewBag.SuccessAlert = successalert;
                        return RedirectToAction("Index", "Student");
                    }
                    else
                    {
                        aStudent.SchoolId = Convert.ToInt32(Session["school_id2133"]);
                        //aStudent.ImagePath = null;
                        string successalert = aStudentDbGateway.UpdateStudentImg(aStudent, schoolId);
                        List<Class> classList = new List<Class>();
                        classList = aStudentDbGateway.GetAllClass(schoolId);
                        ViewBag.ClassList = classList;
                        ViewBag.SuccessAlert = "Student Added Succesfully";
                        return RedirectToAction("Index", "Student");
                    }
                }

                return RedirectToAction("EditStudent", "Student");
            }
        }

        public ActionResult StudentCv(int? stuid)
        {

            Student aStudent = new Student();
            int StudentId = Convert.ToInt32(stuid);
            aStudent = aStudentDbGateway.GetSpecificStudent(StudentId);
            Session["StudentCvDownload"] = aStudent.StudentId;
            ViewBag.SpecificStudent = aStudent;
            return View();

        }


       

        public void StudentCvPrint()
        {
          
                Student aStudent = new Student();
                int StudentId = Convert.ToInt32(Session["StudentCvDownload"]);
                aStudent = aStudentDbGateway.GetSpecificStudent(StudentId);
                using (MemoryStream ms = new MemoryStream())
                {
                    // Creae the document object, assigning the page margins
                    Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                    PdfWriter writer = PdfWriter.GetInstance(document, ms);
                    // Open the document, enabeling writing to the document
                    document.Open();


                    var imageURL = Path.Combine(Server.MapPath("/Studentpicture"), aStudent.ImagePath);
                    iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
                    jpg.ScaleToFit(100f, 100f);

                    PdfPTable Logo = new PdfPTable(2);
                    Logo.DefaultCell.Border = Rectangle.NO_BORDER;

                    Logo.AddCell(new PdfPCell(jpg){Border = 0, BorderColor = BaseColor.WHITE });
                    Logo.AddCell(new PdfPCell() { Border = 0, BorderColor = BaseColor.WHITE });

                    PdfPTable table = new PdfPTable(2);
                    table.DefaultCell.Border = Rectangle.NO_BORDER;

                    PdfPCell cell = new PdfPCell();

                    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    cell.Colspan = 2;
                    cell.Border = 0;

                    table.AddCell(cell);

                    table.AddCell("Student Name");
                    table.AddCell(": " + " " + aStudent.StudentName);

                    table.AddCell("Father Name");
                    table.AddCell(": " + " " + aStudent.FatherName);

                    table.AddCell("Mother Name");
                    table.AddCell(": " + " " + aStudent.MotherName);

                    table.AddCell("Registration Number");
                    table.AddCell(": " + " " + aStudent.RegNo);

                    table.AddCell("Roll Number");
                    table.AddCell(": " + " " + aStudent.RollNumber);

                    table.AddCell("Date Of Birth");
                    table.AddCell(": " + " " + aStudent.Birthday);

                    table.AddCell("Sex");
                    table.AddCell(": " + " " + aStudent.Sex);

                    table.AddCell("Religion");
                    table.AddCell(": " + " " + aStudent.Religion);

                    table.AddCell("Blood Group");
                    table.AddCell(": " + " " + aStudent.BloodGroup);

                    table.AddCell("Contact Address");
                    table.AddCell(": " + " " + aStudent.ContactAddress);

                    table.AddCell("Phone Number");
                    table.AddCell(": " + " " + aStudent.PhoneNumber);

                    table.AddCell("Email");
                    table.AddCell(": " + " " + aStudent.Email);

                    PdfPTable footer = new PdfPTable(1);
                    footer.DefaultCell.Border = Rectangle.NO_BORDER;

                    string printfooter = Server.MapPath("/img/printfooter.png");
                        
                    iTextSharp.text.Image footerimg = iTextSharp.text.Image.GetInstance(printfooter);
                    footer.AddCell(footerimg);

                    PdfPTable Main = new PdfPTable(1);
                    Main.DefaultCell.Border = Rectangle.NO_BORDER;
                    PdfPCell Maincell = new PdfPCell();
                    Maincell.Border = PdfPCell.NO_BORDER;
                    Main.AddCell(Maincell);
                    Main.AddCell(new PdfPCell(Logo) { Border = 0, BorderColor = BaseColor.WHITE });
                    Main.AddCell(new PdfPCell(table) { Border = 0, BorderColor = BaseColor.WHITE });
                    Main.AddCell(new PdfPCell(footer) { Border = 0, BorderColor = BaseColor.WHITE });
                    
                   

                    document.Add(Main);
                    document.Close();
                    writer.Close();
                    ms.Close();
                    Response.ContentType = "pdf/application";
                    Response.AddHeader("content-disposition", "attachment;filename=First PDF document.pdf");
                    Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
                }

        }


        public ActionResult Email()
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int studentId = Convert.ToInt32(Session["user_id2133"]);
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                Student aStudent = new Student();
                aStudent = aStudentDbGateway.GetSpecificStudent(studentId);
                string StudentReg = aStudent.RegNo;
                List<Email> emailList = new List<Email>();
                emailList = aEmailDbGateway.GetStudentEmailList(StudentReg, schoolId);
                
                List<Teacher> aTeacherList = new List<Teacher>();
                aTeacherList = aTeacherDbGateway.GetAllTeacherList(schoolId);
                ViewBag.EmailList = emailList;
                ViewBag.TeacherList = aTeacherList;
                return View();
            }
        }


        [HttpPost]
        public ActionResult Email(Email aEmail)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int studentId = Convert.ToInt32(Session["user_id2133"]);
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                Student aStudent = new Student();
                aStudent = aStudentDbGateway.GetSpecificStudent(studentId);
                aEmail.StudentIdentity = aStudent.RegNo;
                aEmail.StudentReg = "0";

                List<Email> emailList = new List<Email>();
                emailList = aEmailDbGateway.GetStudentEmailList(aStudent.RegNo, schoolId);
                aEmail.Date = Convert.ToDateTime(DateTime.Now);
                string saveEmail = aEmailDbGateway.SaveEmail(aEmail, schoolId);

                List<Teacher> aTeacherList = new List<Teacher>();
                aTeacherList = aTeacherDbGateway.GetAllTeacherList(schoolId);
                ViewBag.EmailList = emailList;
                ViewBag.TeacherList = aTeacherList;
                ViewBag.SaveEmail = saveEmail;
                return View();
            }
        }

        public ActionResult ViewStudentEmail(int? id)
        {
            Email aEmail = new Email();
            int EmailId = Convert.ToInt32(id);//1;
            aEmail = aEmailDbGateway.GetSpecificEmail(EmailId);
            ViewBag.SpecificEmail = aEmail;
            return View();
        }

        public ActionResult ReplyStudentEmail(int? id)
        {
            Email aEmail = new Email();

            int EmailId = Convert.ToInt32(id);
            aEmail = aEmailDbGateway.GetSpecificEmail(EmailId);
            ViewBag.EmployeeIdentity = aEmail;

            return View();
        }

        [HttpPost]
        public ActionResult ReSendStudentEmail(Email aEmail)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int studentId = Convert.ToInt32(Session["user_id2133"]);
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                Student aStudent = new Student();
                aStudent = aStudentDbGateway.GetSpecificStudent(studentId);
                string StudentReg = aStudent.RegNo;
                aEmail.StudentIdentity = StudentReg;
                aEmail.StudentReg = "0";

                aEmail.EmployeeCode = aEmail.EmployeeIdentity;
                aEmail.EmployeeIdentity = "0";
                aEmail.Date = Convert.ToDateTime(DateTime.Now);
                string saveEmail = aEmailDbGateway.SaveEmail(aEmail, schoolId);
                RedirectToAction("Email", "Student");
                ViewBag.SaveEmail = saveEmail;
                return View();
            }
        }


        public ActionResult DeleteEmail(int? eid)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null && Session["school_id2133"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int EmailId = Convert.ToInt32(eid);
                string deleteSucc = aEmailDbGateway.DeleteEmail(EmailId);
                return RedirectToAction("Email", "Student");
            }
        }

       

        public ActionResult DeleteStudent(int? stuid)
        {
            ViewBag.StudentId = Convert.ToInt32(stuid);
            return View();
        }

        public ActionResult DeleteStudentConfirm(int? stuid)
        {
            int schoolId = Convert.ToInt32(Session["school_id2133"]);
            string deletesuccess = aStudentDbGateway.DeleteStudent(stuid, schoolId);
            return RedirectToAction("Index", "Student");
        }

        public ActionResult ViewHomeWork()
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null && Session["school_id2133"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                Student aStudent = new Student();
                int StudentId = Convert.ToInt32(Session["user_id2133"]);
                aStudent = aStudentDbGateway.GetSpecificStudent(StudentId);
                int classId = aStudent.ClassId;
                int sectionId = aStudent.SectionId;
                int schoolId = aStudent.SchoolId;
                List<HomeWork> aHomeWork = new List<HomeWork>();
                aHomeWork = aStudentDbGateway.GetMyHomeWork(classId, sectionId, schoolId);
                ViewBag.HomeWork = aHomeWork;  
                return View();
            }
        }

        public ActionResult HomeWorkDetails(int? hwid)
        {
            HomeWork aHomeWork = new HomeWork();
            int HwId = Convert.ToInt32(hwid);
            aHomeWork = aStudentDbGateway.FindHomeWork(HwId);
            ViewBag.HomeWork = aHomeWork;
            return View();
        }

    }
}