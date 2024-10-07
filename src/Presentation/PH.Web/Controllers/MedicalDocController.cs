using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PH.Core.Domain;
using PH.Services;
using PH.Web.Framework;
using PH.Web.Models;
using System;
using System.IO;
using System.Linq;

public class MedicalDocController : Controller
{
    private const int PageSize = 6;
    private readonly IWebHostEnvironment _environment;
    private readonly IMedicalDoucmentService _medicalDocumentService;

    public MedicalDocController(IWebHostEnvironment environment, IMedicalDoucmentService medicalDocumentService)
    {
        _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        _medicalDocumentService = medicalDocumentService ?? throw new ArgumentNullException(nameof(medicalDocumentService));
    }

    public IActionResult List(int? page)
    {
        try
        {
            var pageNumber = page ?? 1;
            var documents = _medicalDocumentService.GetAllMedicalDocuments();

            if (documents == null || !documents.Any())
            {
                return PartialView("Partials/_Grid", new MedicalDocumentViewModel());  
            }

            var viewModel = new MedicalDocumentViewModel
            {
                MedicalDocuments = documents.OrderByDescending(md => md.Id)
                                            .Skip((pageNumber - 1) * PageSize)
                                            .Take(PageSize)
                                            .ToList().ToViewModelList<MedicalDocument, DocumentModel>(),
                PageNumber = pageNumber,
                TotalPages = (int)Math.Ceiling((double)documents.Count / PageSize)
            };

            return PartialView("Partials/_Grid", viewModel);
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while fetching the medical documents.");
        }
    }

    public IActionResult AddDocument()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddDocument([Bind("FileName,Description")] DocumentModel documentModel, IFormFile file)
    {
        try
        {
            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("File", "Please upload a valid file.");
                return View(documentModel);
            }

            string fileExtension = Path.GetExtension(file.FileName);
            string uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder); 
            }

            string uniqueFileName = $"{Guid.NewGuid()}_{DateTime.Now.Ticks}{fileExtension}";
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            
            documentModel.FilePath = $"/uploads/{uniqueFileName}";
            documentModel.FileType = fileExtension;
            documentModel.DateAdded = DateTime.Now;

            _medicalDocumentService.InsertMedicalDocument(documentModel.ToEntity<MedicalDocument, DocumentModel>());

            return Json(new { success = true });
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while adding the document.");
        }
    }

    [HttpDelete]
    public IActionResult Delete(int id)
    {
        try
        {
            if (id == 0)
            {
                return NotFound();
            }

            var document = _medicalDocumentService.GetById(id);

            if (document == null)
            {
                return NotFound();
            }

            _medicalDocumentService.DeleteMedicalDocument(document);

            return Json(new { success = true });
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while deleting the document.");
        }
    }
}