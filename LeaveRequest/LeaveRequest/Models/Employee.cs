using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequest.Models
{
    [Table("TB_M_Employee")]
    public class Employee
    {
        [Key, ForeignKey(nameof(Manager))]
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
        public Department Department{ get; set; }
        public Account Account { get; set; }
        public virtual Employee Manager { get; set; }

        public ICollection<EmployeeRole> EmployeeRoles { get; set; }
        public ICollection<Request> Requests { get; set; }


    }
}
