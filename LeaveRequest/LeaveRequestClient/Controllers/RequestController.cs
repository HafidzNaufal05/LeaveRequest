using LeaveRequest.Models;
using LeaveRequest.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        private object client;

        public IActionResult Index()
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
        public List<Request> GetHistoryRequest()
        {
            var token = HttpContext.Session.GetString("token");


            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = httpClient.GetAsync("https://localhost:44338/api/Request/GetHistoryRequest3").Result;
            
            var apiResponse = response.Content.ReadAsStringAsync().Result;
            List<Request> data = JsonConvert.DeserializeObject<List<Request>>(apiResponse);
            return data;
        }

        [HttpPost]
        public HttpStatusCode SubmitRequest(RequestVM requestVM)
        {
            var httpClient = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(requestVM), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync("https://localhost:44338/api/Request/RequestCuti", content).Result;
            return result.StatusCode;
        }
    }
}
