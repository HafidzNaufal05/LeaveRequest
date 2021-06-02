using Dapper;
using LeaveRequest.Base.Controller;
using LeaveRequest.Context;
using LeaveRequest.Handler;
using LeaveRequest.Models;
using LeaveRequest.Repositories.Data;
using LeaveRequest.Repositories.Interfaces;
using LeaveRequest.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class AccountController : BaseController<Account,AccountRepository, int>
    {
        private AccountRepository accountRepository;
        private ParameterRepository parameterRepository;
        private MyContext myContext;
        private IConfiguration configuration;
        private readonly IGenericDapper dapper;

        public AccountController(IConfiguration configuration, AccountRepository accountRepository, MyContext myContext, IGenericDapper dapper, ParameterRepository parameterRepository) : base(accountRepository)
        {
            this.configuration = configuration;
            this.accountRepository = accountRepository;
            this.myContext = myContext;
            this.dapper = dapper;
            this.parameterRepository = parameterRepository;
        }


        [HttpPost("Register")]
        public ActionResult Registration(RegisterVM registerVM)
        {
            var HashPassword = Hashing.HashPassword(registerVM.Password);

            DateTime DateJoin = registerVM.JoinDate;
            DateTime Today = DateTime.Today;
            TimeSpan ts = new TimeSpan();
            ts = Today.Subtract(DateJoin);
            Parameter parameter = new Parameter();
            if (ts.Days < 365)
            {
                parameter = parameterRepository.getByName("Dibawah satu tahun");
            }
            else if (ts.Days >= 365 && ts.Days < 1826)
            {
                parameter = parameterRepository.getByName("Diatas satu tahun");
            }
            else if (ts.Days >= 1826)
            {
                parameter = parameterRepository.getByName("Diatas lima tahun");
            }

            //EmployeeRole employeeRole = new EmployeeRole();
            var dbparams = new DynamicParameters();
            //dbparams.Add("NIK", registerVM.NIK, DbType.String);
            dbparams.Add("FirstName", registerVM.FirstName, DbType.String);
            dbparams.Add("LastName", registerVM.LastName, DbType.String);
            dbparams.Add("BirthDate", registerVM.BirthDate, DbType.DateTime);
            dbparams.Add("Gender", registerVM.Gender, DbType.String);
            dbparams.Add("Address", registerVM.Address, DbType.String);
            dbparams.Add("MaritialStatus", registerVM.MaritialStatus, DbType.String);
            dbparams.Add("PhoneNumber", registerVM.PhoneNumber, DbType.String);
            dbparams.Add("Email", registerVM.Email, DbType.String);
            dbparams.Add("JoinDate", registerVM.JoinDate, DbType.DateTime);
            dbparams.Add("RemainingQuota", parameter.Value, DbType.Int32);
            dbparams.Add("DepartmentId", registerVM.DepartmentId, DbType.Int32);
            dbparams.Add("NIK_Manager", registerVM.NIK_Manager, DbType.String);
            dbparams.Add("Role", 1, DbType.Int32);
            dbparams.Add("Password", HashPassword, DbType.String);

            var result = Task.FromResult(dapper.Insert<int>("[dbo].[SP_Register]", dbparams, commandType: CommandType.StoredProcedure));
            return Ok(new { Status = "Success", Message = "User has been registered seccessfully" });
        }

        [HttpPost("Login")]
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

        [Authorize]
        [HttpPost("ChangePassword")]
        public ActionResult ChangePassword(string email, string oldPassword, string newPassword)
        {
            var emailCheck = myContext.Accounts.SingleOrDefault(e => e.Employee.Email == email);
            var passwordCheck = Hashing.ValidatePassword(oldPassword, emailCheck.Password);

            if (emailCheck != null)
            {
                if (passwordCheck)
                {
                    var NewPassword = Hashing.HashPassword(newPassword);
                    emailCheck.Password = NewPassword;
                    var Save = myContext.SaveChanges();
                    if (Save > 0)
                    {
                        return Ok(new { message = "Password Changed", status = "Ok" });
                    }
                }
                else
                {
                    return StatusCode(404, new { status = "404", message = "Wrong password" });
                }
            }
            return NotFound();
        }


        [HttpPost("ForgotPassword")]
        public ActionResult ForgotPassword(string email)
        {
            var CheckAccount = myContext.Employees.SingleOrDefault(e => e.Email == email);
            if (CheckAccount.Email == email)
            {
                var getEmp = myContext.Employees.Where(e => e.NIK == CheckAccount.NIK).FirstOrDefault();
                var jwt = new JwtService(configuration);
                var token = jwt.GenerateSecurityToken(CheckAccount.FirstName, CheckAccount.Email, "Admin");
                var SendEmail = new EmailRequest(myContext);
                SendEmail.SendForgotPassword(token, getEmp);
                return Ok("Check Your Email");
            }
            else
            {
                return NotFound("Email Not Found");
            }
        }

        //[Authorize]
        [HttpPost("ResetPassword")]
        public ActionResult ResetPassword(string email, string newPassword, string confirmPassword)
        {
            var GetEmp = myContext.Employees.SingleOrDefault(e => e.Email == email);
            var GetAcc = myContext.Accounts.SingleOrDefault(a => a.Employee.Email == email);
            if (GetEmp.Email != null)
            {
                if (newPassword == confirmPassword)
                {
                    GetAcc.Password = Hashing.HashPassword(newPassword);
                    var save = myContext.SaveChanges();
                    if (save > 0)
                    {
                        return Ok("Password Berhasil Dirubah");
                    }
                }
                else
                {
                    return BadRequest("Your Password is incorrect");
                }
            }

            return NotFound("Email Not Found");

        }
    }
}
