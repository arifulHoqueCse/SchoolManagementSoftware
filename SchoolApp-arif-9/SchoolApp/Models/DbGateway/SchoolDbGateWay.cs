using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SchoolApp.Models.View;

namespace SchoolApp.Models.DbGateway
{
    public class SchoolDbGateWay:Common
    {
        SqlConnectionManager aSqlConnManager = new SqlConnectionManager();

        internal List<SchoolList> GetAllSchoolList()
        {
           List<SchoolList> aSchoolLists = new List<SchoolList>();
           string sqlQuery = "SELECT tbl_school.school_id, tbl_school.school_name, tbl_userlogin.userid, tbl_school.school_address, tbl_userlogin.password, tbl_userlogin.userlevel, tbl_userlogin.usercode FROM tbl_school JOIN tbl_userlogin ON tbl_school.school_id = tbl_userlogin.school_id WHERE tbl_school.school_code = tbl_userlogin.usercode";
            aSqlCommand = new SqlCommand(sqlQuery, aSqlConnManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            while (aReader.Read())
            {
                SchoolList aSchoolList = new SchoolList();
                aSchoolList.SchoolName = Convert.ToString(aReader["school_name"]);
                aSchoolList.SchoolAddress = aReader["school_address"].ToString();
                aSchoolList.SchoolId = Convert.ToInt32(aReader["school_id"]);
                aSchoolList.Password = aReader["password"].ToString();
                aSchoolList.UserCode = aReader["usercode"].ToString();
                aSchoolList.Userlabel = Convert.ToInt32(aReader["userlevel"]);
                aSchoolList.UserId = Convert.ToInt32(aReader["userid"]);
                aSchoolLists.Add(aSchoolList);
            }
            aSqlConnManager.CloseConnection();
            return aSchoolLists;
        }
    }
}