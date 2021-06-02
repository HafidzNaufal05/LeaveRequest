using LeaveRequest.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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
            var token = HttpContext.Session.GetString("token");
            ViewData["token"] = token;
            return result.StatusCode;
        }

        [HttpPost]
        public string Login(LoginVM loginVM)
        {
            var httpClient = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(loginVM), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync("https://localhost:44338/api/Account/Login", content).Result;

            var token = result.Content.ReadAsStringAsync().Result;
            HttpContext.Session.SetString("token", token);

            if (result.IsSuccessStatusCode)
            {
                var jwtReader = new JwtSecurityTokenHandler();
                var jwt = jwtReader.ReadJwtToken(token);

                var role = jwt.Claims.First(c => c.Type == "role").Value;
                if (role == "Manager")
                {
                    return Url.Action("Index", "Manager");
                }
                else if (role == "HRD")
                {
                    return Url.Action("Index", "HRD");
                }
                else if (role == "Admin")
                {
                    return Url.Action("Index", "Admin");
                }
                else
                {
                    return Url.Action("Index", "Employee");
                }
            }
            else
            {
                return "Error";
                //return BadRequest(new { result });
            }
        }
    }
}
