using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Loanda.Entities;
using Loanda.Services.Contracts;
using Loanda.Services.DTOs;
using Loanda.Web.Mappers.Contracts;
using Loanda.Web.Models.Email;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Loanda.Web.Controllers
{
    public class EmailController : Controller
    {
        private readonly IEmailService emailService;
        private readonly IMapper<ReceivedEmail, EmailViewModel> emailMapper;

        public EmailController(IEmailService emailService, IMapper<ReceivedEmail, EmailViewModel> emailMapper)
        {
            this.emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            this.emailMapper = emailMapper ?? throw new ArgumentNullException(nameof(emailMapper));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var emails = await this.emailService.GetAllAsync();

            //if (authors.Count() == 0)
            //{
            //    return View("Empty");
            //}

            var emailsViewModelList = new List<EmailViewModel>();
            foreach (var email in emails)
            {
                emailsViewModelList.Add(this.emailMapper.Map(email));
            }

            return View(emailsViewModelList);
        }
    }
}