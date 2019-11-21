using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loanda.Services.Contracts;
using Loanda.Web.Mappings;
using Loanda.Web.Models.Applicant;
using Microsoft.AspNetCore.Authorization;
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