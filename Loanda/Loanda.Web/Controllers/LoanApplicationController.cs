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
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var result = await this.loanApplicationService.GetAllAsync(ct);
            return View("Index", result.ToViewModel());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken ct)
        {
            var loanApplication = await this.loanApplicationService.GetByIdAsync(id, ct);

            if (loanApplication == null)
            {
                return NotFound();
            }

            return View("Details", loanApplication.ToViewModel());
        }

        [HttpPost]
        [Route("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAsync(Guid id, LoanApplicationViewModel model, CancellationToken ct)
        {
            model.Id = id; 
            var loanApplication = await this.loanApplicationService.UpdateAsync(model.ToServiceModel(), ct);

            if (loanApplication is null)
            {
                return NotFound();
            }

            //return View("", loanApplication.ToViewModel());

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LoanApplicationViewModel model, CancellationToken ct)
        {
           /* var loanApplication = */await this.loanApplicationService.AddAsync(model.ToServiceModel(), ct);

            //return CreatedAtAction(nameof(GetByIdAsync), new { id = loanApplication.Id }, loanApplication.ToViewModel());

            return RedirectToAction("Index");
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> RemoveAsync(Guid id, CancellationToken ct)
        {
            await this.loanApplicationService.RemoveAsync(id, ct);

            return NoContent();
        }

        [Route("{id}")]
        public async Task<IActionResult> MarkAsDeletedAsync(Guid id, CancellationToken ct)
        {
            await this.loanApplicationService.MarkAsDeletedAsync(id, ct);

            return NoContent();
        }
    }
}