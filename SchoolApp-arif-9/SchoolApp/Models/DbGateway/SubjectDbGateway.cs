using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SchoolApp.Models.DbGateway
{
    public class SubjectDbGateway:Common
    {
        SqlConnectionManager aSqlConManager = new SqlConnectionManager();
        internal List<Subject> GetTheListOfSubject(int schoolId)
        {
            List<Subject> aSubjectList = new List<Subject>();
            string sqlQuery = "SELECT tblSubject.name, tblSubject.subject_id, tblSubject.class_id FROM tblSubject JOIN tblClass ON tblSubject.class_id= tblClass.class_id WHERE tblClass.school_id=" + schoolId + "";
            aSqlCommand = new SqlCommand(sqlQuery, aSqlConManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            while (aReader.Read())
            {
                Subject aSubject = new Subject();
                aSubject.ClassId = Convert.ToInt32(aReader["class_id"]);
                aSubject.SubjectId = Convert.ToInt32(aReader["subject_id"]);
                aSubject.Name = aReader["name"].ToString();
                aSubjectList.Add(aSubject);
            }
            aSqlConManager.CloseConnection();
            return aSubjectList;
        }


        internal List<Subject> GetSubjectOfClass(int? classid, int schoolId)
        {
            List<Subject> aSubjectList = new List<Subject>();
            string sqlQuery = "SELECT tblSubject.name, tblSubject.subject_id FROM tblSubject JOIN tblClass ON tblSubject.class_id = tblClass.class_id WHERE tblSubject.class_id = " +
                classid + " AND tblClass.school_id = " + schoolId + "";
            aSqlCommand = new SqlCommand(sqlQuery, aSqlConManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            while (aReader.Read())
            {
                Subject aSubject = new Subject();
                aSubject.ClassId = Convert.ToInt32(aReader["class_id"]);
                aSubject.SubjectId = Convert.ToInt32(aReader["subject_id"]);
                aSubject.Name = aReader["name"].ToString();
               
                aSubjectList.Add(aSubject);
            }
            aSqlConManager.CloseConnection();
            return aSubjectList;
        }

        internal string SaveSubject(Subject aSubject)
        {
            string sqlQuery = "INSERT INTO tblSubject VALUES('" + aSubject.Name + "', '" + aSubject.ClassId +
                             "', '" + aSubject.TeacherId + "', '" + aSubject.SchoolId + "')";
            aSqlCommand = new SqlCommand(sqlQuery, aSqlConManager.GetConnection());
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

        internal List<Subject> GetAllSubject(int schoolId)
        {
            List<Subject> SubjectList = new List<Subject>();
            string sqlQuery = "SELECT * FROM tblSubject WHERE school_id=" + schoolId + "";
            aSqlCommand = new SqlCommand(sqlQuery, aSqlConManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            while (aReader.Read())
            {
                Subject aSubject = new Subject();
                aSubject.ClassId = Convert.ToInt32(aReader["class_id"]);
                aSubject.Name = aReader["name"].ToString();
                aSubject.SubjectId = Convert.ToInt32(aReader["subject_id"]);
                SubjectList.Add(aSubject);
            }
            aSqlConManager.CloseConnection();
            return SubjectList;
        }

        internal Subject GetPostInfo(int SubjectId)
        {
            string sqlQuery = "SELECT * FROM tblSubject WHERE subject_id = " + SubjectId + "";
            aSqlCommand = new SqlCommand(sqlQuery, aSqlConManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            Subject aSubject = new Subject();
            while (aReader.Read())
            {
                aSubject.ClassId = Convert.ToInt32(aReader["class_id"]);
                aSubject.Name = aReader["name"].ToString();
                aSubject.SubjectId = Convert.ToInt32(aReader["subject_id"]);

            }
            aSqlConManager.CloseConnection();
            return aSubject;
 
        }

        internal string UpdateSubject(Subject aSubject)
        {
            string updateQuery = "UPDATE tblSubject SET name='" + aSubject.Name + "', class_id='" + aSubject.ClassId +
                                 "' WHERE subject_id=" + aSubject.SubjectId + "";
            aSqlCommand = new SqlCommand(updateQuery, aSqlConManager.GetConnection());
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