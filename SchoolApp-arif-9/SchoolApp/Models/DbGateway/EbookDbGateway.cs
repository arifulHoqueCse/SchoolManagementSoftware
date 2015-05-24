using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SchoolApp.Models.View;

namespace SchoolApp.Models.DbGateway
{
    public class EBookDbGateway : Common
    {
        SqlConnectionManager aConnectionManager = new SqlConnectionManager();

        internal string SaveEBook(EBook aEBook)
        {
            string sqlQuery = "INSERT INTO tblEBook VALUES('" + aEBook.Name + "', '" + aEBook.BookDescription +
                            "', '" + aEBook.Author + "', '" + aEBook.EbookFileName + "', '" + aEBook.ClassId + "', '" +
                           aEBook.TeacherId + "', '"+ aEBook.SchoolId +"')";

            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            int ef = aSqlCommand.ExecuteNonQuery();
            aConnectionManager.CloseConnection();
            if (ef > 0)
            {
                return "Save";
            }
            else
            {
                return "fail";
            }
        }

        internal List<EBook> GetAllEBook(int schoolId)
        {
            List<EBook> aEBookList = new List<EBook>();
            string sqlQuery = "SELECT * FROM tblEBook WHERE school_id = "+schoolId+"";
            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            while (aReader.Read())
            {
                EBook aEBook = new EBook();
                aEBook.EbookId = Convert.ToInt32(aReader["ebook_id"]);
                aEBook.Name = aReader["name"].ToString();
                aEBook.BookDescription = aReader["book_description"].ToString();
                aEBook.Author = aReader["author"].ToString();
                aEBook.EbookFileName = aReader["ebook_file_name"].ToString();
                aEBook.ClassId = Convert.ToInt32(aReader["class_id"].ToString());
                aEBook.TeacherId = Convert.ToInt32(aReader["teacher_id"].ToString());
                aEBook.SchoolId = Convert.ToInt32(aReader["school_id"].ToString());
                aEBookList.Add(aEBook);
            }
            aConnectionManager.CloseConnection();
            return aEBookList;
        }

        internal EBook GetPostInfo(int EBookId)
        {
            string sqlQuery = "SELECT * FROM tblEBook WHERE ebook_id = " + EBookId + "";
            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            EBook aEBook = new EBook();
            while (aReader.Read())
            {
                aEBook.EbookId = Convert.ToInt32(aReader["ebook_id"]);
                aEBook.Name = aReader["name"].ToString();
                aEBook.BookDescription = aReader["book_description"].ToString();
                aEBook.Author = aReader["author"].ToString();
                aEBook.EbookFileName = aReader["ebook_file_name"].ToString();
                aEBook.ClassId = Convert.ToInt32(aReader["class_id"].ToString());
                aEBook.TeacherId = Convert.ToInt32(aReader["teacher_id"].ToString());
                aEBook.SchoolId = Convert.ToInt32(aReader["school_id"].ToString());

            }
            aConnectionManager.CloseConnection();
            return aEBook;
        }

        internal string UpdateEBookData(EBookUpdate aEBookUpdate)
        {
            string updateQuery = "UPDATE tblEBook SET name='" + aEBookUpdate.Name + "', book_description='" + aEBookUpdate.BookDescription +
                                 "', author='" + aEBookUpdate.Author + "', " +
                                 "ebook_file_name='" + aEBookUpdate.EbookFileName + "', class_id='" + aEBookUpdate.ClassId + "'";
            aSqlCommand = new SqlCommand(updateQuery, aConnectionManager.GetConnection());
            int effectedrows = aSqlCommand.ExecuteNonQuery();
            if (effectedrows > 0)
            {
                return "Data updated successfully";
            }
            else
            {
                return "Please fill all information correctly";
            }
        }

        internal List<EBook> GetAllEBookTheSchool(int schoolId)
        {
            List<EBook> aEBookList = new List<EBook>();
            string sqlQuery = "SELECT * FROM tblEBook WHERE school_id = " + schoolId + "";
            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            while (aReader.Read())
            {
                EBook aEBook = new EBook();
                aEBook.EbookId = Convert.ToInt32(aReader["ebook_id"]);
                aEBook.Name = aReader["name"].ToString();
                aEBook.Author = aReader["author"].ToString();
                aEBook.EbookFileName = aReader["ebook_file_name"].ToString();
                aEBookList.Add(aEBook);
            }
            return aEBookList;
        }

        internal string DeleteEbook(int ebookId)
        {
            string sqlQuery = "DELETE FROM tblEBook WHERE ebook_id=" + ebookId + "";
            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            int ef = aSqlCommand.ExecuteNonQuery();
            if (ef > 0)
            {
                return "Book has been deleted";
            }
            else
            {
                return "Fail";
            }
        }
    }
}