using LeaveRequest.Base.Controller;
using LeaveRequest.Context;
using LeaveRequest.Models;
using LeaveRequest.Repositories.Data;
using LeaveRequest.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
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
        private readonly IConfiguration configuration;
        private readonly IGenericDapper dapper;
        public EmployeeController(EmployeeRepository employeeRepository, MyContext myContext, IConfiguration configuration, IGenericDapper dapper) : base(employeeRepository)
        {
            this.myContext = myContext;
            this.configuration = configuration;
            this.dapper = dapper;
        }

        [HttpGet("GetEmployeeData")]
        public List<dynamic> GetEmployeeData()
        {
            string query = string.Format("SELECT emp.NIK, req.* FROM TB_M_Employee AS emp INNER JOIN TB_T_Request AS req ON req.EmployeeNIK = emp.NIK");

            List<dynamic> get = dapper.GetAllNoParam<dynamic>(query, CommandType.Text);

            return get;
        }

        [HttpPut("UpdateData")]
        public List<dynamic> UpdateData()
        {
            string query = string.Format("SELECT emp.NIK, req.* FROM TB_M_Employee AS emp INNER JOIN TB_T_Request AS req ON req.EmployeeNIK = emp.NIK");

            List<dynamic> get = dapper.GetAllNoParam<dynamic>(query, CommandType.Text);

            return get;
        }

    }
}
