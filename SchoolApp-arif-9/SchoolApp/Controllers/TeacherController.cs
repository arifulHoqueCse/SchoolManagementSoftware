using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.VisualBasic.Devices;
using SchoolApp.Models;
using SchoolApp.Models.DbGateway;
using SchoolApp.Models.View;

namespace SchoolApp.Controllers
{
    public class TeacherController : Controller
    {
        EmailDbGateway aEmailDbGateway = new EmailDbGateway();
        TeacherDbGateway aTeacherDbGateway = new TeacherDbGateway();
        StudentDbGateway aStudentDbGateway = new StudentDbGateway();
        //
        // GET: /Teacher/
        public ActionResult Index()
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null && Session["school_id2133"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                List<Teacher> aTeacherList = new List<Teacher>();
                aTeacherList = aTeacherDbGateway.GetAllTeacher(schoolId);
                ViewBag.TeacherList = aTeacherList;
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Teacher aTeacher, HttpPostedFileBase file)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null && Session["school_id2133"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                if (aTeacher != null)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var myFileName = aTeacher.TeacherId + fileName;
                    var imagePath = Path.Combine(Server.MapPath("/Teacherprofile"), myFileName);
                    file.SaveAs(imagePath);
                    aTeacher.ImagePath = fileName;
                    aTeacher.SchoolId = schoolId;
                    string schoolCode = Convert.ToString(Session["school_Code23133"]);
                    string successalert = aTeacherDbGateway.SaveTeacher(aTeacher, schoolCode);
                    List<Teacher> aTeacherList = new List<Teacher>();
                    aTeacherList = aTeacherDbGateway.GetAllTeacher(schoolId);
                    ViewBag.TeacherList = aTeacherList;
                    ViewBag.SuccessAlert = "Teacher Added Succesfully";
                    return View();
                }
                return View();
            }
        }

        [HttpPost]
        public JsonResult FindDuplicateEmail(string email)
        {
            bool findMail = aTeacherDbGateway.FindDuplicateEmail(email);
            if (findMail)
            {
                return Json(email == null);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult EditTeacher(int? id)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null && Session["school_id2133"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int teacherId = Convert.ToInt32(id);
                Teacher aTeacher = new Teacher();
                aTeacher = aTeacherDbGateway.GetPostInfo(teacherId);
                ViewBag.EditTeacherInfo = aTeacher;
                return View();
            }
        }

        public ActionResult DeleteTeacher(int? id)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null && Session["school_id2133"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                ViewBag.teacherId = id;
                return View();
            }
        }

        public ActionResult DeleteTeacherConfirm(int? id)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null && Session["school_id2133"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                int teacherId = Convert.ToInt32(id);
                string success = aTeacherDbGateway.DeleteTeacher(schoolId, teacherId);
                return RedirectToAction("Index", "Teacher");
            }
        }

        [HttpPost]
        public ActionResult UpdateTeacherData(TeacherUpdate aTeacherUpdate, HttpPostedFileBase file)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null && Session["school_id2133"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                if (aTeacherUpdate != null)
                {
                    if (file != null)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var imagePath = Path.Combine(Server.MapPath("/Teacherprofile"), fileName);
                        file.SaveAs(imagePath);
                        aTeacherUpdate.ImagePath = fileName;
                        string successalert = aTeacherDbGateway.UpdateTeacherData(aTeacherUpdate);
                        ViewBag.UpdateData = successalert;
                        return View();
                    }
                    else
                    {
                        string successalert = aTeacherDbGateway.UpdateTeacherDataImg(aTeacherUpdate);
                        ViewBag.UpdateData = successalert;
                        return View();
                    }
                }
                return View();
            }
        }

        public ActionResult TeacherProfile()
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null && Session["school_id2133"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                Teacher aTeacher = new Teacher();
                int TeacherId = Convert.ToInt32(Session["user_id2133"]); //1;
                aTeacher = aTeacherDbGateway.GetSpecificTeacher(TeacherId);
                ViewBag.SpecificTeacher = aTeacher;
                return View();
            }
        }

        public ActionResult MyProfile()
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null && Session["school_id2133"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                Teacher aTeacher = new Teacher();
                int TeacherId = Convert.ToInt32(Session["user_id2133"]); //1;
                aTeacher = aTeacherDbGateway.GetSpecificTeacher(TeacherId);
                ViewBag.SpecificTeacher = aTeacher;
                return View();
            }
        }

        public ActionResult TeacherListView()
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null && Session["school_id2133"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                List<Teacher> TeacherList = new List<Teacher>();
                TeacherList = aTeacherDbGateway.GetAllTeacherList(schoolId);
                ViewBag.TeacherList = TeacherList;
                return View();
            }
        }

        public ActionResult Email()
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null && Session["school_id2133"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int employeeId = Convert.ToInt32(Session["user_id2133"]);
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                Teacher aTeacher = new Teacher();
                aTeacher = aTeacherDbGateway.GetSpecificTeacher(employeeId);
                string EmployeeCode = aTeacher.EmployeeCode;
                List<Email> emailList = new List<Email>();

                emailList = aEmailDbGateway.GetTeacherEmailList(EmployeeCode, schoolId);
                ViewBag.EmailList = emailList;
                return View();
            }
        }


        [HttpPost]
        public ActionResult Email(Email aEmail)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null && Session["school_id2133"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int employeeId = Convert.ToInt32(Session["user_id2133"]);
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                Teacher aTeacher = new Teacher();
                aTeacher = aTeacherDbGateway.GetSpecificTeacher(employeeId);
                string EmployeeCode = aTeacher.EmployeeCode;

                aEmail.EmployeeIdentity = EmployeeCode;
                aEmail.EmployeeCode = "0";
                aEmail.Date = Convert.ToDateTime(DateTime.Now);
                string saveEmail = aEmailDbGateway.SaveEmail(aEmail, schoolId);
                ViewBag.SaveEmail = saveEmail;


                List<Email> emailList = new List<Email>();

                emailList = aEmailDbGateway.GetTeacherEmailList(EmployeeCode, schoolId);
                ViewBag.EmailList = emailList;
                return View();
            }
        }


        public ActionResult ViewTeacherEmail(int? id)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null && Session["school_id2133"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                Email aEmail = new Email();
                int EmailId = Convert.ToInt32(id); //1;
                aEmail = aEmailDbGateway.GetSpecificEmail(EmailId);
                ViewBag.SpecificEmail = aEmail;
                return View();
            }
        }

        public ActionResult ReplyTeacherEmail(int? id)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null && Session["school_id2133"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                Email aEmail = new Email();

                int EmailId = Convert.ToInt32(id);
                aEmail = aEmailDbGateway.GetSpecificEmail(EmailId);
                ViewBag.StudentIdentity = aEmail;

                return View();
            }
        }


        [HttpPost]
        public ActionResult ReSendTeacherEmail(Email aEmail)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null && Session["school_id2133"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int employeeId = Convert.ToInt32(Session["user_id2133"]);
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                Teacher aTeacher = new Teacher();
                aTeacher = aTeacherDbGateway.GetSpecificTeacher(employeeId);
                string EmployeeCode = aTeacher.EmployeeCode;
                aEmail.EmployeeIdentity = EmployeeCode;
                aEmail.EmployeeCode = "0";
                aEmail.StudentReg = aEmail.StudentIdentity;
                aEmail.StudentIdentity = "0";
                aEmail.Date = Convert.ToDateTime(DateTime.Now);
                string saveEmail = aEmailDbGateway.SaveEmail(aEmail, schoolId);
                RedirectToAction("ReplyTeacherEmail", "Teacher", new { id = aEmail.StudentReg });
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
                return RedirectToAction("Email", "Teacher");
            }
        }

        public ActionResult TeacherCv(int? id)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null && Session["school_id2133"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                Teacher aTeacher = new Teacher();
                int TeacherId = Convert.ToInt32(id);
                aTeacher = aTeacherDbGateway.GetSpecificTeacher(TeacherId);
                Session["TeacherCvDownload"] = aTeacher.TeacherId;
                ViewBag.SpecificTeacher = aTeacher;
                return View();
            }

        }
        public ActionResult TeacherCvView(int? id)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null && Session["school_id2133"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                Teacher aTeacher = new Teacher();
                int TeacherId = Convert.ToInt32(id);
                aTeacher = aTeacherDbGateway.GetSpecificTeacher(TeacherId);
                Session["TeacherCvDownload"] = aTeacher.TeacherId;
                ViewBag.SpecificTeacher = aTeacher;
                return View();
            }

        }


        public void TeacherCvPrint()
        {

            Teacher aTeacher = new Teacher();
            int TeacherId = Convert.ToInt32(Session["TeacherCvDownload"]);
            aTeacher = aTeacherDbGateway.GetSpecificTeacher(TeacherId);
            using (MemoryStream ms = new MemoryStream())
            {
                // Creae the document object, assigning the page margins
                Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                PdfWriter writer = PdfWriter.GetInstance(document, ms);
                // Open the document, enabeling writing to the document
                document.Open();


                var imageURL = Path.Combine(Server.MapPath("/Teacherprofile"), aTeacher.ImagePath);
                iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
                jpg.ScaleToFit(100f, 100f);

                PdfPTable Logo = new PdfPTable(2);
                Logo.DefaultCell.Border = Rectangle.NO_BORDER;

                Logo.AddCell(new PdfPCell(jpg){Border = 0, BorderColor = BaseColor.WHITE});
                Logo.AddCell(new PdfPCell() { Border = 0, BorderColor = BaseColor.WHITE });

                PdfPTable table = new PdfPTable(2);
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                

                PdfPCell cell = new PdfPCell();
                cell.Border = PdfPCell.NO_BORDER;
                
                cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                cell.Colspan = 2;
                cell.Border = 0;
                
                
                
                table.AddCell(cell);

                table.AddCell("Name");
                table.AddCell(": " + " " + aTeacher.TeacherName);



                table.AddCell("Employee Code");
                table.AddCell(": " + " " + aTeacher.EmployeeCode);



                table.AddCell("Date Of Birth");
                table.AddCell(": " + " " + aTeacher.BirthDay);

                table.AddCell("Gender");
                table.AddCell(": " + " " + aTeacher.Sex);

                table.AddCell("Religion");
                table.AddCell(": " + " " + aTeacher.Religion);

                table.AddCell("Blood Group");
                table.AddCell(": " + " " + aTeacher.BloodGroup);

                table.AddCell("Contact Address");
                table.AddCell(": " + " " + aTeacher.ContactAddress);

                table.AddCell("Phone Number");
                table.AddCell(": " + " " + aTeacher.PhoneNumber);

                table.AddCell("Email");
                table.AddCell(": " + " " + aTeacher.Email);


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
                Main.AddCell(new PdfPCell(table) { Border = 0, BorderColor = BaseColor.WHITE});

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


        public ActionResult HomeWork()
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null && Session["school_id2133"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int employeeId = Convert.ToInt32(Session["user_id2133"]);
                int schoolId = Convert.ToInt32(Session["school_id2133"]);

                List<HomeWorkView> aHomeWorkList = new List<HomeWorkView>();
                aHomeWorkList = aTeacherDbGateway.GetAllHomeWork(employeeId,schoolId);

                List<Class> classList = new List<Class>();
                classList = aStudentDbGateway.GetAllClass(schoolId);
                ViewBag.ClassList = classList;
                ViewBag.homeworkList = aHomeWorkList;
                return View();
            }
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HomeWork(HomeWork aHomeWork)
        {

            if (Session["user_id2133"] == null && Session["userlevel301"] == null && Session["school_id2133"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                aHomeWork.EmployeeId = Convert.ToInt32(Session["user_id2133"]);
                aHomeWork.SchoolId = Convert.ToInt32(Session["school_id2133"]);

                Teacher aTeacher = new Teacher();
                int TeacherId = aHomeWork.EmployeeId;
                aTeacher = aTeacherDbGateway.GetSpecificTeacher(TeacherId);

                aHomeWork.TeacherName = aTeacher.TeacherName;
                string Success = aTeacherDbGateway.SaveHW(aHomeWork);

                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                List<Class> classList = new List<Class>();

                classList = aStudentDbGateway.GetAllClass(schoolId);

                int employeeId = Convert.ToInt32(Session["user_id2133"]);
                List<HomeWorkView> aHomeWorkList = new List<HomeWorkView>();
                aHomeWorkList = aTeacherDbGateway.GetAllHomeWork(employeeId, schoolId);

                ViewBag.homeworkList = aHomeWorkList;
                ViewBag.ClassList = classList;
                ViewBag.SucsecAlert = Success;
                return View();
            }
        }

        public ActionResult HomeWorkDetails(int? hwid)
        {
            HomeWorkView aHomeWorkView = new HomeWorkView();
            int HwId = Convert.ToInt32(hwid);
            aHomeWorkView = aTeacherDbGateway.FindHomeWork(HwId);
            ViewBag.HomeWork = aHomeWorkView;
            return View();
        }

        public ActionResult DeleteHomeWork(int? hwid)
        {
            int HwId = Convert.ToInt32(hwid);
            string deleteSucc = aTeacherDbGateway.DeleteHomeWork(HwId);

            return RedirectToAction("HomeWork", "Teacher");
        }

     


    }
}