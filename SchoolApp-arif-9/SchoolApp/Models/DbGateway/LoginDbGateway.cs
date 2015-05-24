using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SchoolApp.Models.View;

namespace SchoolApp.Models.DbGateway
{
    public class LoginDbGateway:Common
    {
        SqlConnectionManager aManager = new SqlConnectionManager();


        internal AdminLoginView AdminLogin(Admin aAdmin)
        {
            string sqlQuery = "SELECT * FROM tbl_userlogin WHERE tbl_userlogin.usercode = '"+aAdmin.UserCode+"' and tbl_userlogin.password = '"+aAdmin.Password+"'";
            aSqlCommand = new SqlCommand(sqlQuery, aManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            AdminLoginView aAdminLoginView = null;
            if (aReader.HasRows)
            {
                aReader.Read();
                aAdminLoginView = new AdminLoginView();
                aAdminLoginView.UserId = Convert.ToInt32(aReader["userid"]);
                aAdminLoginView.SchoolId = Convert.ToInt32(aReader["school_id"]);
                aAdminLoginView.UserLevel = Convert.ToInt32(aReader["userlevel"]);
            }
            aManager.CloseConnection();
            return aAdminLoginView;
        }

        internal AdminLoginView GetSchoolInformation(int schoolId)
        {
            AdminLoginView adminLogin = null;
            string sqlQuery = "SELECT tbl_school.school_id, tbl_school.school_name, tbl_school.school_address, tbl_school.school_code, tblTeacher.name as teachername, tblTeacher.teacher_id FROM tbl_school JOIN tblTeacher on tbl_school.school_id= tblTeacher.school_id where tblTeacher.school_id = " + schoolId + " and tbl_school.school_id=" + schoolId + "";
            aSqlCommand = new SqlCommand(sqlQuery, aManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            if (aReader.HasRows)
            {
                adminLogin = new AdminLoginView();
                aReader.Read();
                adminLogin.SchoolId = Convert.ToInt32(aReader["school_id"]);
                adminLogin.SchoolName = aReader["school_name"].ToString();
                adminLogin.SchoolAddress = aReader["school_address"].ToString();
                adminLogin.SchoolCode = aReader["school_code"].ToString();
            }
            aManager.CloseConnection();
            return adminLogin;
        }

        internal Admin OthersLogin(Admin aAdmin)
        {
            string sqlQuery = "SELECT * FROM tbl_userlogin WHERE usercode='" + aAdmin.UserCode + "' AND password='" +
                             aAdmin.Password + "'";
            aSqlCommand = new SqlCommand(sqlQuery, aManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            Admin aLdmin = null;
            if (aReader.HasRows)
            {
                aLdmin = new Admin();
                aReader.Read();
                aLdmin.AdminLavel = Convert.ToInt32(aReader["userlevel"]);
                aLdmin.SchoolId = Convert.ToInt32(aReader["userid"]);
            }
            aManager.CloseConnection();
            return aLdmin;
        }

        public School GetSchoolInfo(int schoolId)
        {
            string sqlQuery = "SELECT * FROM tbl_school WHERE school_id = " + schoolId + "";
            aSqlCommand = new SqlCommand(sqlQuery, aManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            School aSchool = null;
            while (aReader.Read())
            {
                aSchool = new School();
                aSchool.SchoolId = Convert.ToInt32(aReader["school_id"]);
                aSchool.SchoolName = aReader["school_name"].ToString();
                aSchool.SchoolAddress = aReader["school_address"].ToString();
                aSchool.SchoolCode = aReader["school_code"].ToString();
            }
            aManager.CloseConnection();
            return aSchool;
        }

        internal string UpdateSchoolData(School aSchool)
        {
            string sqlQuery = "UPDATE tbl_school SET school_name = '" + aSchool.SchoolName + "', school_address='" +
                              aSchool.SchoolAddress + "' WHERE school_id = " + aSchool.SchoolId + "";
            aSqlCommand = new SqlCommand(sqlQuery, aManager.GetConnection());
            int ef = aSqlCommand.ExecuteNonQuery();
            if (ef > 0)
            {
                return "School Data update successfully";
            }
            else
            {
                return "Failed to update data";
            }
        }
    }
}