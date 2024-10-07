using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PH.Core.Domain;
using PH.Services;
using PH.Web.Controllers;
using PH.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace PH.Web.Tests
{
    public class MedicalDocControllerTests
    {
        private readonly Mock<IWebHostEnvironment> _mockEnvironment;
        private readonly Mock<IMedicalDoucmentService> _mockMedicalDocumentService;
        private readonly MedicalDocController _controller;

        public MedicalDocControllerTests()
        {
            _mockEnvironment = new Mock<IWebHostEnvironment>();
            _mockMedicalDocumentService = new Mock<IMedicalDoucmentService>();
            _controller = new MedicalDocController(_mockEnvironment.Object, _mockMedicalDocumentService.Object);
        }

        [Fact]
        public void List_ReturnsPartialViewResult_WithMedicalDocumentViewModel()
        {
            
            var documents = new List<MedicalDocument>
            {
                new MedicalDocument { Id = 1, FileName = "Test1.pdf" },
                new MedicalDocument { Id = 2, FileName = "Test2.pdf" }
            };
            _mockMedicalDocumentService.Setup(s => s.GetAllMedicalDocuments()).Returns(documents);
            var result = _controller.List(1) as PartialViewResult;
            Assert.NotNull(result);
            Assert.Equal("Partials/_Grid", result.ViewName);
            Assert.IsType<MedicalDocumentViewModel>(result.Model);
            var model = result.Model as MedicalDocumentViewModel;
            Assert.Equal(2, model.MedicalDocuments.Count());
            Assert.Equal(1, model.PageNumber);
            Assert.Equal(1, model.TotalPages);
        }

        [Fact]
        public void List_ReturnsPartialViewResult_WhenNoDocuments()
        {
            
            _mockMedicalDocumentService.Setup(s => s.GetAllMedicalDocuments()).Returns(new List<MedicalDocument>());

            var result = _controller.List(1) as PartialViewResult;

            Assert.NotNull(result);
            Assert.Equal("Partials/_Grid", result.ViewName);
            Assert.IsType<MedicalDocumentViewModel>(result.Model);
            var model = result.Model as MedicalDocumentViewModel;
            Assert.Empty(model.MedicalDocuments);
        }

        [Fact]
        public void AddDocument_ReturnsViewResult()
        {
            var result = _controller.AddDocument();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void AddDocument_ReturnsViewResult_WhenModelStateIsInvalid()
        {
            
            var documentModel = new DocumentModel();
            _controller.ModelState.AddModelError("FileName", "Required");
            var result = _controller.AddDocument(documentModel, null) as ViewResult;
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
            Assert.Equal(documentModel, result.Model);
        }
        [Fact]
        public void Delete_ReturnsNotFound_WhenDocumentDoesNotExist()
        {
            
            int id = 1;
            _mockMedicalDocumentService.Setup(s => s.GetById(id)).Returns((MedicalDocument)null);
            var result = _controller.Delete(id);
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
