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
    public class ClassController : Controller
    {
        ClassDbGateway aDbGateway = new ClassDbGateway();
        SectionDbGateway aSectionDbGateway = new SectionDbGateway();
        //
        // GET: /Class/
        public ActionResult ClassInformation()
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                List<ClassListView> aClasseList = new List<ClassListView>();
                List<Section> sectionList = new List<Section>();
                sectionList = GetSectionList(schoolId);
                aClasseList = GetAllClassList();
                ViewBag.SectionList = sectionList;
                ViewBag.ClassList = aClasseList;
                return View();
            }
        }
        private List<Section> GetSectionList(int schoolid)
        {
            List<Section> sectionList = aSectionDbGateway.GetAllSection(schoolid);
            return sectionList;
        }

        private List<ClassListView> GetAllClassList()
        {
            int schoolId = Convert.ToInt32(Session["school_id2133"]);
            List<ClassListView> aListViews = aDbGateway.GetAllClassList(schoolId);

            TeacherDbGateway aTeacherDbGateway = new TeacherDbGateway();
            List<Teacher> aTeacherList = new List<Teacher>();
            aTeacherList = aTeacherDbGateway.GetAllTeacherList(schoolId);
            ViewBag.TeacherList = aTeacherList;
            return aListViews;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ClassInformation(Class aClass)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                aClass.SchoolId = Convert.ToInt32(Session["school_id2133"]);
                string saveClass = aDbGateway.AddNewClass(aClass);
                List<Section> sectionList = new List<Section>();
                sectionList = GetSectionList(aClass.SchoolId);
                List<ClassListView> aClasseList = new List<ClassListView>();
                aClasseList = GetAllClassList();
                TeacherDbGateway aTeacherDbGateway = new TeacherDbGateway();
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                List<Teacher> aTeacherList = new List<Teacher>();
                aTeacherList = aTeacherDbGateway.GetAllTeacherList(schoolId);
                ViewBag.SectionList = sectionList;
                ViewBag.TeacherList = aTeacherList;
                ViewBag.ClassList = aClasseList;
                ViewBag.SaveClass = saveClass;
                return View();
            }
        }
	}
}