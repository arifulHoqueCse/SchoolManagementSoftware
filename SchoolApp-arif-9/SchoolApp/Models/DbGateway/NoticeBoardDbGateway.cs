using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SchoolApp.Models.DbGateway
{
    public class NoticeBoardDbGateway : Common
    {
        private SqlConnectionManager aConnectionManager = new SqlConnectionManager();
        internal string SaveNoticeBoard(NoticeBoard aNoticeBoard)
        {
            string sqlQuery = "INSERT INTO tblNoticeBoard VALUES('" + aNoticeBoard.NoticeTitle + "', '" + aNoticeBoard.NoticeDescription +
                              "', '"+aNoticeBoard.NoticeTimeStamp+"', "+aNoticeBoard.SchoolId+")";

            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            int ef = aSqlCommand.ExecuteNonQuery();
            if (ef > 0)
            {
                return "Save";
            }
            else
            {
                return "fail";
            }
        }

        internal List<NoticeBoard> GetAllNotice(int schoolId)
        {
            List<NoticeBoard> noticeBoardlist = new List<NoticeBoard>();
            string sqlQuery = "SELECT * FROM tblNoticeBoard WHERE school_id = " + schoolId + "";
            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            while (aReader.Read())
            {
                NoticeBoard aNoticeBoard = new NoticeBoard();
                aNoticeBoard.NoticeId = Convert.ToInt32(aReader["notice_id"]);
                aNoticeBoard.NoticeTitle = aReader["notice_title"].ToString();
                aNoticeBoard.NoticeDescription = aReader["notice_description"].ToString();
                aNoticeBoard.NoticeTimeStamp = aReader["create_timestamp"].ToString();
                noticeBoardlist.Add(aNoticeBoard);
            }
            aConnectionManager.CloseConnection();
            return noticeBoardlist;
        }

        internal NoticeBoard GetSpecifiqNotice(int noticeId, int schooId)
        {
            string sqlQuery = "SELECT * FROM tblNoticeBoard WHERE notice_id=" + noticeId + " AND school_id=" + schooId +"";
            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            NoticeBoard aNoticeBoard = null;
            while (aReader.Read())
            {
                aNoticeBoard = new NoticeBoard();
                aNoticeBoard.NoticeId = Convert.ToInt32(aReader["notice_id"]);
                aNoticeBoard.NoticeTitle = aReader["notice_title"].ToString();
                aNoticeBoard.NoticeDescription = aReader["notice_description"].ToString();
                aNoticeBoard.NoticeTimeStamp = aReader["create_timestamp"].ToString();
            }
            return aNoticeBoard;
        }

        internal string DeleteSpecificNotice(int? ntid, int schoolId)
        {
            string sqlQuery = "DELETE FROM tblNoticeBoard WHERE notice_id=" + ntid + " AND school_id=" + schoolId + "";
            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            int ef = aSqlCommand.ExecuteNonQuery();
            if (ef > 0)
            {
                return "sucess";
            }
            else
            {
                return "fail";
            }
        }
    }
}