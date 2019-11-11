using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Loanda.Web.Models;
using Loanda.EmailClient.Contracts;

namespace Loanda.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGmailApi gmailApi;

        public HomeController(IGmailApi gmailApi)
        {
            this.gmailApi = gmailApi;
        }

        public IActionResult Index()
        {
            this.gmailApi.GetEmailsFromGmail();

            return View();
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
