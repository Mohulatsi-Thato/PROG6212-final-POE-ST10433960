using ClaimsManagementApp.Models;
using ClaimsManagementApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClaimsManagementApp.Controllers
{
    public class LecturerController : Controller
    {
        private readonly IClaimService _claimService;
        private readonly IFileService _fileService;

        public LecturerController(IClaimService claimService, IFileService fileService)
        {
            _claimService = claimService;
            _fileService = fileService;
        }

        public IActionResult Index()
        {
            var claims = _claimService.GetAllClaims();
            return View(claims);
        }

        public IActionResult SubmitClaim()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitClaim(Claim claim, List<IFormFile> documents)
        {
            if (ModelState.IsValid)
            {
                _claimService.AddClaim(claim);

                if (documents != null && documents.Any())
                {
                    foreach (var document in documents)
                    {
                        if (_fileService.ValidateFile(document))
                        {
                            var storedFileName = await _fileService.SaveFileAsync(document, claim.Id.ToString());

                            var supportingDoc = new SupportingDocument
                            {
                                FileName = document.FileName,
                                StoredFileName = storedFileName,
                                ContentType = document.ContentType,
                                FileSize = document.Length
                            };

                            _claimService.AddDocumentToClaim(claim.Id, supportingDoc);
                        }
                    }
                }

                TempData["SuccessMessage"] = "Claim submitted successfully!";
                return RedirectToAction("Index");
            }

            return View(claim);
        }

        public IActionResult TrackClaim(int id)
        {
            var claim = _claimService.GetClaimById(id);
            if (claim == null)
            {
                return NotFound();
            }
            return View(claim);
        }
    }
}