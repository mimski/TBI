using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Loanda.EmailClient.Contracts;
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
        private readonly IGmailApi gmailApi;



        public EmailController(IEmailService emailService, IMapper<ReceivedEmailEntity, EmailViewModel> emailMapper, IGmailApi gmailApi)
        {
            this.emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            this.emailMapper = emailMapper ?? throw new ArgumentNullException(nameof(emailMapper));
            this.gmailApi = gmailApi ?? throw new ArgumentNullException(nameof(gmailApi));
        }

        [HttpGet]
        public async Task<IActionResult> Invalid(CancellationToken cancellationToken)
        {
            var result = await this.emailService.GetAllInvalidAsync(cancellationToken);
            return View("Invalid", result.ToViewModel());
        }


        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var result = await this.emailService.GetAllNotReviewedAsync(cancellationToken);
            return View("Index", result.ToViewModel());
        }

        [HttpGet]
        public async Task<IActionResult> New(CancellationToken cancellationToken)
        {
            var result = await this.emailService.GetAllNewAsync(cancellationToken);
            return View("New", result.ToViewModel());
        }


        public async Task<IActionResult> MarkInvalid(EmailViewModel emailViewModel, CancellationToken cancellationToken)
        {
            try
            {
                await this.emailService.MarkInvalidAsync(emailViewModel.ToServiceModel(), cancellationToken);
            }
            catch (Exception)
            {
                //return NoContent();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> MarkNotReviewed(EmailViewModel emailViewModel, CancellationToken cancellationToken)
        {

            try
            {
                await this.gmailApi.GetEmailByGmailId(emailViewModel.Id, cancellationToken); 
                //await this.emailService.MarkNotReviewedAsync(emailViewModel.ToServiceModel(), cancellationToken);
            }
            catch (Exception)
            {
                //return NoContent();
            }

            return RedirectToAction(nameof(Invalid));
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

            await this.emailService.ChangeEmailStatusToNewAsync(id, cancellationToken);


            return View("Details", email.ToViewModel());
        }

        public async Task<IActionResult> Edit()
        {
            return PartialView("_EmailDetails");
        }
    }
}