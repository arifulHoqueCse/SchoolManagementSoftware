using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolApp.Models
{
    public class AccountsFee
    {
        public int Id { set; get; }
        public string StudentReg { get; set; }
        public string StudentName { get; set; }
        public string Month  { get; set; }
        public string ExamFee  { get; set; }
        public string Amount { get; set; }
        public DateTime PaymentDate  { get; set; }
        public int SchoolId  { get; set; }
        public string Status  { get; set; }

    }
}