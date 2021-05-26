using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequest.Models
{
    [Table("TB_M_Department")]
    public class Department
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
    }
}
