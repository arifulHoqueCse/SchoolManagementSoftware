using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SchoolApp.Models.DbGateway
{
    public class SectionDbGateway:Common
    {
        SqlConnectionManager aSqlConManager = new SqlConnectionManager();
        internal string SaveSection(Section aSection)
        {
            string sqlQuery = "INSERT INTO tbl_section VALUES('" + aSection.SectionName + "', " + aSection.SchoolId +
                              ")";
            aSqlCommand = new SqlCommand(sqlQuery, aSqlConManager.GetConnection());
            int ef = aSqlCommand.ExecuteNonQuery();
            if (ef > 0)
            {
                return "Add New section successfully";
            }
            else
            {
                return "Failed to add new section";
            }
        }

        internal List<Section> GetAllSection(int schoolid)
        {
            List<Section> sectionList = new List<Section>();
            string sqlQuery = "SELECT * FROM tbl_section WHERE school_id=" + schoolid + "";
            aSqlCommand = new SqlCommand(sqlQuery, aSqlConManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            while (aReader.Read())
            {
                Section aSection = new Section();
                aSection.SectionId = Convert.ToInt32(aReader["section_id"]);
                aSection.SectionName = aReader["section_name"].ToString();
                aSection.SchoolId = Convert.ToInt32(aReader["school_id"]);
                sectionList.Add(aSection);
            }
            return sectionList;
        }

        public List<Section> GetTheSection(int classid, int schoolid)
        {
            List<Section> sectionList = new List<Section>();
            string sqlQuery = "SELECT tbl_section.section_name, tbl_section.section_id FROM tbl_section join tbl_sec_class ON tbl_section.section_id= tbl_sec_class.section_id WHERE tbl_sec_class.class_id = "+classid+" AND tbl_sec_class.school_id = "+schoolid+"";
            aSqlCommand = new SqlCommand(sqlQuery, aSqlConManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            while (aReader.Read())
            {
                Section aSection = new Section();
                aSection.SectionId = Convert.ToInt32(aReader["section_id"]);
                aSection.SectionName = aReader["section_name"].ToString();
                sectionList.Add(aSection);
            }
            return sectionList;
        }


        internal Section GetSpecificSection(int SectionId)
        {

            string sqlQuery = "SELECT * FROM tbl_section WHERE section_id=" + SectionId + "";
            aSqlCommand = new SqlCommand(sqlQuery, aSqlConManager.GetConnection());
            aReader = aSqlCommand.ExecuteReader();
            Section aSection = null;
            while (aReader.Read())
            {
                aSection = new Section();
                aSection.SectionId = Convert.ToInt32(aReader["section_id"]);
                aSection.SectionName = aReader["section_name"].ToString();
                aSection.SchoolId = Convert.ToInt32(aReader["school_id"]);
               
            }
            return aSection;
        }

        internal string UpdateSection(Section aSection)
        {
            string updateQuery = "UPDATE tbl_section SET section_name='" + aSection.SectionName + "' WHERE section_id=" + aSection.SectionId + "";
            aSqlCommand = new SqlCommand(updateQuery, aSqlConManager.GetConnection());
            int effectedrows = aSqlCommand.ExecuteNonQuery();
            if (effectedrows > 0)
            {
                return " Section has been updated";
            }
            else
            {
                return "Please fill all information correctly";
            }
        }
    }
}