using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PH.Services;
using PH.Web.Models;
using PH.Web.Framework;
using System;
using PH.Core.Domain;

namespace PH.Web.Controllers
{
    public class LabResultController : Controller
    {
        private const int PageSize = 5;
        private readonly ILabResultService _labResultService;
        private readonly ILogger<LabResultController> _logger;

        public LabResultController(ILabResultService labResultService, ILogger<LabResultController> logger)
        {
            _labResultService = labResultService;
            _logger = logger;
        }

        public IActionResult List(int? page)
        {
            try
            {
                var pageNumber = page ?? 1;
                _logger.LogInformation($"Fetching lab results for page {pageNumber}");

                var labResults = _labResultService.GetAllLabResults();
                var viewModel = new LabResultViewModel
                {
                    LabResults = labResults.OrderByDescending(lr => lr.Id)
                        .Skip((pageNumber - 1) * PageSize)
                        .Take(PageSize)
                        .ToList().ToViewModelList<LabResult, ResultModel>(),
                    PageNumber = pageNumber,
                    TotalPages = (int)Math.Ceiling((double)labResults.Count / PageSize)
                };

                return PartialView("Partials/_Grid", viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching lab results.");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        [HttpGet]
        public IActionResult AddResult()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while loading AddResult view.");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        [HttpPost]
        public IActionResult AddResult(ResultModel resultModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _labResultService.InsertLabResult(resultModel.ToEntity<LabResult, ResultModel>());
                    return RedirectToAction("Index", "MedicalData");
                }
                return View(resultModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding lab result.");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogWarning("Invalid lab result id: 0.");
                    return NotFound();
                }

                var labResult = _labResultService.GetById(id);
                if (labResult == null)
                {
                    _logger.LogWarning($"Lab result with id {id} not found.");
                    return NotFound();
                }

                _labResultService.DeleteLabResult(labResult);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting lab result with id {id}.");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }
    }
}