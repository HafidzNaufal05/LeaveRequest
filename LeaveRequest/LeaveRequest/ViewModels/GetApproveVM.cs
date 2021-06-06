using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequest.ViewModels
{
    public class GetApproveVM
    {
        public int Id { get; set; }
        public string LeaveCategory { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ReasonRequest { get; set; }
        public string Notes { get; set; }
        //public StatusRequest StatusRequest { get; set; }
        public string StatusRequest { get; set; }

        public string NIK { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string MaritialStatus { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime JoinDate { get; set; }
        public int RemainingQuota { get; set; }
#nullable enable
        public string? NIK_Manager { get; set; }
    }
}
