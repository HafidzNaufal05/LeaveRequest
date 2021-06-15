using LeaveRequest.Models;
using LeaveRequest.ViewModels;
using LeaveRequestClient.Models;
using Microsoft.AspNetCore.Http;
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
    public class EmployeeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public EmployeeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("SummaryDashboard")]
        public IActionResult Dashboard()
        {
            string id = HttpContext.Session.GetString("nik");
            ViewBag.Role = HttpContext.Session.GetString("role");
            if (ViewBag.Role == "Employee" || ViewBag.Role == "Manager" || ViewBag.Role == "HR")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
