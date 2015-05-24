using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolApp.Models;
using SchoolApp.Models.DbGateway;
using SchoolApp.Models.View;

namespace SchoolApp.Controllers
{
    public class EBookController : Controller
    {
        EBookDbGateway aEBookDbGateway = new EBookDbGateway();
        StudentDbGateway aStudentDbGateway = new StudentDbGateway();
        //
        // GET: /EBook/

        public ActionResult Ebook()
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int schoolId = Convert.ToInt32(Session["school_id2133"]);

                List<EBook> aEBookList = new List<EBook>();
                aEBookList = aEBookDbGateway.GetAllEBook(schoolId);

                List<Class> classList = new List<Class>();
                classList = aStudentDbGateway.GetAllClass(schoolId);
                ViewBag.ClassList = classList;
                ViewBag.EBookList = aEBookList;
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ebook(EBook aEBook, HttpPostedFileBase file)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                aEBook.TeacherId = Convert.ToInt32(Session["userlevel301"]);
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                List<EBook> aEBookList = new List<EBook>();
                aEBookList = aEBookDbGateway.GetAllEBook(schoolId);
                
                
                var fileName = Path.GetFileName(file.FileName);
                var MyFile = fileName + aEBook.TeacherId + aEBook.Name;
                var imagePath = Path.Combine(Server.MapPath("/ebook"), MyFile);
                file.SaveAs(imagePath);
                aEBook.EbookFileName = imagePath;
                aEBook.SchoolId = Convert.ToInt32(Session["school_id2133"]);
                string successalert = aEBookDbGateway.SaveEBook(aEBook);

                List<Class> classList = new List<Class>();
                classList = aStudentDbGateway.GetAllClass(schoolId);

                ViewBag.ClassList = classList;
                ViewBag.EBookList = aEBookList;
                return View();
            }
        }

        public ActionResult EditEBook(int? id)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                int EBookId = Convert.ToInt32(id);
                EBook aEBook = new EBook();
                aEBook = aEBookDbGateway.GetPostInfo(EBookId);
                List<Class> classList = new List<Class>();
                classList = aStudentDbGateway.GetAllClass(schoolId);

                ViewBag.ClassList = classList;
                ViewBag.EditEBookInfo = aEBook;
                return View();
            }
        }

        [HttpPost]
        public ActionResult UpdateEBookData(EBookUpdate aEBookUpdate, HttpPostedFileBase file)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                if (aEBookUpdate != null)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var MyFile = fileName + aEBookUpdate.TeacherId + aEBookUpdate.Name;
                    var imagePath = Path.Combine(Server.MapPath("/ebook"), MyFile);
                    file.SaveAs(imagePath);
                    aEBookUpdate.EbookFileName = fileName;
                    string successalert = aEBookDbGateway.UpdateEBookData(aEBookUpdate);
                    ViewBag.UpdateData = successalert;
                    return View();
                }
                return View();
            }
        }

        public ActionResult EbookView()
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null && Session["school_id2133"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int userId = Convert.ToInt32(Session["user_id2133"]);
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                List<EBook> aEBookList = new List<EBook>();
                aEBookList = aEBookDbGateway.GetAllEBookTheSchool(schoolId);
                ViewBag.EbookList = aEBookList;
                return View();
            }
        }

        public ActionResult DeleteEbook(int? id)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null && Session["school_id2133"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                int ebookId = Convert.ToInt32(id);
                string success = aEBookDbGateway.DeleteEbook(ebookId);
                ViewBag.Success = success;
                return View();
            }
        }

        public FileResult BookDownload(string book)
        {
            
            string contentType = "application/pdf";
            return File(book, contentType, "Myschool.pdf");
        }

	}
}