using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequest.Models
{
    [Table("TB_T_EmployeeRole")]
    public class EmployeeRole
    {
        public int NIK { get; set; }
        public Employee Employee { get; set; }
        public Role Role { get; set; }
    }
}
