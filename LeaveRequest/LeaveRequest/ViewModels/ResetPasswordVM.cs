using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequest.ViewModels
{
    public class ResetPasswordVM
    {
        public string Email { get; set; }
        public string newPassword { get; set; }
        public string confirmPassword { get; set; }
    }
}
