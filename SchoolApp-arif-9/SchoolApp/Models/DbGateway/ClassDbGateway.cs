using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SchoolApp.Models.View;

namespace SchoolApp.Models.DbGateway
{
    public class ClassDbGateway : Common
    {
        SqlConnectionManager aManager = new SqlConnectionManager();

        internal string AddNewClass(Class aClass)
        {
            string sqlQuery = "INSERT INTO tblClass VALUES('" + aClass.Name + "', '" + aClass.NameNumeric + "', " +
                              aClass.TeacherId + ", " + aClass.SchoolId + ")";
            string findClassId = "SELECT MAX(class_id) AS classid FROM tblClass WHERE school_id=" + aClass.SchoolId + "";
            aSqlCommand = new SqlCommand(sqlQuery, aManager.GetConnection());
            int effectedRows = aSqlCommand.ExecuteNonQuery();
            if (effectedRows > 0)
            {
                aSqlCommand.CommandText = findClassId;
                aReader = aSqlCommand.ExecuteReader();
                aReader.Read();
                int ClassId = Convert.ToInt32(aReader["classid"]);
                aManager.CloseConnection();
                string addQuery = "INSERT INTO tbl_sec_class VALUES(" + ClassId + ", " + aClass.SectionId + ", "+aClass.SchoolId+")";
                aSqlCommand = new SqlCommand(addQuery, aManager.GetConnection());
                int eff = aSqlCommand.ExecuteNonQuery();
                if (eff > 0)
                {
                    return "Add New Class Successfull";
                }
                else
                {
                    return "fail";
                }

            }
            else
            {
                return "Some Information missing or communicate with addministrator";
            }
        }

        internal List<ClassListView> GetAllClassList(int schoolId)
        {
            List<ClassListView> aClasseList = new List<ClassListView>();
            string sqlQuery = "SELECT tblClass.name, tblTeacher.name tname FROM tblClass JOIN tblTeacher ON tblClass.teacher_id = tblTeacher.teacher_id WHERE tblClass.school_id="+schoolId+"";
            aSqlCommand = new SqlCommand(sqlQuery, aManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            while (aReader.Read())
            {
                ClassListView aClassListView = new ClassListView();
                aClassListView.ClassName = aReader["name"].ToString();
                aClassListView.TeacherName = aReader["tname"].ToString();
                aClasseList.Add(aClassListView);
            }
            return aClasseList;
        }

        internal List<Class> GetAllClass(int schoolId)
        {
            List<Class> classList = new List<Class>();
            string sqlQuery = "SELECT * FROM tblClass WHERE school_id=" + schoolId + "";
            aSqlCommand = new SqlCommand(sqlQuery, aManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            while (aReader.Read())
            {
                Class aClass = new Class();
                aClass.ClassId = Convert.ToInt32(aReader["class_id"]);
                aClass.Name = aReader["name"].ToString();
                classList.Add(aClass);
            }
            aManager.CloseConnection();
            return classList;
        }

        internal string SaveClassRoutine(ClassRoutine aRoutine)
        {
            string sqlQuery = "INSERT INTO tblClassRoutine VALUES('" + aRoutine.FullStartTime + "', '" +
                              aRoutine.FullEndTime + "', '" + aRoutine.ClassDay + "', " + aRoutine.ClassId + ", " +
                              aRoutine.SubjectId + ", " + aRoutine.SchoolId + ")";
            aSqlCommand = new SqlCommand(sqlQuery, aManager.GetConnection());
            int ef = aSqlCommand.ExecuteNonQuery();
            aManager.CloseConnection();
            if (ef > 0)
            {
                return "New Routine added";
            }
            else
            {
                return "Failed Add Routine, Please provide valid data";
            }

        }

        internal List<Class> GetAllRoutine(int schoolId)
        {
            List<Class> ClassList = new List<Class>();
            string sqlQuery = "SELECT * FROM tblClass where school_id = 1;";
            aSqlCommand = new SqlCommand(sqlQuery, aManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            while (aReader.Read())
            {
                Class aClass = new Class();
                aClass.ClassId = Convert.ToInt32(aReader["class_id"]);
                aClass.Name = aReader["name"].ToString();
                ClassList.Add(aClass);
            }
            aManager.CloseConnection();
            return ClassList;
        }

        internal List<EditRoutine> GetTheRoutine(int routineId, int classId, int schoolId)
        {
            List<EditRoutine> editRoutineList = new List<EditRoutine>();
            string sqlQuery = "SELECT tblClassRoutine.routine_id, tblClassRoutine.time_start, tblClassRoutine.time_end, tblClassRoutine.class_day, tblClass.class_id," +
                              " tblClass.name as class_name, tblSubject.subject_id, tblSubject.name as subject_name from tblClassRoutine join tblClass on tblClassRoutine.class_id = tblClass.class_id join tblSubject on tblClassRoutine.subject_id = tblSubject.subject_id where tblClassRoutine.routine_id = '" + routineId + "' and tblClassRoutine.class_id = '" + classId + "' and tblClassRoutine.school_id = '" + schoolId + "'";
            aSqlCommand = new SqlCommand(sqlQuery, aManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            while (aReader.Read())
            {
                EditRoutine aRoutine = new EditRoutine();
                aRoutine.RoutineId = Convert.ToInt32(aReader["routine_id"]);
                aRoutine.TimeStart = aReader["time_start"].ToString();
                aRoutine.TimeEnd = aReader["time_end"].ToString();
                aRoutine.ClassDay = aReader["class_day"].ToString();
                aRoutine.ClassId = Convert.ToInt32(aReader["class_id"]);
                aRoutine.ClassName = aReader["class_name"].ToString();
                aRoutine.SubjectId = Convert.ToInt32(aReader["subject_id"]);
                aRoutine.SubjectName = aReader["subject_name"].ToString();
                editRoutineList.Add(aRoutine);
            }
            return editRoutineList;
        }

        internal string UpdateRoutine(ClassRoutine aRoutine, int routineId)
        {
            string sqlQuery = "UPDATE tblClassRoutine SET time_start='" + aRoutine.FullStartTime + "', time_end='" +
                              aRoutine.FullEndTime + "', class_day='" + aRoutine.ClassDay + "', class_id=" +
                              aRoutine.ClassId + ", subject_id=" + aRoutine.SubjectId + " where routine_id = " +
                              routineId + " AND school_id = " + aRoutine.SchoolId + "";
            bool isSuccess = DdlQueryExecute(sqlQuery);
            if (isSuccess)
            {
                return "Routine Update Successfully";
            }
            else
            {
                return "Failed Update";
            }
        }


        private bool DdlQueryExecute(string sqlQuery)
        {
            aSqlCommand = new SqlCommand(sqlQuery, aManager.GetConnection());
            int ef = aSqlCommand.ExecuteNonQuery();
            aManager.CloseConnection();
            if (ef > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal List<SubjectView> GetTheSubjectDetails(int userId, int schoolId)
        {
            string sqlQuery = "select tblSubject.name as subjectname, tblClass.name as classname, tblTeacher.name as teachername from tblSubject join tblStudent on tblSubject.class_id = tblStudent.class_id join tblClass on tblStudent.class_id = tblClass.class_id join tblTeacher on tblSubject.teacher_id = tblTeacher.teacher_id where tblStudent.student_id = " + userId + " AND tblStudent.school_id=" + schoolId + "";
            aSqlCommand = new SqlCommand(sqlQuery, aManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            List<SubjectView> aSubjectViews = new List<SubjectView>();
            while (aReader.Read())
            {
                SubjectView aSubjectView = new SubjectView();
                aSubjectView.ClassName = aReader["classname"].ToString();
                aSubjectView.SubjectName = aReader["subjectname"].ToString();
                aSubjectView.TeacherName = aReader["teachername"].ToString();
                aSubjectViews.Add(aSubjectView);
            }
            aManager.CloseConnection();
            return aSubjectViews;
        }

        internal Class GetTheStudentClassName(int studentId, int schoolId)
        {
            string sqlQuery = "SELECT tblClass.name as classname, tblClass.class_id from tblStudent join tblClass on tblStudent.class_id = tblClass.class_id where tblStudent.student_id = "+studentId+" and tblStudent.school_id="+schoolId+"";
            aSqlCommand = new SqlCommand(sqlQuery, aManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            Class aClass = null;
            while (aReader.Read())
            {
                aClass = new Class();
                aClass.ClassId = Convert.ToInt32(aReader["class_id"]);
                aClass.Name = Convert.ToString(aReader["classname"]);
            }
            aManager.CloseConnection();
            return aClass;
        }
    }
}