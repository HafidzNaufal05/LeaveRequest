using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequest.ViewModels
{
    public class RequestVM
    {
        public int Id { get; set; }
        public string EmployeeNIK { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ReasonRequest { get; set; }
        public string Notes { get; set; }
        public int RemainingQuota { get; set; }
    }
}
