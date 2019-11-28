using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Loanda.EmailClient.Contracts;
using Loanda.Entities;
using Loanda.Services.Contracts;
using Loanda.Web.Mappers.Contracts;
using Loanda.Web.Mappings;
using Loanda.Web.Models.Email;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Loanda.Web.Controllers
{
    [Authorize]
    public class EmailController : Controller
    {
        private readonly IEmailService emailService;
        private readonly IMapper<ReceivedEmailEntity, EmailViewModel> emailMapper;
        private readonly IGmailApi gmailApi;
        private readonly ILoanApplicationService loanApplicationService;
        private readonly UserManager<User> userManager;


        public EmailController(IEmailService emailService, IMapper<ReceivedEmailEntity, EmailViewModel> emailMapper, IGmailApi gmailApi, ILoanApplicationService loanApplicationService, UserManager<User> userManager)
        {
            this.emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            this.emailMapper = emailMapper ?? throw new ArgumentNullException(nameof(emailMapper));
            this.gmailApi = gmailApi ?? throw new ArgumentNullException(nameof(gmailApi));
            this.loanApplicationService = loanApplicationService ?? throw new ArgumentNullException(nameof(loanApplicationService));
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Invalid(CancellationToken cancellationToken)
        {
            var user = userManager.GetUserAsync(User);
            if (user.Result.IsFirstLogin)
            {
                return View("~/Views/Home/ChangePassword.cshtml");
            }

            var result = await this.emailService.GetAllInvalidAsync(cancellationToken);
            return View("Invalid", result.ToViewModel());
        }


        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken, string search = null)
        {
            var user = userManager.GetUserAsync(User);
            if (user.Result.IsFirstLogin)
            {
                return View("~/Views/Home/ChangePassword.cshtml");
            }

            if (!string.IsNullOrEmpty(search))
            {
                var searchResult = await this.emailService.Search(search, cancellationToken);
                return View("Index", searchResult.ToViewModel());
            }

            var result = await this.emailService.GetAllNotReviewedAsync(cancellationToken);
            return View("Index", result.ToViewModel());
        }

        [HttpGet]
        public async Task<IActionResult> New(CancellationToken cancellationToken)
        {
            var user = userManager.GetUserAsync(User);
            if (user.Result.IsFirstLogin)
            {
                return View("~/Views/Home/ChangePassword.cshtml");
            }

            var result = await this.emailService.GetAllNewAsync(cancellationToken);
            return View("New", result.ToViewModel());
        }

        [HttpGet]
        public async Task<IActionResult> Open(CancellationToken cancellationToken)
        {
            var user = userManager.GetUserAsync(User);
            if (user.Result.IsFirstLogin)
            {
                return View("~/Views/Home/ChangePassword.cshtml");
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await this.emailService.GetAllOpenAsync(userId,cancellationToken);
            return View("Open", result.ToViewModel());
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

        public async Task<IActionResult> MarkNew(EmailViewModel emailViewModel, CancellationToken cancellationToken)
        {
            try
            {
                await this.emailService.ChangeEmailStatusToNewAsync(emailViewModel.Id, cancellationToken);
                await this.loanApplicationService.RemoveAsync(emailViewModel.Id, cancellationToken);
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
            return  PartialView("_EmailDetails");
        }
    }
}