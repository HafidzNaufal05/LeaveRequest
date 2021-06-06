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
    public class AccountController : BaseController<Account, AccountRepository, int>
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
            var dbparams = new DynamicParameters();
            dbparams.Add("FirstName", registerVM.FirstName, DbType.String);
            dbparams.Add("LastName", registerVM.LastName, DbType.String);
            dbparams.Add("BirthDate", registerVM.BirthDate, DbType.DateTime);
            dbparams.Add("Gender", registerVM.Gender, DbType.String);
            dbparams.Add("Address", registerVM.Address, DbType.String);
            dbparams.Add("MaritialStatus", registerVM.MaritialStatus, DbType.String);
            dbparams.Add("PhoneNumber", registerVM.PhoneNumber, DbType.String);
            dbparams.Add("Email", registerVM.Email, DbType.String);
            dbparams.Add("JoinDate", registerVM.JoinDate, DbType.DateTime);
            dbparams.Add("DepartmentId", registerVM.DepartmentId, DbType.Int32);
            dbparams.Add("Password", HashPassword, DbType.String);

            var result = Task.FromResult(dapper.Insert<int>("[dbo].[SP_RegisterEmp]", dbparams, commandType: CommandType.StoredProcedure));
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
                var token = jwt.GenerateSecurityToken(result.NIK, result.Email, result.Role);
                return Ok(token);
            }

            return BadRequest("Failed to login");

        }

        //[Authorize]
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
        public ActionResult ForgotPassword(ForgotPasswordVM forgotPasswordVM)
        {
            var CheckAccount = myContext.Employees.SingleOrDefault(e => e.Email == forgotPasswordVM.Email);
            if (CheckAccount.Email == forgotPasswordVM.Email)
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
        public ActionResult ResetPassword(ResetPasswordVM resetPasswordVM)
        {
            var GetEmp = myContext.Employees.SingleOrDefault(e => e.Email == resetPasswordVM.Email);
            var GetAcc = myContext.Accounts.SingleOrDefault(a => a.Employee.Email == resetPasswordVM.Email);
            if (GetEmp.Email != null)
            {
                if (resetPasswordVM.newPassword == resetPasswordVM.confirmPassword)
                {
                    GetAcc.Password = Hashing.HashPassword(resetPasswordVM.newPassword);
                    var save = myContext.SaveChanges();
                    if (save > 0)
                    {
                        return Ok("Reset Password Has Been Successfuly");
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
