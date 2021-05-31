using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequest.ViewModels
{
    public class RegisterVM
    {
        public string NIK { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required(ErrorMessage = "Tidak boleh kosong"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string MaritialStatus { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        [Required(ErrorMessage = "Tidak boleh kosong"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime JoinDate { get; set; }
        //public int RemainingQuota { get; set; }
        public string DepartmentId { get; set; }
        public string NIK_Manager { get; set; }
        //public string RoleId { get; set; }
        public string Password { get; set; }
    }
}
