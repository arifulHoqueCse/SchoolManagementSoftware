using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SchoolApp.Models.DbGateway
{
    public class AccountsDbGateway : Common
    {
        SqlConnectionManager aConnectionManager = new SqlConnectionManager();


        internal string SubmitFee(AccountsFee aAccountsFee)
        {
            string sqlQuery = "INSERT INTO tblAccountsStudentFee VALUES('" + aAccountsFee.StudentReg + "', '" + aAccountsFee.StudentName +
                             "', '" + aAccountsFee.Month + "', " + aAccountsFee.ExamFee + ", '" + aAccountsFee.PaymentDate + "','" + aAccountsFee.Amount + "', " + aAccountsFee.SchoolId + ", '" + aAccountsFee.Status + "')";

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
    }
}