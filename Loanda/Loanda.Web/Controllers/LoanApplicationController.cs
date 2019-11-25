using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Loanda.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Loanda.Web.Mappings;
using System.Threading;
using Loanda.Web.Models.LoanApplication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Loanda.Web.Controllers
{
    [Authorize]
    public class LoanApplicationController : Controller
    {
        private readonly ILoanApplicationService loanApplicationService;

        public LoanApplicationController(ILoanApplicationService loanApplicationService)
        {
            this.loanApplicationService = loanApplicationService ?? throw new ArgumentNullException(nameof(loanApplicationService));
        }

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var result = await this.loanApplicationService.GetAllAsync(cancellationToken);
            return View("Index", result.ToViewModel());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var loanApplication = await this.loanApplicationService.GetByIdAsync(id, cancellationToken);

            if (loanApplication == null)
            {
                return NotFound();
            }

            return View("Details", loanApplication.ToViewModel());
        }

        [HttpPost]
        [Route("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAsync(Guid id, LoanApplicationViewModel model, CancellationToken cancellationToken)
        {
            model.Id = id; 
            var loanApplication = await this.loanApplicationService.UpdateAsync(model.ToServiceModel(), cancellationToken);

            if (loanApplication is null)
            {
                return NotFound();
            }

            //return View("", loanApplication.ToViewModel());

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LoanApplicationViewModel model, CancellationToken cancellationToken)
        {
            var currentOperatorId = this.User.FindFirst(u => u.Type == ClaimTypes.NameIdentifier);
            //model.OpenedById = currentOperatorId; 
            var loanApplication = await this.loanApplicationService.AddAsync(model.ToServiceModel(), cancellationToken);

            //return CreatedAtAction(nameof(GetByIdAsync), new { id = loanApplication.Id }, loanApplication.ToViewModel());

            return RedirectToAction("Index");
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> RemoveAsync(Guid id, CancellationToken cancellationToken)
        {
            await this.loanApplicationService.RemoveAsync(id, cancellationToken);

            return NoContent();
        }

        [Route("{id}")]
        public async Task<IActionResult> MarkAsDeletedAsync(Guid id, CancellationToken cancellationToken)
        {
            await this.loanApplicationService.MarkAsDeletedAsync(id, cancellationToken);

            return NoContent();
        }
    }
}