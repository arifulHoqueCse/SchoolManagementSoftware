using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SchoolApp.Models.DbGateway
{
    public class BookDbGateway : Common
    {
        private SqlConnectionManager aConnectionManager = new SqlConnectionManager();

        internal string SaveBook(Book aBook)
        {
            string sqlQuery = "INSERT INTO tblBook VALUES('" + aBook.name + "', '" + aBook.BookDescription +
                              "', '" + aBook.AuthorName + "', '" + aBook.BookStatus + "', '" + aBook.Price + "', '" +
                              aBook.Ssbn + "', '" + aBook.ClassId + "', '"+ aBook.SchoolId +"')";
            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            int ef = aSqlCommand.ExecuteNonQuery();
            if (ef > 0)
            {
                return "Book has been added";
            }
            else
            {
                return "Fail to add book";
            }
        }

        internal List<Book> GetAllBook(int schoolId)
        {
            List<Book> aBookList = new List<Book>();
            string sqlQuery = "SELECT * FROM tblBook WHERE school_id = " + schoolId + "";
            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            while (aReader.Read())
            {
                Book aBook = new Book();
                aBook.BookId = Convert.ToInt32(aReader["book_id"]);
                aBook.name = aReader["name"].ToString();
                aBook.BookDescription = aReader["book_description"].ToString();
                aBook.AuthorName = aReader["author"].ToString();
                aBook.BookStatus = Convert.ToInt32(aReader["book_status"].ToString());
                aBook.Price = aReader["price"].ToString();
                aBook.Ssbn = aReader["ssbn"].ToString();
                aBook.ClassId = Convert.ToInt32(aReader["class_id"].ToString());
              
                aBookList.Add(aBook);
            }
            aConnectionManager.CloseConnection();
            return aBookList;
        }



        internal Book GetPostInfo(int BookId)
        {
            string sqlQuery = "SELECT * FROM tblBook WHERE book_id = " + BookId + "";
            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            Book aBook = null;
            while (aReader.Read())
            {
                aBook = new Book();
                aBook.BookId = Convert.ToInt32(aReader["book_id"]);
                aBook.name = aReader["name"].ToString();
                aBook.BookDescription = aReader["book_description"].ToString();
                aBook.AuthorName = aReader["author"].ToString();
                aBook.BookStatus = Convert.ToInt32(aReader["book_status"].ToString());
                aBook.Price = aReader["price"].ToString();
                aBook.Ssbn = aReader["ssbn"].ToString();
                aBook.ClassId = Convert.ToInt32(aReader["class_id"].ToString());
            }
            aConnectionManager.CloseConnection();
            return aBook;
 
        }

        internal List<Book> GetTheStudentBookList(int userId, int schoolId)
        {
            List<Book> bookList = new List<Book>();
            string sqlQuery = "SELECT tblBook.name, tblBook.author from tblBook where class_id = (select tblStudent.class_id from tblStudent where tblStudent.school_id="+schoolId+" and tblStudent.student_id="+userId+")";
            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            while (aReader.Read())
            {
                Book aBook = new Book();
                aBook.name = aReader["name"].ToString();
                aBook.AuthorName = aReader["author"].ToString();
                bookList.Add(aBook);
            }
            return bookList;
        }

        internal string DeleteBook(int? bookId, int schoolId)
        {
            string sqlQuery = "DELETE FROM tblBook WHERE book_id=" + bookId + " AND school_id=" + schoolId + "";
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

        internal string UpdateBook(Book aBook)
        {
            string updateQuery = "UPDATE tblBook SET name='" + aBook.name + "', book_description='" + aBook.BookDescription +
                                  "', author='" + aBook.AuthorName + "', " +
                                  "book_status='" + aBook.Price + "', ssbn='" + aBook.Ssbn + "', class_id='" +
                                  aBook.ClassId + "' WHERE book_id=" + aBook.BookId + "";
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
    }
}