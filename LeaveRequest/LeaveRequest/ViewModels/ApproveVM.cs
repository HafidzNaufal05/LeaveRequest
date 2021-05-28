using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequest.ViewModels
{
    public class ApproveVM
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public Boolean IsApproved { get; set; }
    }
}
