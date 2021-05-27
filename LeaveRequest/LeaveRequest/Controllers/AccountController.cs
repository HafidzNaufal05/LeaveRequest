using Dapper;
using LeaveRequest.Base.Controller;
using LeaveRequest.Context;
using LeaveRequest.Handler;
using LeaveRequest.Models;
using LeaveRequest.Repositories.Data;
using LeaveRequest.Repositories.Interfaces;
using LeaveRequest.Services;
using LeaveRequest.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LeaveRequest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private AccountRepository accountRepository;
        private MyContext myContext;
        private IConfiguration configuration;
        private readonly IGenericDapper dapper;

        public AccountController(IConfiguration configuration, AccountRepository accountRepository, MyContext myContext, IGenericDapper dapper)
        {
            this.configuration = configuration;
            this.accountRepository = accountRepository;
            this.myContext = myContext;
            this.dapper = dapper;
        }

        [Route("register")]
        [HttpPost]
        public ActionResult Registration(RegisterVM registerVM)
        {
            var HashPassword = Hashing.HashPassword(registerVM.Password);

            var dbparams = new DynamicParameters();
            dbparams.Add("NIK", registerVM.NIK, DbType.String);
            dbparams.Add("FirstName", registerVM.FirstName, DbType.String);
            dbparams.Add("LastName", registerVM.LastName, DbType.String);
            dbparams.Add("BirthDate", registerVM.BirthDate, DbType.DateTime);
            dbparams.Add("Gender", registerVM.Gender, DbType.String);
            dbparams.Add("Address", registerVM.Address, DbType.String);
            dbparams.Add("MaritialStatus", registerVM.MaritialStatus, DbType.String);
            dbparams.Add("PhoneNumber", registerVM.PhoneNumber, DbType.String);
            dbparams.Add("Email", registerVM.Email, DbType.String);
            dbparams.Add("JoinDate", registerVM.JoinDate, DbType.DateTime);
            dbparams.Add("DepartmentId", registerVM.DepartmentId, DbType.String);
            dbparams.Add("Role", registerVM.RoleId, DbType.String);
            dbparams.Add("Password", HashPassword, DbType.String);

            var result = Task.FromResult(dapper.Insert<int>("[dbo].[SP_Register]", dbparams, commandType: CommandType.StoredProcedure));
            return Ok(new { Status = "Success", Message = "User has been registered seccessfully" });
        }

        [Route("login")]
        [HttpPost]
        public ActionResult Login(LoginVM loginVM)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("Email", loginVM.Email, DbType.String);
            var result = Task.FromResult(dapper.Get<dynamic>("[dbo].[SP_Login]", dbparams,
                commandType: CommandType.StoredProcedure)).Result;

            if (Hashing.ValidatePassword(loginVM.Password, result.Password))
            {
                var jwt = new JwtService(configuration);
                var token = jwt.GenerateSecurityToken(result.Name, result.Email, result.Role);
                return Ok(new { token });
            }

            return BadRequest("Failed to login");

        }
    }
}
