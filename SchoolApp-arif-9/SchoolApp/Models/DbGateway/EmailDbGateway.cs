using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SchoolApp.Models.DbGateway
{
    public class EmailDbGateway : Common
    {
        private SqlConnectionManager aConnectionManager = new SqlConnectionManager();

        internal string SaveEmail(Email aEmail, int schoolid)
        {
            string sqlQuery = "INSERT INTO tblEmail VALUES('" + aEmail.Subject + "', '" + aEmail.Message +
                              "', '" + aEmail.EmployeeCode + "', '" + aEmail.StudentReg + "', '" + aEmail.EmployeeIdentity + "', '" + aEmail.StudentIdentity + "', " + schoolid + ", '" + aEmail.Date + "')";
            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            int ef = aSqlCommand.ExecuteNonQuery();
            if (ef > 0)
            {
                return "Email Has Been Send";
            }
            else
            {
                return "failed to Send";
            }
        }

        //internal List<Email> GetEmailList(int StudentReg)
        //{
        //    List<Email> aEmailList = new List<Email>();
        //    string sqlQuery = "SELECT * FROM tblEmail WHERE student_registration=" + StudentReg + "";
        //    aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
        //    aReader = aSqlCommand.ExecuteReader();
        //    while (aReader.Read())
        //    {
        //        Email aEmail = new Email();
        //       // aEmail.StudentId = Convert.ToInt32(aReader["student_id"]);
        //        aEmail.Subject = aReader["sub"].ToString();
        //        aEmail.Message = aReader["message"].ToString();
        //        aEmailList.Add(aEmail);
        //    }
        //    aConnectionManager.CloseConnection();
        //    return aEmailList;
        //}

        internal List<Email> GetTeacherEmailList(string EmployeeCode, int schoolId)
        {
            List<Email> aEmailList = new List<Email>();
            string sqlQuery = "SELECT * FROM tblEmail WHERE employee_code='" + EmployeeCode + "' AND school_id ="+schoolId+"";
            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            while (aReader.Read())
            {
                Email aEmail = new Email();
                aEmail.EmailId = Convert.ToInt32(aReader["email_id"]);
               
                aEmail.StudentIdentity = aReader["student_identity"].ToString();
                aEmail.Subject = aReader["sub"].ToString();
                aEmail.Message = aReader["message"].ToString();
                aEmail.Date = Convert.ToDateTime(aReader["date"]);
                aEmailList.Add(aEmail);
            }
            aConnectionManager.CloseConnection();
            return aEmailList;
        }

        internal Email GetSpecificEmail(int EmailId)
        {
            Email aEmail = new Email();
            string sqlQuery = "SELECT * FROM tblEmail WHERE email_id=" + EmailId + "";
            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            while (aReader.Read())
            {
               
                aEmail.EmailId = Convert.ToInt32(aReader["email_id"]);
                aEmail.StudentIdentity = aReader["student_identity"].ToString();
                aEmail.EmployeeIdentity = aReader["employee_identity"].ToString();
                aEmail.Subject = aReader["sub"].ToString();
                aEmail.Message = aReader["message"].ToString();
                aEmail.StudentReg = aReader["student_registration"].ToString();
                aEmail.Date = Convert.ToDateTime(aReader["date"]);
                
            }
            aConnectionManager.CloseConnection();
            return aEmail;
        }



        internal List<Email> GetStudentEmailList(string StudentReg, int schoolId)
        {
            List<Email> aEmailList = new List<Email>();
            string sqlQuery = "SELECT * FROM tblEmail WHERE student_registration='" + StudentReg + "' AND school_id = "+schoolId+"";
            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            while (aReader.Read())
            {
                Email aEmail = new Email();
                aEmail.EmailId = Convert.ToInt32(aReader["email_id"]);

                aEmail.EmployeeIdentity = aReader["employee_identity"].ToString();
                aEmail.Subject = aReader["sub"].ToString();
                aEmail.Message = aReader["message"].ToString();
                aEmail.Date = Convert.ToDateTime(aReader["date"]);
                aEmailList.Add(aEmail);
            }
            aConnectionManager.CloseConnection();
            return aEmailList;
        }



        internal string DeleteEmail(int EmailId)
        {
            string sqlQuery = "DELETE FROM tblEmail WHERE email_id=" + EmailId + "";
            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            int ef = aSqlCommand.ExecuteNonQuery();
            if (ef > 0)
            {
                return "Email has been deleted";
            }
            else
            {
                return "Fail to delete Email";
            }
        }
    }
}