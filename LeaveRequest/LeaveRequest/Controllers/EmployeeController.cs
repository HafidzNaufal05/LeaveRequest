using LeaveRequest.Base.Controller;
using LeaveRequest.Context;
using LeaveRequest.Models;
using LeaveRequest.Repositories.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequest.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : BaseController<Employee, EmployeeRepository, int>
    {
        private MyContext myContext;
        public EmployeeController(EmployeeRepository employeeRepository, MyContext myContext) : base(employeeRepository)
        {
            this.myContext = myContext;
        }

        //public int UpdateRemainingLeave(string nik, double leaveRequest)
        //{
        //    var data = myContext.Employees.Where(e => e.NIK == nik).FirstOrDefault();

        //    data.RemainingQuota = data.RemainingQuota - Convert.ToInt32(leaveRequest);
        //    myContext.Update(data);
        //    myContext.SaveChanges();
        //    return 1;
        //}
    }
}
