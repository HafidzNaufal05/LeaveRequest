using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeaveRequest.Context;
using LeaveRequest.Handler;
using LeaveRequest.Models;
using Microsoft.EntityFrameworkCore;

namespace LeaveRequest.Repositories.Data
{
    public class AccountRepository : GeneralRepository<Account, MyContext, string>
    {
        private readonly MyContext myContext;

        public AccountRepository(MyContext myContext) : base(myContext)
        {
            //this.myContext = myContext;
        }

        //public int ResetPassword(string email)
        //{
        //    string resetCode = Guid.NewGuid().ToString();
        //    var time24 = DateTime.Now.ToString("HH:mm:ss");

        //    var getemp = myContext.Employees.Include(u => u.Account).Where(a => a.Email == email).FirstOrDefault();

        //    if (getemp == null)
        //    {
        //        return 0;
        //    }
        //    else
        //    {
        //        var password = Hashing.HashPassword(resetCode);
        //        getemp.Account.Password = password;
        //        var result = myContext.SaveChanges();
        //        //sendEmail.SendForgotPassword2(resetCode, email);
        //        return result;
        //    }
        //}
    }
}
