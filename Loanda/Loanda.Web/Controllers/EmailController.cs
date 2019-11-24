using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Loanda.Entities;
using Loanda.Services.Contracts;
using Loanda.Web.Mappers.Contracts;
using Loanda.Web.Mappings;
using Loanda.Web.Models.Email;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Loanda.Web.Controllers
{
    [Authorize]
    public class EmailController : Controller
    {
        private readonly IEmailService emailService;
        private readonly IMapper<ReceivedEmailEntity, EmailViewModel> emailMapper;

        public EmailController(IEmailService emailService, IMapper<ReceivedEmailEntity, EmailViewModel> emailMapper)
        {
            this.emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            this.emailMapper = emailMapper ?? throw new ArgumentNullException(nameof(emailMapper));
        }

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var result = await this.emailService.GetAllAsync(cancellationToken);
            return View("Index", result.ToViewModel());
        }


        //[HttpGet]
        //[Authorize]
        //public async Task<IActionResult> Index(CancellationToken ct)
        //{
        //    var emails = await this.emailService.GetAllAsync();

        //    //if (authors.Count() == 0)
        //    //{
        //    //    return View("Empty");
        //    //}

        //    var emailsViewModelList = new List<EmailViewModel>();
        //    foreach (var email in emails)
        //    {
        //        emailsViewModelList.Add(this.emailMapper.Map(email));
        //    }

        //    return View(emailsViewModelList);
        //}

        //[HttpGet("/details")]
        //[Authorize]
        //public async Task<IActionResult> Details(Guid id)
        //{
        //    var email = await this.emailService.FindByIdAsync(id);

        //    var emailViewModel = this.emailMapper.Map(email);


        //    // TODO Get the user processing the received email
        //    //var loggedUser = User.Identity.Name;

        //    //var user = await this.userService.GetUserByUsername(loggedUser);

        //    //emailViewModel.UserId = user.Id;

        //    return View(emailViewModel);
        //}

        [HttpGet("/details")]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Details(long id, CancellationToken cancellationToken)
        {
            var email = await this.emailService.FindByIdAsync(id, cancellationToken);

            if (email == null)
            {
                return NotFound();
            }

            return View("Details", email.ToViewModel());
        }

        public async Task<IActionResult> Edit()
        {
            return PartialView("_EmailDetails");
        }
    }
}