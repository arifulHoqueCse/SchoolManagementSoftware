using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolApp.Models;
using SchoolApp.Models.DbGateway;

namespace SchoolApp.Controllers
{
    public class NoticeController : Controller
    {
        NoticeBoardDbGateway aNoticeBoardDbGateway = new NoticeBoardDbGateway();
        //
        // GET: /Notice/
        public ActionResult Notice()
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                List<NoticeBoard> noticeBoardList = new List<NoticeBoard>();
                noticeBoardList = GetAllNotice(schoolId);
                ViewBag.NoticeList = noticeBoardList;
                return View();
            }
        }

        private List<NoticeBoard> GetAllNotice(int schoolId)
        {
            List<NoticeBoard> aNoticeBoardList = aNoticeBoardDbGateway.GetAllNotice(schoolId);
            return aNoticeBoardList;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Notice(NoticeBoard aNoticeBoard)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                DateTime dt = DateTime.Now; // Or whatever
                string createTime = dt.ToString("dd-MMM-yyyy HH:mm:ss");
                aNoticeBoard.NoticeTimeStamp = createTime;
                aNoticeBoard.SchoolId = 1;
                if (aNoticeBoard != null)
                {
                    string successalert = aNoticeBoardDbGateway.SaveNoticeBoard(aNoticeBoard);
                    ViewBag.SuccessAlert = "Notice Added Succesfully";
                }
                List<NoticeBoard> noticeBoardList = new List<NoticeBoard>();
                noticeBoardList = GetAllNotice(schoolId);
                ViewBag.NoticeList = noticeBoardList;
                return View();
            }
        }

        public ActionResult NoticeView()
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null && Session["school_id2133"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int userId = Convert.ToInt32(Session["user_id2133"]);
                int schoolId = Convert.ToInt32(Session["school_id2133"]);
                List<NoticeBoard> noticeBoardList = new List<NoticeBoard>();
                noticeBoardList = GetAllNotice(schoolId);
                ViewBag.NoticeList = noticeBoardList;
                return View();
            }
        }

        public ActionResult NoticeDetailsView(int? ntid)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null && Session["school_id2133"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                int noticeId = Convert.ToInt32(ntid);
                int schooId = Convert.ToInt32(Session["school_id2133"]);
                NoticeBoard aNoticeBoard = new NoticeBoard();
                aNoticeBoard = aNoticeBoardDbGateway.GetSpecifiqNotice(noticeId, schooId);
                ViewBag.NoticeDetails = aNoticeBoard;
                return View();
            }
        }

        public ActionResult DeleteNotice(int? ntid)
        {
            if (Session["user_id2133"] == null && Session["userlevel301"] == null && Session["school_id2133"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                ViewBag.NoticeId = Convert.ToInt32(ntid);
                return View();
               
            }
        }

        public ActionResult DeleteNoticeConfirm(int? nid)
        {
            int schoolId = Convert.ToInt32(Session["school_id2133"]);
            string deleteSucc = aNoticeBoardDbGateway.DeleteSpecificNotice(nid, schoolId);
            return RedirectToAction("Notice", "Notice");
        }
	}
}