using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Loanda.Services.Contracts;
using Loanda.Web.Mappings;
using Loanda.Web.Models.Applicant;
using Loanda.Web.Models.LoanApplication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Loanda.Web.Controllers
{
    [Authorize]
    public class ApplicantController : Controller
    {
        private readonly IApplicantService applicantService;

        public ApplicantController(IApplicantService applicantService)
        {
            this.applicantService = applicantService ?? throw new ArgumentNullException(nameof(applicantService));
        }

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var result = await this.applicantService.GetAllAsync(cancellationToken);
            return View("Index", result.ToViewModel());
        }

        [TempData]
        public string ErrorMessage { get; set; }

        [HttpGet]
        public IActionResult Create()
        {
            //var bookViewModel = new BookViewModel();

            //return View(bookViewModel);

            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApplicantViewModel model, CancellationToken cancellationToken)
        {
            var applicant = await this.applicantService.AddAsync(model.ToServiceModel(), cancellationToken);

            //return CreatedAtAction(nameof(GetByIdAsync), new { id = loanApplication.Id }, loanApplication.ToViewModel());
            //model.Id = applicant.Id;

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var name = HttpContext.User.Identity.Name;

            // TODO service get user by email/username

            var loanViewModel = new LoanApplicationViewModel { ApplicantId = applicant.Id, OpenedById = userId, EmailId = model.EmailId };
            return PartialView("_CreateLoanPartial", loanViewModel);
        }


        [HttpGet]
        public IActionResult Edit()
        {
            //var bookViewModel = new BookViewModel();

            //return View(bookViewModel);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(/*Guid id, */ApplicantViewModel model, CancellationToken cancellationToken)
        {
            //model.Id = id;
            var applicant = await this.applicantService.UpdateAsync(model.ToServiceModel(), cancellationToken);

            //if (applicant is null)
            //{
            //    return NotFound();
            //}

            //return View("", loanApplication.ToViewModel());

            var name = HttpContext.User.Identity.Name;

            // get logged user id
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


            var loanViewModel = new LoanApplicationViewModel { ApplicantId = applicant.Id, OpenedById = userId, EmailId = model.EmailId };

            return PartialView("_CreateLoanPartial", loanViewModel);
        }

        [HttpGet]
        public IActionResult CheckApplicant(long emailId)
        {
            var egnViewModel = new EGNViewModel { EmialId = emailId };
            return PartialView("_EgnPartial", egnViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CheckApplicant(EGNViewModel vm, CancellationToken cancellationToken)
        {
            var applicant = await this.applicantService.GetByEgnAsync(vm.EGN, cancellationToken);

            if (applicant == null)
            {
                return PartialView("_CreateApplicantPartial");
            }
            else if (applicant != null)
            {
                return PartialView("_EditApplicantPartial", applicant.ToViewModel());
            }
            else
            {
                return NotFound();
            }
        }
        //[HttpPost]
        //public async Task<IActionResult> Test(string egn, string id)
        //{
        //    var stop = 0;
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(ApplicantViewModel applicantViewModel, CancellationToken cancellationToken)
        //{
        //    //if (!this.ModelState.IsValid)
        //    //{
        //    //    //throw new ArgumentException("Invalid input!");
        //    //    return this.View();
        //    //}

        //    await this.applicantService.AddAsync(applicantViewModel.ToServiceModel(), cancellationToken);

        //    return RedirectToAction(nameof(Index));
        //}

        //[HttpPost]
        //[Route("")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(LoanApplicationViewModel model, CancellationToken cancellationToken)
        //{
        //    /* var loanApplication = */
        //    await this.loanApplicationService.AddAsync(model.ToServiceModel(), cancellationToken);

        //    //return CreatedAtAction(nameof(GetByIdAsync), new { id = loanApplication.Id }, loanApplication.ToViewModel());

        //    return RedirectToAction("Index");
        //}
    }
}