using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc.Html;
using SchoolApp.Models.View;

namespace SchoolApp.Models.DbGateway
{
    public class StudentDbGateway:Common
    {
        SqlConnectionManager aConnectionManager = new SqlConnectionManager();

        internal string SaveStudent(Student aStudent, int schoolId, string schoolCode)
        {
            string sqlQuery = "INSERT INTO tblStudent VALUES('" + aStudent.StudentName + "', '" + aStudent.Birthday +
                            "', '" + aStudent.Sex + "', '" + aStudent.Religion + "', '" + aStudent.BloodGroup + "', '" +
                           aStudent.ContactAddress + "', '" + aStudent.PhoneNumber + "', '" + aStudent.Email +
                            "', '" + aStudent.Password + "', '" + aStudent.FatherName + "' , '" + aStudent.MotherName + "', '" + aStudent.RollNumber + "'" +
                              ", '" + aStudent.RegNo + "', '" + aStudent.ImagePath + "', '" + aStudent.ClassId + "', "+aStudent.SchoolId+", "+aStudent.SectionId+")";
            string findId = "SELECT MAX(student_id) AS StudentId FROM tblStudent";
            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            int ef = aSqlCommand.ExecuteNonQuery();
            if (ef > 0)
            {
                aSqlCommand.CommandText = findId;
                aReader = aSqlCommand.ExecuteReader();
                aReader.Read();
                int studentId = Convert.ToInt32(aReader["StudentId"]);
                aConnectionManager.CloseConnection();
                
                string usercode =  schoolCode+"-"+ aStudent.RegNo;
                int userLabel = 3;
                string insertLogin = "INSERT INTO tbl_userlogin VALUES('" + usercode + "','" + aStudent.Password + "', " +
                                     userLabel + ", " + studentId + ", "+schoolId+")";
                aSqlCommand = new SqlCommand(insertLogin, aConnectionManager.GetConnection());
                int efRows = aSqlCommand.ExecuteNonQuery();
                aConnectionManager.CloseConnection();
                if (efRows > 0)
                {
                    return "Student has been saved";
                }
                else
                {
                    return "Fail to save";
                }
            }
            else
            {
                return "fail";
            }
        }

        internal List<Student> GetAllStudent(int schoolId)
        {
            List<Student> aStudentList = new List<Student>();
            string sqlQuery = "SELECT * FROM tblStudent WHERE school_id = '"+schoolId+"'";
            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            while (aReader.Read())
            {
                Student aStudent = new Student();
                aStudent.StudentId = Convert.ToInt32(aReader["student_id"]);
                aStudent.StudentName = aReader["name"].ToString();
                aStudent.RegNo = aReader["reg_no"].ToString();
                aStudent.RollNumber = aReader["roll_number"].ToString();
                aStudent.FatherName = aReader["father_name"].ToString();
                aStudent.Email = aReader["email"].ToString();
                aStudent.ClassId = Convert.ToInt32(aReader["class_id"]);
                aStudent.ImagePath = aReader["image_path"].ToString();
                aStudentList.Add(aStudent);
            }
            aConnectionManager.CloseConnection();
            return aStudentList;
        }

        internal List<Class> GetAllClass(int schoolId)
        {
            List<Class> classList = new List<Class>();
            string sqlQuery = "SELECT * FROM tblClass WHERE school_id="+schoolId+"";
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

        internal Student GetSpecificStudent(int StudentId)
        {
            Student aStudent = new Student();
            string sqlQuery = "SELECT * FROM tblStudent WHERE student_id=" + StudentId + "";
            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            while (aReader.Read())
            {
                aStudent.StudentId = Convert.ToInt32(aReader["student_id"]);
                aStudent.StudentName = aReader["name"].ToString();
                aStudent.FatherName = aReader["father_name"].ToString();
                aStudent.MotherName = aReader["mother_name"].ToString();
                aStudent.RegNo = aReader["reg_no"].ToString();
                aStudent.RollNumber = aReader["roll_number"].ToString();
                aStudent.Birthday = aReader["birthday"].ToString();
                aStudent.Sex = aReader["sex"].ToString();
                aStudent.Religion = aReader["religion"].ToString();
                aStudent.BloodGroup = aReader["blood_group"].ToString();
                aStudent.ContactAddress = aReader["contact_address"].ToString();
                aStudent.PhoneNumber = aReader["phone"].ToString();
                aStudent.Email = aReader["email"].ToString();
                aStudent.Password = aReader["pasword"].ToString();
                aStudent.ImagePath = aReader["image_path"].ToString();
                aStudent.ClassId = Convert.ToInt32(aReader["class_id"]);
                aStudent.SchoolId = Convert.ToInt32(aReader["school_id"]);
                aStudent.SectionId = Convert.ToInt32(aReader["section_id"]);
            }
            aConnectionManager.CloseConnection();
            return aStudent;
        }

        internal List<Student> GetTheClassStudent(int classId, int schoolId, int sectionid)
        {
            List<Student> studentList= new List<Student>();
            string sqlQuery = "SELECT * FROM tblStudent WHERE school_id=" + schoolId + " AND class_id = " + classId + " AND section_id="+sectionid+"";
            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            while (aReader.Read())
            {
                Student aStudent = new Student();
                aStudent.StudentName = aReader["name"].ToString();
                aStudent.RegNo = aReader["reg_no"].ToString();
                aStudent.RollNumber = aReader["roll_number"].ToString();
                aStudent.StudentId = Convert.ToInt32(aReader["student_id"]);
                aStudent.ClassId = Convert.ToInt32(aReader["class_id"]);
                studentList.Add(aStudent);
            }
            return studentList;
        }
        internal List<Student> GetTheClassStudent(int classId, int schoolId)
        {
            List<Student> studentList = new List<Student>();
            string sqlQuery = "SELECT * FROM tblStudent WHERE school_id=" + schoolId + " AND class_id = " + classId + "";
            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            while (aReader.Read())
            {
                Student aStudent = new Student();
                aStudent.StudentName = aReader["name"].ToString();
                aStudent.RegNo = aReader["reg_no"].ToString();
                aStudent.RollNumber = aReader["roll_number"].ToString();
                aStudent.StudentId = Convert.ToInt32(aReader["student_id"]);
                aStudent.ClassId = Convert.ToInt32(aReader["class_id"]);
                studentList.Add(aStudent);
            }
            return studentList;
        }
        internal string UpdateStudent(Student aStudent, int schoolId)
        {
            string sqlQuery = "UPDATE tblStudent SET name = '" + aStudent.StudentName + "', birthday='" +
                              aStudent.Birthday + "', sex='" + aStudent.Birthday + "', religion='" + aStudent.Religion +
                              "', blood_group ='" + aStudent.BloodGroup + "', contact_address='" +
                              aStudent.ContactAddress + "', phone='" + aStudent.PhoneNumber + "', email = '" +
                              aStudent.Email + "', pasword='" + aStudent.Password + "', father_name = '" +
                              aStudent.FatherName + "', mother_name='" + aStudent.MotherName + "', roll_number='" +
                              aStudent.RollNumber + "', reg_no='" + aStudent.RollNumber + "', image_path='" +
                              aStudent.ImagePath + ", section_id='" +
                              aStudent.SectionId + "' WHERE class_id =" + aStudent.ClassId + " AND school_id=" +
                              aStudent.SchoolId + " AND student_id=" + aStudent.StudentId + "";
            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            int ef = aSqlCommand.ExecuteNonQuery();
            if (ef > 0)
            {
                return "Student Data Update Successfully";
            }
            else
            {
                return "Fail";
            }
        }

        internal string DeleteStudent(int? stuid, int schoolId)
        {
            string sqlQuery = "DELETE FROM tblStudent WHERE student_id=" + stuid + " AND school_id=" + schoolId + "";
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

        internal string UpdateStudentImg(Student aStudent, int schoolId)
        {
            string sqlQuery = "UPDATE tblStudent SET name = '" + aStudent.StudentName + "', birthday='" +
                              aStudent.Birthday + "', sex='" + aStudent.Birthday + "', religion='" + aStudent.Religion +
                              "', blood_group ='" + aStudent.BloodGroup + "', contact_address='" +
                              aStudent.ContactAddress + "', phone='" + aStudent.PhoneNumber + "', email = '" +
                              aStudent.Email + "', pasword='" + aStudent.Password + "', father_name = '" +
                              aStudent.FatherName + "', mother_name='" + aStudent.MotherName + "', roll_number='" +
                              aStudent.RollNumber + "', reg_no='" + aStudent.RollNumber + "' WHERE class_id =" + aStudent.ClassId + " AND school_id=" +
                              aStudent.SchoolId + " AND student_id=" + aStudent.StudentId + "";
            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            int ef = aSqlCommand.ExecuteNonQuery();
            if (ef > 0)
            {
                return "Student Data Update Successfully";
            }
            else
            {
                return "Fail";
            }
        }

        internal string UpdateStudentProfile(Student aStudent)
        {
            string sqlQuery = "UPDATE tblStudent SET blood_group ='" + aStudent.BloodGroup + "',pasword='" +
                              aStudent.Password + "', email = '" +aStudent.Email + "' WHERE class_id =" + aStudent.ClassId + " AND school_id=" +
                              aStudent.SchoolId + " AND student_id=" + aStudent.StudentId + "";
            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            int ef = aSqlCommand.ExecuteNonQuery();
            aConnectionManager.CloseConnection();
            if (ef > 0)
            {
                string uplogin = "UPDATE tbl_userlogin SET password='" + aStudent.Password + "' WHERE userid=" +
                                 aStudent.StudentId + " AND school_id=" + aStudent.SchoolId + "";
                aSqlCommand = new SqlCommand(uplogin, aConnectionManager.GetConnection());
                int eff = aSqlCommand.ExecuteNonQuery();
                aConnectionManager.CloseConnection();
                if (eff > 0)
                {
                    return "Update Data successfully";
                }
                else
                {
                    return "failed to Update Password";
                }

            }
            else
            {
                return "Failed to update data";
            }
        }



        internal List<HomeWork> GetMyHomeWork(int classId, int sectionId, int schoolId)
        {
            List<HomeWork> aHomeWorkList = new List<HomeWork>();
            string sqlQuery = "Select * from homework WHERE class_id =" + classId + " AND section_id =" + sectionId +" AND school_id=" + schoolId + "";
            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
           
            while (aReader.Read())
            {
                HomeWork aHomeWork = new HomeWork();
                aHomeWork.HwId = Convert.ToInt32(aReader["hw_id"]);
                aHomeWork.Title = aReader["title"].ToString();
                aHomeWork.Description = aReader["description"].ToString();
                aHomeWork.TeacherName = aReader["teacher_name"].ToString();
                aHomeWork.SubmissionDate = aReader["submission_date"].ToString();
                aHomeWorkList.Add(aHomeWork);

            }
            aConnectionManager.CloseConnection();
            return aHomeWorkList;
        }



        internal HomeWork FindHomeWork(int HwId)
        {
            string sqlQuery = "Select * from homework WHERE hw_id =" + HwId + "";
            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            HomeWork aHomeWork = null;
            while (aReader.Read())
            {
                aHomeWork = new HomeWork();
                aHomeWork.HwId = Convert.ToInt32(aReader["hw_id"]);
                aHomeWork.Title = aReader["title"].ToString();
                aHomeWork.Description = aReader["description"].ToString();
                aHomeWork.TeacherName = aReader["teacher_name"].ToString();
                aHomeWork.SubmissionDate = aReader["submission_date"].ToString();



            }
            aConnectionManager.CloseConnection();
            return aHomeWork;
        }
    }
}