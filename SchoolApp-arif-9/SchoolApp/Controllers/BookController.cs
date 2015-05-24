using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolApp.Models;
using SchoolApp.Models.DbGateway;

namespace SchoolApp.Controllers
{
    public class BookController : Controller
    {
        BookDbGateway aBookDbGateway = new BookDbGateway();
        ClassDbGateway aClassDbGateway = new ClassDbGateway();
        //
        // GET: /Book/
        public ActionResult Index()
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                List<Book> aBookList = new List<Book>();
                aBookList = GetAllBook(schoolId);

                List<Class> classList = new List<Class>();
                classList = aClassDbGateway.GetAllClass(schoolId);
                ViewBag.ClassList = classList;
                ViewBag.BookList = aBookList;
                return View();
            }
        }

        private List<Book> GetAllBook(int schoolId)
        {
            List<Book> aBookList = new List<Book>();
            aBookList = aBookDbGateway.GetAllBook(schoolId);
            return aBookList;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Book aBook)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                if (aBook != null)
                {
                    aBook.SchoolId = Convert.ToInt32(Session["school_id2133"]);
                    int schoolId = aBook.SchoolId;
                    string successalert = aBookDbGateway.SaveBook(aBook);
                    List<Book> aBookList = new List<Book>();
                    aBookList = GetAllBook(schoolId);
                    List<Class> classList = new List<Class>();
                    classList = aClassDbGateway.GetAllClass(schoolId);
                    ViewBag.ClassList = classList;
                    ViewBag.BookList = aBookList;
                    ViewBag.SuccessAlert = successalert;
                    return View();
                }

                return View();
            }
        }

        public ActionResult EditBook(int? id)
        {
            int schoolId = Convert.ToInt32(Session["school_id2133"]);
            int BookId = Convert.ToInt32(id);
            Book aBook = new Book();
            aBook = aBookDbGateway.GetPostInfo(BookId);

            List<Class> classList = new List<Class>();
            classList = aClassDbGateway.GetAllClass(schoolId);
            ViewBag.ClassList = classList;

            ViewBag.EditBookInfo = aBook;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateBookData(Book aBook)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                if (aBook != null)
                {
                    aBook.SchoolId = Convert.ToInt32(Session["school_id2133"]);
                   
                    string successalert = aBookDbGateway.UpdateBook(aBook);
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

        public ActionResult BookView()
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null && Session["school_id2133"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int userId = Convert.ToInt32(Session["user_id2133"]);
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                List<Book> aBookList = new List<Book>();
                aBookList = aBookDbGateway.GetTheStudentBookList(userId, schoolId);
                ViewBag.StudentBook = aBookList;
                return View();
            }
        }

        public ActionResult DeleteBook(int? id)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null && Session["school_id2133"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                ViewBag.bookId = id;
                return View();
            }
        }

        public ActionResult DeleteBookConfirm(int? id)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null && Session["school_id2133"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                int bookId = Convert.ToInt32(id);
                string success = aBookDbGateway.DeleteBook(bookId, schoolId);
                return RedirectToAction("Index", "Book");
            }
        }
    }
}