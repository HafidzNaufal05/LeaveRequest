using LeaveRequest.Context;
using LeaveRequest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequest.Repositories.Data
{
    public class SendEmailRepository : GeneralRepository<SendEmail, MyContext, int>
    {
        private readonly MyContext myContext;
        public SendEmailRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
