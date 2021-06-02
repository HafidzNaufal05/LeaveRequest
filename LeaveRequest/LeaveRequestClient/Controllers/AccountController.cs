using LeaveRequest.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace LeaveRequestClient.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public HttpStatusCode Register(RegisterVM registerVM)
        {
            var httpClient = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(registerVM), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync("https://localhost:44338/api/Account/Register", content).Result;
            return result.StatusCode;
        }

        [HttpPost]
        public HttpStatusCode ForgotPassword(string email)
        {
            var url = "https://localhost:44338/api/Account/ForgotPassword?email=" + email;
            var httpClient = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(email), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(url, content).Result;
            return result.StatusCode;
        }

        [HttpPost]
        public HttpStatusCode ResetPassword(string email, string newPassword, string confirmPassword)
        {
            var url = "https://localhost:44338/api/Account/ResetPassword?email=" + email + "&newPassword=" + newPassword + "&confirmPassword=" + confirmPassword;
            var httpClient = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(email), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(url, content).Result;
            return result.StatusCode;
        }

        [HttpPost]
        public HttpStatusCode Login(LoginVM loginVM)
        {
            var httpClient = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(loginVM), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync("https://localhost:44338/api/Account/Login", content).Result;
            return result.StatusCode;
        }
    }
}
