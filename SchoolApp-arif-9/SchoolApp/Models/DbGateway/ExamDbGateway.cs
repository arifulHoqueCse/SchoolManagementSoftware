using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SchoolApp.Models.View;

namespace SchoolApp.Models.DbGateway
{
    public class ExamDbGateway:Common
    {
        SqlConnectionManager aConnectionManager = new SqlConnectionManager();
        internal string SaveExam(Exam aExam)
        {
            string sqlQuery = "INSERT INTO tblExam VALUES('" + aExam.Name + "', '" + aExam.ExamDate +
                            "', '" + aExam.Comment + "', '" + aExam.SchoolId + "')";

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

        internal List<Exam> GetAllExam(int schoolId)
            {
                List<Exam> aExamList = new List<Exam>();
                string sqlQuery = "SELECT * FROM tblExam WHERE school_id = "+schoolId+"";
            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            while (aReader.Read())
            {
                Exam aExam = new Exam();
                aExam.ExamId = Convert.ToInt32(aReader["exam_id"]);
                aExam.Name = aReader["name"].ToString();
                aExam.ExamDate = String.Format("{0:d-MMM-yyyy}", aReader["exam_date"]);
                aExam.Comment = aReader["comment"].ToString();
                aExamList.Add(aExam);
            }
            aConnectionManager.CloseConnection();
            return aExamList;
        }

        internal Exam GetPostInfo(int ExampId)
        {
            Exam aExam = null;
            string sqlQuery = "SELECT * FROM tblExam WHERE exam_id = " + ExampId + "";
            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            while (aReader.Read())
            {
                aExam = new Exam();
                aExam.ExamId = Convert.ToInt32(aReader["exam_id"]);
                aExam.Name = aReader["name"].ToString();
                aExam.ExamDate = aReader["exam_date"].ToString();
                aExam.Comment = aReader["comment"].ToString();
            }
            aConnectionManager.CloseConnection();
            return aExam;
        }

        internal string UpdateMyExam(Exam aExam)
        {
            string sqlQuery = "UPDATE tblExam SET name = '" + aExam.Name + "', exam_date='" + aExam.ExamDate +
                                 "', comment='" + aExam.Comment + "' WHERE exam_id='"+ aExam.ExamId +"' ";

            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            int ef = aSqlCommand.ExecuteNonQuery();
            if (ef > 0)
            {
                return "Updated";
            }
            else
            {
                return "fail";
            }
            
        }



        internal string SaveExamGrade(ExamGrade aExamGrade)
        {
            string sqlQuery = "INSERT INTO tblGrade VALUES('" + aExamGrade.GradeName + "', " + aExamGrade.GradePoint +
                              ", " + aExamGrade.MarkFrom + ", " + aExamGrade.MarkUpTo + ", '" + aExamGrade.Comment +
                              "', " + aExamGrade.SchoolId + ")";
            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            int ef = aSqlCommand.ExecuteNonQuery();
            if (ef > 0)
            {
                return "New Grade added successfully";
            }
            else
            {
                return "Fail";
            }
        }

        internal List<ExamGrade> GetExamGradeList(int schoolId)
        {
            List<ExamGrade> aExamGradeList = new List<ExamGrade>();
            string sqlQuery = "SELECT * FROM tblGrade WHERE school_id = " + schoolId + "";
            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            while (aReader.Read())
            {
                ExamGrade aExamGrade = new ExamGrade();
                aExamGrade.ExamGradeId = Convert.ToInt32(aReader["grade_id"]);
                aExamGrade.GradeName = aReader["name"].ToString();
                aExamGrade.GradePoint = Convert.ToDouble(aReader["grade_point"]);
                aExamGrade.MarkFrom = Convert.ToInt32(aReader["mark_from"]);
                aExamGrade.MarkUpTo = Convert.ToInt32(aReader["mark_upto"]);
                aExamGrade.Comment = aReader["comment"].ToString();
                aExamGrade.SchoolId = Convert.ToInt32(aReader["school_id"]);
                aExamGradeList.Add(aExamGrade);
            }
            return aExamGradeList;
        }

        internal string ManageClassMark(Mark aMark)
        {
            string sqlQuery = "INSERT INTO tblMark VALUES(" + aMark.MarkObtained + ", " + aMark.MarkTotal + ", '" +
                              aMark.Attendence + "', '" + aMark.Comment + "', " + aMark.StudentId + ", " +
                              aMark.SubjectId + ", " + aMark.ClassId + ", " + aMark.ExamId + ", " + aMark.SchoolId + ")";
            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            int ef = aSqlCommand.ExecuteNonQuery();
            if (ef > 0)
            {
                return "Student mark save successfully";
            }
            else
            {
                return "Fail!!";
            }
        }

        internal List<ExamResultView> GetTheStudentResult(Mark aMark)
        {
            List<ExamResultView> aResultViews = new List<ExamResultView>();
            string sqlQuery = "SELECT tblMark.mark_obtained, tblMark.mark_total, tblMark.attendance, tblSubject.name from tblMark join tblSubject on tblMark.subject_id = tblSubject.subject_id where tblSubject.class_id = " + aMark.ClassId + " AND tblMark.school_id = " + aMark.SchoolId + " AND tblMark.student_id = " + aMark.StudentId + " AND tblMark.exam_id = "+aMark.ExamId+"";
            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            while (aReader.Read())
            {
             ExamResultView aExamResultView = new ExamResultView();
                aExamResultView.MarkObtain = Convert.ToDouble(aReader["mark_obtained"]);
                aExamResultView.MarkTotal = Convert.ToDouble(aReader["mark_total"]);
                aExamResultView.Attendence = aReader["attendance"].ToString();
                aExamResultView.SubjectName = aReader["name"].ToString();
                aResultViews.Add(aExamResultView);
            }
            aConnectionManager.CloseConnection();
            return aResultViews;
        }

        internal string UpdateManageClassMark(Mark aMark)
        {
           string sqlQuery = "UPDATE tblMark SET mark_obtained = "+aMark.MarkObtained+", mark_total="+aMark.MarkTotal+", attendance = '"+aMark.Attendence+"', comment= '"+aMark.Comment+"' WHERE student_id="+aMark.StudentId+" AND subject_id="+aMark.SubjectId+" AND class_id="+aMark.ClassId+" AND exam_id="+aMark.ExamId+" AND school_id="+aMark.SchoolId+"";
           aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            int ef = aSqlCommand.ExecuteNonQuery();
            if (ef > 0)
            {
                return "Result update succesfully";
            }
            else
            {
                return "Fail";
            }
        }

        internal string SaveExamRoutine(ExamRoutine aExamRoutine)
        {
            string sqlQuery = "INSERT INTO tblExamRoutine VALUES('" + aExamRoutine.ExamTitle + "', '" + aExamRoutine.SubjectName +
                             "', '" + aExamRoutine.ExamDate + "', '" + aExamRoutine.ExamDay + "', '" + aExamRoutine.ExamTime +
                             "', " + aExamRoutine.ClassId + "," + aExamRoutine.SchoolId + ")";
            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            int ef = aSqlCommand.ExecuteNonQuery();
            if (ef > 0)
            {
                return "Subject has been added";
            }
            else
            {
                return "Fail to add";
            }
            
        }

        internal List<ExamRoutineView> GetAllExamRoutine(int schoolId)
        {
            List<ExamRoutineView> aExamExamRoutineList = new List<ExamRoutineView>();
            string sqlQuery = "SELECT tblExamRoutine.id, tblExamRoutine.exam_date, tblExamRoutine.exam_day, tblExamRoutine.exam_time, tblExamRoutine.exam_title,tblExamRoutine.school_id,tblExamRoutine.subject_name,tblClass.name FROM tblExamRoutine JOIN tblClass ON tblExamRoutine.class_id = tblClass.class_id   WHERE tblExamRoutine.school_id = " + schoolId + "";
            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            while (aReader.Read())
            {
                ExamRoutineView aExamRoutine = new ExamRoutineView();
                aExamRoutine.Id = Convert.ToInt32(aReader["id"]);
                aExamRoutine.ExamTitle = aReader["exam_title"].ToString();
                aExamRoutine.SubjectName = aReader["subject_name"].ToString();
                aExamRoutine.ExamDate = aReader["exam_date"].ToString();
                aExamRoutine.ExamDay = aReader["exam_day"].ToString();
                aExamRoutine.ExamTime = aReader["exam_time"].ToString();
                aExamRoutine.Classname = aReader["name"].ToString();
                aExamRoutine.SchoolId = Convert.ToInt32(aReader["school_id"]);
              
                aExamExamRoutineList.Add(aExamRoutine);
            }
            return aExamExamRoutineList;
            
        }



        internal ExamRoutine GetExamRoutineInfo(int? id)
        {
            string sqlQuery = "SELECT * FROM tblExamRoutine WHERE id = " + id + "";
            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            ExamRoutine aExamRoutine = new ExamRoutine();
            while (aReader.Read())
            {
                aExamRoutine.Id = Convert.ToInt32(aReader["id"]);
                aExamRoutine.ExamTitle = aReader["exam_title"].ToString();
                aExamRoutine.SubjectName = aReader["subject_name"].ToString();
                aExamRoutine.ExamDate = aReader["exam_date"].ToString();
                aExamRoutine.ExamDay = aReader["exam_day"].ToString();
                aExamRoutine.ExamTime = aReader["exam_time"].ToString();
                aExamRoutine.ClassId = Convert.ToInt32(aReader["class_id"]);
                aExamRoutine.SchoolId = Convert.ToInt32(aReader["school_id"]);

            }
            aConnectionManager.CloseConnection();
            return aExamRoutine;
        }

        internal string UpdateExamRoutine(ExamRoutine aExamRoutineUpate)
        {
            string updateQuery = "UPDATE tblExamRoutine SET exam_title='" + aExamRoutineUpate.ExamTitle + "', subject_name='" + aExamRoutineUpate.SubjectName +
                                  "', exam_date='" + aExamRoutineUpate.ExamDate + "', " +
                                  "exam_day='" + aExamRoutineUpate.ExamDay + "', exam_time='" + aExamRoutineUpate.ExamTime + "', class_id='" +
                                  aExamRoutineUpate.ClassId + "' WHERE id=" + aExamRoutineUpate.Id + "";
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

        internal List<ExamRoutine> GetExamRoutineInfo(int schoolId, int clssId)
        {
            List<ExamRoutine> aExamRoutinesList = new List<ExamRoutine>();
            string sqlQuery = "SELECT * FROM tblExamRoutine WHERE school_id = " + schoolId + " AND class_id=" + clssId + "";
            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            ExamRoutine aExamRoutine = null;

            while (aReader.Read())
            {
                aExamRoutine = new ExamRoutine();
                aExamRoutine.Id = Convert.ToInt32(aReader["id"]);
                aExamRoutine.ExamTitle = aReader["exam_title"].ToString();
                aExamRoutine.SubjectName = aReader["subject_name"].ToString();
                aExamRoutine.ExamDate = aReader["exam_date"].ToString();
                aExamRoutine.ExamDay = aReader["exam_day"].ToString();
                aExamRoutine.ExamTime = aReader["exam_time"].ToString();
                aExamRoutine.ClassId = Convert.ToInt32(aReader["class_id"]);
                aExamRoutine.SchoolId = Convert.ToInt32(aReader["school_id"]);
                aExamRoutinesList.Add(aExamRoutine);

            }
            aConnectionManager.CloseConnection();
            return aExamRoutinesList;
        }

        internal ExamRoutine GetExamRoutineTitle(int schoolId, int clssId)
        {
           
            string sqlQuery = "SELECT * FROM tblExamRoutine WHERE school_id = " + schoolId + " AND class_id=" + clssId + "";
            aSqlCommand = new SqlCommand(sqlQuery, aConnectionManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            ExamRoutine aExamRoutine = null;
            while (aReader.Read())
            {
                aExamRoutine = new ExamRoutine();
              
                aExamRoutine.ExamTitle = aReader["exam_title"].ToString();

            }
            aConnectionManager.CloseConnection();
            return aExamRoutine;
        }

        internal string DeleteExamRoutine(int? id)
        {
            string sqlQuery = "DELETE FROM tblExamRoutine WHERE id =" + id + "";
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