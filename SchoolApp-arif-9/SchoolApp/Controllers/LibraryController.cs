using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolApp.Models;

namespace SchoolApp.Controllers
{
    public class LibraryController : Controller
    {
        LibraryDbGateway aLibraryDbGateway = new LibraryDbGateway();
        //
        // GET: /Library/
        public ActionResult Library()
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                List<Library> aLibrary = new List<Library>();
                aLibrary = aLibraryDbGateway.GetAllLibrary(schoolId);
                ViewBag.LibraryList = aLibrary;
                return View();
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Library(Library aLibrary)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                if (aLibrary != null)
                {
                    aLibrary.SchoolId = Convert.ToInt32(Session["school_id2133"]);
                    string successalert = aLibraryDbGateway.SaveLibrary(aLibrary);

                    int schoolId = Convert.ToInt32(Session["school_id2133"]);
                    List<Library> aLibraryList = new List<Library>();
                    aLibraryList = aLibraryDbGateway.GetAllLibrary(schoolId);
                    ViewBag.LibraryList = aLibraryList;
                    return View();
                }
                return View();
            }
        }

        public ActionResult EditLibrary(int? id)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int LibraryId = Convert.ToInt32(id);
                Library aLibrary = new Library();
                aLibrary = aLibraryDbGateway.GetPostInfo(LibraryId);
                ViewBag.EditLibraryInfo = aLibrary;
                return View();
            }
        }


        [HttpPost]
        public ActionResult UpdateLibraryData(Library aLibraryUpdate)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                if (aLibraryUpdate != null)
                {

                    string successalert = aLibraryDbGateway.UpdateLibraryData(aLibraryUpdate);
                    ViewBag.UpdateData = successalert;
                    return View();
                }
                return View();
            }
        }

        public ActionResult DeleteLibraryData(int? stuid)
        {
            ViewBag.LibraryData = Convert.ToInt32(stuid);
            return View();
        }

        public ActionResult DeleteLibraryDataConfirm(int? bookid)
        {
            int schoolId = Convert.ToInt32(Session["school_id2133"]);
            int stuid = Convert.ToInt32(bookid);
            string deletesuccess = aLibraryDbGateway.DeleteLibraryData(stuid, schoolId);
            return RedirectToAction("Library", "Library");
        }

	}
}