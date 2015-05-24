using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web;
using SchoolApp.Models.View;

namespace SchoolApp.Models.DbGateway
{
    public class TeacherDbGateway:Common
    {
        SqlConnectionManager aConnectionManager  = new SqlConnectionManager();

        internal string SaveTeacher(Teacher aTeacher, string schoolCode)
        {
            string sqlQuery = "INSERT INTO tblTeacher VALUES('" + aTeacher.TeacherName + "', '" + aTeacher.BirthDay +
                              "', '" + aTeacher.Sex + "', '" + aTeacher.Religion + "', '" + aTeacher.BloodGroup + "', '" +
                              aTeacher.ContactAddress + "', '" + aTeacher.PhoneNumber + "', '" + aTeacher.ImagePath +
                              "', '" + aTeacher.Email + "', '" + aTeacher.EmployeeCode + "', '" + aTeacher.Password + "', '" + aTeacher.SchoolId + "')";

            string findId = "SELECT MAX(teacher_id) AS TeacherId FROM tblTeacher";
            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            int ef = aSqlCommand.ExecuteNonQuery();
            if (ef > 0)
            {
                aSqlCommand.CommandText = findId;
                SqlDataReader aReader = aSqlCommand.ExecuteReader();
                aReader.Read();
                int teacherId = Convert.ToInt32(aReader["TeacherId"]);
                aConnectionManager.CloseConnection();
                string usercode = schoolCode+"-"+aTeacher.EmployeeCode;
                int userLabel = 2;
                string insertLogin = "INSERT INTO tbl_userlogin VALUES('" + usercode + "','" + aTeacher.Password + "', " +
                                     userLabel + ", " + teacherId + ","+aTeacher.SchoolId+")";
                aSqlCommand = new SqlCommand(insertLogin, aConnectionManager.GetConnection());
                int efRows = aSqlCommand.ExecuteNonQuery();
                aConnectionManager.CloseConnection();
                if (efRows > 0)
                {
                    return "Teacher has been added";
                }
                else
                {
                    return "Fail";
                }
            }
            else
            {
                return "fail";
            }
        }

        internal bool FindDuplicateEmail(string email)
        {
            string sqlQuery = "SELECT * FROM tblTeacher WHERE email = '" + email + "'";
            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            bool findMail = aReader.HasRows;
            if (findMail)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal List<Teacher> GetAllTeacher(int schoolId)
        {
            List<Teacher> aTeacherList = new List<Teacher>();
            string sqlQuery = "SELECT * FROM tblTeacher WHERE school_id="+schoolId+"";
            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            while (aReader.Read())
            {
                Teacher aTeacher = new Teacher();
                aTeacher.TeacherId = Convert.ToInt32(aReader["teacher_id"]);
                aTeacher.TeacherName = aReader["name"].ToString();
                aTeacher.Email = aReader["email"].ToString();
                aTeacher.ImagePath = aReader["image_path"].ToString();
                aTeacherList.Add(aTeacher);
            }
            aConnectionManager.CloseConnection();
            return aTeacherList;
        }

        internal Teacher GetPostInfo(int teacherid)
        {
            string sqlQuery = "SELECT * FROM tblTeacher WHERE teacher_id = " + teacherid + "";
            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            Teacher aTeacher = new Teacher();
            while (aReader.Read())
            {
                aTeacher.TeacherId = Convert.ToInt32(aReader["teacher_id"]);
                aTeacher.TeacherName = aReader["name"].ToString();
                aTeacher.Email = aReader["email"].ToString();
                aTeacher.BirthDay = aReader["birthday"].ToString();
                aTeacher.Sex = aReader["sex"].ToString();
                aTeacher.Religion = aReader["religion"].ToString();
                aTeacher.BloodGroup = aReader["blood_group"].ToString();
                aTeacher.ContactAddress = aReader["contact_address"].ToString();
                aTeacher.PhoneNumber = aReader["phone"].ToString();
                aTeacher.Email = aReader["email"].ToString();
                aTeacher.ImagePath = aReader["image_path"].ToString();
                aTeacher.Password = aReader["pasword"].ToString();

            }
            aConnectionManager.CloseConnection();
            return aTeacher;
        }

        internal string UpdateTeacherData(TeacherUpdate aUpdate)
        {
            string updateQuery = "UPDATE tblTeacher SET name='" + aUpdate.TeacherName + "', email='" + aUpdate.Email +
                                 "', birthday='" + aUpdate.BirthDay + "', " +
                                 "sex='" + aUpdate.Sex + "', religion='" + aUpdate.Religion + "', blood_group='" +
                                 aUpdate.BloodGroup + "',contact_address='" + aUpdate.ContactAddress + "'," +
                                 "phone='" + aUpdate.PhoneNumber + "', image_path='" +
                                 aUpdate.ImagePath + "',pasword='" + aUpdate.Password + "' WHERE teacher_id="+aUpdate.TeacherId+"";
            aSqlCommand = new SqlCommand(updateQuery, aConnectionManager.GetConnection());
            int effectedrows = aSqlCommand.ExecuteNonQuery();
            if (effectedrows > 0)
            {
                return "Routine Has Been Updated";
            }
            else
            {
                return "Please fill all information correctly";
            }
        }

        internal Teacher GetSpecificTeacher(int TeacherId)
        {
            string sqlQuery = "SELECT * FROM tblTeacher WHERE teacher_id = " + TeacherId + "";
            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            Teacher aTeacher = new Teacher();
            while (aReader.Read())
            {
                aTeacher.TeacherId = Convert.ToInt32(aReader["teacher_id"]);
                aTeacher.TeacherName = aReader["name"].ToString();
                aTeacher.Email = aReader["email"].ToString();
                aTeacher.BirthDay = aReader["birthday"].ToString();
                aTeacher.Sex = aReader["sex"].ToString();
                aTeacher.Religion = aReader["religion"].ToString();
                aTeacher.BloodGroup = aReader["blood_group"].ToString();
                aTeacher.ContactAddress = aReader["contact_address"].ToString();
                aTeacher.PhoneNumber = aReader["phone"].ToString();
                aTeacher.Email = aReader["email"].ToString();
                aTeacher.ImagePath = aReader["image_path"].ToString();
                aTeacher.Password = aReader["pasword"].ToString();
                aTeacher.EmployeeCode = aReader["employee_code"].ToString();

            }
            aConnectionManager.CloseConnection();
            return aTeacher;
        }

        internal List<Teacher> GetAllTeacherList(int schoolId)
        {
            List<Teacher> aTeacherList = new List<Teacher>();
            string sqlQuery = "SELECT * FROM tblTeacher WHERE school_id = "+schoolId+"";
            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            while (aReader.Read())
            {
                Teacher aTeacher = new Teacher();
                aTeacher.TeacherId = Convert.ToInt32(aReader["teacher_id"]);
                aTeacher.TeacherName = aReader["name"].ToString();
                aTeacher.Email = aReader["email"].ToString();
                aTeacher.ImagePath = aReader["image_path"].ToString();
                aTeacher.EmployeeCode = aReader["employee_code"].ToString();
                aTeacherList.Add(aTeacher);
            }
            aConnectionManager.CloseConnection();
            return aTeacherList;
        }

        internal string UpdateTeacherDataImg(TeacherUpdate aUpdate)
        {
            string updateQuery = "UPDATE tblTeacher SET name='" + aUpdate.TeacherName + "', email='" + aUpdate.Email +
                                 "', birthday='" + aUpdate.BirthDay + "', " +
                                 "sex='" + aUpdate.Sex + "', religion='" + aUpdate.Religion + "', blood_group='" +
                                 aUpdate.BloodGroup + "',contact_address='" + aUpdate.ContactAddress + "'," +
                                 "phone='" + aUpdate.PhoneNumber + "', pasword='" + aUpdate.Password + "' WHERE teacher_id=" + aUpdate.TeacherId + "";
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

        internal string DeleteTeacher(int schoolId, int teacherId)
        {
            string sqlQuery = "DELETE FROM tblTeacher WHERE teacher_id=" + teacherId + " AND school_id=" + schoolId + "";
            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            int ef = aSqlCommand.ExecuteNonQuery();
            if (ef > 0)
            {
                return "Success";
            }
            else
            {
                return "Fail";
            }
        }

        internal List<Class> GetAllClass(int schoolId)
        {
            List<Class> classList = new List<Class>();
            string sqlQuery = "SELECT * FROM tblClass WHERE school_id=" + schoolId + "";
            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            while (aReader.Read())
            {
                Class aClass = new Class();
                aClass.ClassId = Convert.ToInt32(aReader["class_id"]);
                aClass.Name = aReader["name"].ToString();
                classList.Add(aClass);
            }
            aConnectionManager.CloseConnection();
            return classList;
        }


        internal string SaveHW(HomeWork aHomeWork)
        {
            string sqlQuery = "INSERT INTO homework VALUES('" + aHomeWork.Title + "', '" + aHomeWork.Description +
                              "', '" + aHomeWork.TeacherName + "', '" + aHomeWork.ClassId + "','" + aHomeWork.SectionId + "','" + aHomeWork.SubmissionDate + "','" + aHomeWork.SchoolId + "','" + aHomeWork.EmployeeId + "')";

            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            int ef = aSqlCommand.ExecuteNonQuery();
            if (ef > 0)
            {
                return "Home work has been saved";
            }
            else
            {
                return "fail";
            }
        }

        internal List<HomeWorkView> GetAllHomeWork(int employeeId, int schoolId)
        {
            List<HomeWorkView> aHomeWorkList = new List<HomeWorkView>();
            string sqlQuery = "Select tblClass.name, tbl_section.section_name, homework.hw_id, homework.title, homework.description, homework.teacher_name, homework.submission_date, homework.class_id from homework join tblClass on homework.class_id = tblClass.class_id JOIN tbl_section on homework.section_id = tbl_section.section_id WHERE homework.employee_id = " + employeeId + " AND homework.school_id = " + schoolId + "";
            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            while (aReader.Read())
            {
                HomeWorkView aHomeWork = new HomeWorkView();
                aHomeWork.HwId = Convert.ToInt32(aReader["hw_id"]);
                aHomeWork.Title = aReader["title"].ToString();
                aHomeWork.Description = aReader["description"].ToString();

                aHomeWork.ClassName = aReader["name"].ToString();
                aHomeWork.SectionName = aReader["section_name"].ToString();
                aHomeWork.SubmissionDate = aReader["submission_date"].ToString();


                aHomeWorkList.Add(aHomeWork);
            }
            aConnectionManager.CloseConnection();
            return aHomeWorkList;
        }

        internal HomeWorkView FindHomeWork(int HwId)
        {
            
            string sqlQuery = "Select tblClass.name, tbl_section.section_name, homework.hw_id, homework.title, homework.description, homework.teacher_name, homework.submission_date, homework.class_id from homework join tblClass on homework.class_id = tblClass.class_id JOIN tbl_section on homework.section_id = tbl_section.section_id WHERE homework.hw_id = " + HwId + "";
            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            HomeWorkView aHomeWork = null;
            while (aReader.Read())
            {
                aHomeWork = new HomeWorkView();
                aHomeWork.HwId = Convert.ToInt32(aReader["hw_id"]);
                aHomeWork.Title = aReader["title"].ToString();
                aHomeWork.Description = aReader["description"].ToString();
                aHomeWork.TeacherName = aReader["teacher_name"].ToString();

                aHomeWork.ClassName = aReader["name"].ToString();
                aHomeWork.SectionName = aReader["section_name"].ToString();
                aHomeWork.SubmissionDate = aReader["submission_date"].ToString();


               
            }
            aConnectionManager.CloseConnection();
            return aHomeWork;
        }


        internal string DeleteHomeWork(int HwId)
        {
            string sqlQuery = "DELETE FROM homework WHERE hw_id=" + HwId + "";
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