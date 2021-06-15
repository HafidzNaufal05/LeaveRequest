using LeaveRequest.Models;
using LeaveRequest.ViewModels;
using LeaveRequestClient.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LeaveRequestClient.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public AdminController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }        
        public IActionResult Index2()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        //[HttpGet]
        //public String Get(int Id)
        //{
        //    var httpClient = new HttpClient();
        //    var response = httpClient.GetAsync("https://localhost:44338/api/Employee/" + Id).Result;
        //    var apiResponse = response.Content.ReadAsStringAsync();
        //    return apiResponse.Result;
        //}

        [HttpPut]
        public HttpStatusCode UpdateRole(Employee employee)
        {
            var httpClient = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(employee), Encoding.UTF8, "application/json");
            var result = httpClient.PutAsync("https://localhost:44338/api/Request/SubmitRejectManager", content).Result;
            return result.StatusCode;
        }

        [HttpGet]
        public Employee Edit(string nik)
        {
            var client = new HttpClient();
            var response = client.GetAsync("https://localhost:44338/api/Employee/" + nik).Result;
            var apiResponse = response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<Employee>(apiResponse.Result);
            return data;
        }
        [HttpDelete]
        public HttpStatusCode Delete(string nik)
        {
            var httpClient = new HttpClient();
            var response = httpClient.DeleteAsync("https://localhost:44338/api/Employee/" + nik).Result;
            return response.StatusCode;
        }
        [HttpPut]
        public HttpStatusCode Update(RegisterVM model)
        {
            var httpClient = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var result = httpClient.PutAsync("https://localhost:44338/api/Employee/", content).Result;
            return result.StatusCode;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
