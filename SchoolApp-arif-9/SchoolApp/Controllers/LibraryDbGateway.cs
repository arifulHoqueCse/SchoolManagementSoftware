using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SchoolApp.Models;
using SchoolApp.Models.DbGateway;

namespace SchoolApp.Controllers
{
    public class LibraryDbGateway:Common
    {
        private SqlConnectionManager aConnectionManager = new SqlConnectionManager();

        internal List<Library> GetAllLibrary(int schoolId)
        {
            List<Library> aLibraryList = new List<Library>();
            string sqlQuery = "SELECT * FROM tblLibrary WHERE school_id=" + schoolId + "";
            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            while (aReader.Read())
            {
                Library aLibrary = new Library();
                aLibrary.Id = Convert.ToInt32(aReader["id"].ToString());
                aLibrary.StudentName = aReader["student_name"].ToString();
                aLibrary.SchoolId = Convert.ToInt32(aReader["school_id"].ToString());
                aLibrary.Class = aReader["class"].ToString();
                aLibrary.StudentReg = aReader["reg_no"].ToString();
                aLibrary.StudentRoll = aReader["roll_no"].ToString();
                aLibrary.BookName = aReader["book_name"].ToString();
                aLibrary.AuthorName = aReader["author"].ToString();
                aLibraryList.Add(aLibrary);
            }
            aConnectionManager.CloseConnection();
            return aLibraryList;
        }

        internal string SaveLibrary(Library aLibrary)
        {
            string sqlQuery = "INSERT INTO tblLibrary VALUES('" + aLibrary.StudentName + "', '" + aLibrary.SchoolId +
                             "', '" + aLibrary.Class + "', '" + aLibrary.StudentReg + "', '" + aLibrary.StudentRoll + "', '" +
                             aLibrary.BookName + "', '" + aLibrary.AuthorName + "')";
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

        internal Library GetPostInfo(int LibraryId)
        {
            Library aLibrary = null;
            string sqlQuery = "SELECT * FROM tblLibrary WHERE id = " + LibraryId + "";
            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            while (aReader.Read())
            {
                aLibrary = new Library();
                aLibrary.Id = Convert.ToInt32(aReader["id"]);
                aLibrary.StudentName = aReader["student_name"].ToString();
                aLibrary.SchoolId = Convert.ToInt32(aReader["school_id"].ToString());
                aLibrary.Class = aReader["class"].ToString();
                aLibrary.StudentReg = aReader["reg_no"].ToString();
                aLibrary.StudentRoll = aReader["roll_no"].ToString();
                aLibrary.BookName = aReader["book_name"].ToString();
                aLibrary.AuthorName = aReader["author"].ToString();
               
            }
            aConnectionManager.CloseConnection();
            return aLibrary;
        }

        internal string UpdateLibraryData(Library aLibraryUpdate)
        {
            string updateQuery = "UPDATE tblLibrary SET student_name='" + aLibraryUpdate.StudentName + "', class='" + aLibraryUpdate.Class +
                                "', reg_no='" + aLibraryUpdate.StudentReg + "', roll_no='" + aLibraryUpdate.StudentRoll + "', book_name='" + aLibraryUpdate.BookName + "', author='" +
                                aLibraryUpdate.AuthorName + "' WHERE id='" + aLibraryUpdate.Id + "'";
            aSqlCommand = new SqlCommand(updateQuery, aConnectionManager.GetConnection());
            int effectedrows = aSqlCommand.ExecuteNonQuery();
            if (effectedrows > 0)
            {
                return "Date updated successfully";
            }
            else
            {
                return "Please fill all information correctly";
            }
        }

        internal string DeleteLibraryData(int? stuid, int schoolId)
        {

            string sqlQuery = "DELETE FROM tblLibrary WHERE id=" + stuid + " AND school_id=" + schoolId + "";
            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            int ef = aSqlCommand.ExecuteNonQuery();
            if (ef > 0)
            {
                return "Delete Successfully";
            }
            else
            {
                return "Fail";
            }
       
        }
    }
}