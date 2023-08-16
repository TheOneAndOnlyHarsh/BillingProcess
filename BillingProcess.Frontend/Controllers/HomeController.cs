using BillingProcess.Frontend.Models;
using BillingProcess.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BillingProcess.Frontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var courses = new List<PaymentVM>
         {
            new PaymentVM { Name = "Mess Fee", ImageUrl = "https://www.wemakescholars.com/uploads/blog/TopprofessionalITcoursetopursueincollege.jpg"  },
            new PaymentVM { Name = "Institutional Fee", ImageUrl = "https://www.wemakescholars.com/uploads/blog/TopprofessionalITcoursetopursueincollege.jpg"  },
            new PaymentVM { Name = "Hostel Fee", ImageUrl = "https://www.wemakescholars.com/uploads/blog/TopprofessionalITcoursetopursueincollege.jpg"  },

         };
            return View(courses);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}