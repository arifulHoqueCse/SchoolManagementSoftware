using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Razor.Text;
using SchoolApp.Models.View;

namespace SchoolApp.Models.DbGateway
{

    public class AttendenceDbGateway:Common
    {
        SqlConnectionManager aSqlConnectionManager = new SqlConnectionManager();
        internal bool SaveAttendence(Attendance aAttendance)
        {
            string dt = aAttendance.AttendanceDate.ToString("dd-MM-yyyy");
            string checkQuery = "SELECT count(*) as isfind from tblAttendane WHERE FORMAT(attendance_date, 'dd-MM-yyyy') = '" +dt+"' AND student_id = " + aAttendance.StudentId + " AND school_id = " + aAttendance.SchoolId + " AND class_id="+aAttendance.ClassId+" AND section_id="+aAttendance.SectionId+"";
            aSqlCommand = new SqlCommand(checkQuery, aSqlConnectionManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            aReader.Read();
            int find = Convert.ToInt32(aReader["isfind"]);
           aSqlConnectionManager.CloseConnection();
           if (find>0)
            {
                string updateQuery = "UPDATE tblAttendane SET attendance_status = " + aAttendance.AttendanceStatus +
                                     " WHERE student_id = " + aAttendance.StudentId + " AND school_id = " +
                                     aAttendance.SchoolId + " AND class_id=" + aAttendance.ClassId + " AND section_id=" + aAttendance.SectionId + "";
                aSqlCommand = new SqlCommand(updateQuery, aSqlConnectionManager.GetConnection());
                int efupdate = aSqlCommand.ExecuteNonQuery();
               aSqlConnectionManager.CloseConnection();
                if (efupdate > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
           else
           {
               string sqlQuery = "INSERT INTO tblAttendane VALUES(" + aAttendance.AttendanceStatus + ", '" + aAttendance.AttendanceDate + "', " + aAttendance.StudentId + ", " + aAttendance.SchoolId + ", " + aAttendance.TeacherId + ", '" + aAttendance.ShiftTime + "', "+aAttendance.ClassId+", "+aAttendance.SectionId+")";
               aSqlCommand = new SqlCommand(sqlQuery, aSqlConnectionManager.GetConnection());
               int ef = aSqlCommand.ExecuteNonQuery();
               aSqlConnectionManager.CloseConnection();
               if (ef > 0)
               {
                   return true;
               }
               else
               {
                   return false;
               } 
           }
            
        }

        internal List<Student> GetTheClassStudent(int? classid, int? sectionid, int schoolid)
        {
            List<Student> studentList = new List<Student>();
            string sqlQuery = "SELECT * FROM tblStudent WHERE class_id=" + classid + " AND school_id="+schoolid+" AND section_id="+sectionid+"";
            aSqlCommand = new SqlCommand(sqlQuery, aSqlConnectionManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            while (aReader.Read())
            {
                Student aStudent = new Student();
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
                aStudent.SectionId = Convert.ToInt32(aReader["section_id"]);
                studentList.Add(aStudent);
            }
            aSqlConnectionManager.CloseConnection();
            return studentList;
        }

        public int CountPresentStatus(int studentid, int studentclassid)
        {
            string sqlQuery = "select count(*) as total from tblAttendane where student_id = "+studentid+" and class_id= "+studentclassid+" and attendance_status = 1";
            aSqlCommand = new SqlCommand(sqlQuery, aSqlConnectionManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            int presentstatus = 0;
            while (aReader.Read())
            {
               presentstatus = Convert.ToInt32(aReader["total"]);
            }
            return presentstatus;
        }

        internal List<AttendenceView> GetTheStudentAttendence(int? stuid, int? classid, int schoolid, int? sectionid)
        {
            string sqlquery = "SELECT tblStudent.name, tblStudent.roll_number, tblAttendane.attendance_date, tblAttendane.attendance_status from tblStudent JOIN tblAttendane ON tblStudent.student_id = tblAttendane.student_id WHERE tblAttendane.student_id=" + stuid + " AND tblAttendane.class_id = " + classid + " AND tblAttendane.school_id = " + schoolid + " AND tblAttendane.section_id="+sectionid+"";
            aSqlCommand = new SqlCommand(sqlquery, aSqlConnectionManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
           List<AttendenceView> attendenceList = new List<AttendenceView>();
            while (aReader.Read())
            {
                AttendenceView attendenceView = new AttendenceView();
                attendenceView.StudentName = aReader["name"].ToString();
                attendenceView.StudentRoll = aReader["roll_number"].ToString();
                attendenceView.AttendenceDate = Convert.ToDateTime(aReader["attendance_date"]);
                attendenceView.AttendenceStatus = Convert.ToInt32(aReader["attendance_status"]);
                attendenceList.Add(attendenceView);
            }
            aSqlConnectionManager.CloseConnection();
            return attendenceList;
        }

        internal List<AttendenceView> GetTheStudentAttendence(int? stuid, int? classid, int schoolid)
        {
            string sqlquery = "SELECT tblStudent.name, tblStudent.roll_number, tblAttendane.attendance_date, tblAttendane.attendance_status from tblStudent JOIN tblAttendane ON tblStudent.student_id = tblAttendane.student_id WHERE tblAttendane.student_id=" + stuid + " AND tblAttendane.class_id = " + classid + " AND tblAttendane.school_id = " + schoolid + "";
            aSqlCommand = new SqlCommand(sqlquery, aSqlConnectionManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            List<AttendenceView> attendenceList = new List<AttendenceView>();
            while (aReader.Read())
            {
                AttendenceView attendenceView = new AttendenceView();
                attendenceView.StudentName = aReader["name"].ToString();
                attendenceView.StudentRoll = aReader["roll_number"].ToString();
                attendenceView.AttendenceDate = Convert.ToDateTime(aReader["attendance_date"]);
                attendenceView.AttendenceStatus = Convert.ToInt32(aReader["attendance_status"]);
                attendenceList.Add(attendenceView);
            }
            aSqlConnectionManager.CloseConnection();
            return attendenceList;
        }
    }
}