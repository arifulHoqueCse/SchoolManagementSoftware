using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolApp.Models;
using SchoolApp.Models.DbGateway;

namespace SchoolApp.Controllers
{
    public class SectionController : Controller
    {
        //
        // GET: /Section/
        SectionDbGateway aSectionDbGateway = new SectionDbGateway();
        public ActionResult Section()
        {
            int schoolid = Convert.ToInt32(Session["school_id2133"]);
            List<Section> sectionList = new List<Section>();
            sectionList = GetSectionList(schoolid);
            ViewBag.SectionList = sectionList;
            return View();
        }

        private List<Section> GetSectionList(int schoolid)
        {
            List<Section> sectionList = aSectionDbGateway.GetAllSection(schoolid);
            return sectionList;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Section(Section aSection)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                aSection.SchoolId = Convert.ToInt32(Session["school_id2133"]);
                List<Section> sectionList = new List<Section>();
                sectionList = GetSectionList(aSection.SchoolId);
                ViewBag.SectionList = sectionList;
                string sectionSave = aSectionDbGateway.SaveSection(aSection);
                ViewBag.SaveSection = sectionSave;
                return View();
            }
        }

        public ActionResult EditSection(int? secid)
        {
         
            int SectionId = Convert.ToInt32(secid);
            Section aSection = new Section();
            aSection = aSectionDbGateway.GetSpecificSection(SectionId);
            ViewBag.EditSectionInfo = aSection;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateSection(Section aSection)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                if (aSection != null)
                {
                  string  sucessAlert = aSectionDbGateway.UpdateSection(aSection);
                  ViewBag.SuccessAlert = sucessAlert;
                  return View();
                }

                return View();
            }
        }

        public JsonResult GetTheSection(int classid, int schoolid)
        {
            SectionDbGateway aSectionDbGateway = new SectionDbGateway();
            int schoolId = Convert.ToInt32(schoolid);
            int classId = Convert.ToInt32(classid);
            List<Section> sectionList = new List<Section>();
            sectionList = aSectionDbGateway.GetTheSection(classid, schoolid);
            if (sectionList != null)
            {
                return Json(sectionList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }
	}
}