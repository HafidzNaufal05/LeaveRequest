using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequest.Models
{
    [Table("TB_M_Role")]
    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public ICollection<EmployeeRole> EmployeeRoles { get; set; }
        public ICollection<Account> Accounts { get; set; }
    }
}
