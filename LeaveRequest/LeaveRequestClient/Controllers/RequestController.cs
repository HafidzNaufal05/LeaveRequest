using LeaveRequest.Models;
using LeaveRequest.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LeaveRequestClient.Controllers
{
    public class RequestController : Controller
    {
        private readonly object client;

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult IndexHistory()
        {
            return View();
        }

        [HttpGet]
        public string Get(int Id)
        {
            var httpClient = new HttpClient();
            var response = httpClient.GetAsync("https://localhost:44338/api/Request/" + Id).Result;
            var apiResponse = response.Content.ReadAsStringAsync();
            return apiResponse.Result;
        }

        [HttpGet]
        public List<dynamic> GetHistoryRequest()
        {
            var token = HttpContext.Session.GetString("token");


            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = httpClient.GetAsync("https://localhost:44338/api/Request/GetHistoryRequest3").Result;

            var apiResponse = response.Content.ReadAsStringAsync().Result;
            List<dynamic> data = JsonConvert.DeserializeObject<List<dynamic>>(apiResponse);
            return data;
        }
        //public async Task<IActionResult> EmpProfile()
        //{
        //    ViewBag.Role = HttpContext.Session.GetString("role");
        //    //if (ViewBag.Role != "Employee")
        //    //{
        //    //    return RedirectToAction("Error", "Home");
        //    //}

        //    ViewBag.NIK = HttpContext.Session.GetString("nik");

        //    var header = HttpContext.Session.GetString("token");
        //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", header);

        //    var id = ViewBag.NIK;

        //    var response = await httpClient.GetAsync("Employee/" + id);
        //    string apiResponse = await response.Content.ReadAsStringAsync();
        //    var data = JsonConvert.DeserializeObject<ResponseVM<Employee>>(apiResponse);
        //    return new JsonResult(data);
        //}

        [HttpPost]
        public HttpStatusCode SubmitRequest(RequestVM requestVM)
        {
            var token = HttpContext.Session.GetString("token");
            var jwtReader = new JwtSecurityTokenHandler();
            var jwt = jwtReader.ReadJwtToken(token);

            var NIK = jwt.Claims.First(c => c.Type == "unique_name").Value;
            requestVM.EmployeeNIK = NIK;
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            StringContent content = new StringContent(JsonConvert.SerializeObject(requestVM), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync("https://localhost:44338/api/Request/RequestCuti", content).Result;
            return result.StatusCode;
        }

        [HttpGet]
        public string GetHistory(int Id)
        {
            var httpClient = new HttpClient();
            var response = httpClient.GetAsync("https://localhost:44338/api/Request/RequestHistory" + Id).Result;
            var apiResponse = response.Content.ReadAsStringAsync();
            return apiResponse.Result;
        }
    }
}
