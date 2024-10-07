using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PH.Services;
using PH.Web;
using PH.Web.Models;
using PH.Web.Controllers;
using Xunit;
using PH.Core.Domain;

namespace PH.WebTests.Controllers;

[TestSubject(typeof(LabResultController))]
public class LabResultControllerTest
{
    [Fact]
    public void List_ReturnsPartialViewWithLabResultViewModel_WhenCalledWithValidPageNumber()
    {
        var mockLabResultService = new Mock<ILabResultService>();
        var mockLogger = new Mock<ILogger<LabResultController>>();
        var controller = new LabResultController(mockLabResultService.Object, mockLogger.Object);

        var labResults = new List<LabResult>
        {
            new LabResult { Id = 1, ResultValue = "Result 1" },
            new LabResult { Id = 2, ResultValue = "Result 2" },
            new LabResult { Id = 3, ResultValue = "Result 3" }
        };
        mockLabResultService.Setup(s => s.GetAllLabResults()).Returns(labResults);
        var result = controller.List(1) as PartialViewResult;
        var viewModel = result?.Model as LabResultViewModel;
        Assert.IsType<PartialViewResult>(result);
        Assert.Equal("Partials/_Grid", result.ViewName);
        Assert.NotNull(viewModel);
        Assert.Equal(3, viewModel.LabResults.Count);
        Assert.Equal(1, viewModel.PageNumber);
        Assert.Equal(1, viewModel.TotalPages);
    }

    [Fact]
    public void AddResult_ReturnsView_WhenNoException()
    {
        
        var mockLabResultService = new Mock<ILabResultService>();
        var mockLogger = new Mock<ILogger<LabResultController>>();
        var controller = new LabResultController(mockLabResultService.Object, mockLogger.Object);

        
        var result = controller.AddResult() as ViewResult;

        
        Assert.NotNull(result);
        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public void AddResult_Post_RedirectsToIndex_WhenModelStateIsValid()
    {
        
        var mockLabResultService = new Mock<ILabResultService>();
        var mockLogger = new Mock<ILogger<LabResultController>>();
        var controller = new LabResultController(mockLabResultService.Object, mockLogger.Object);

        var resultModel = new ResultModel();
        controller.ModelState.Clear(); 

        
        var result = controller.AddResult(resultModel) as RedirectToActionResult;

        
        Assert.NotNull(result);
        Assert.Equal("Index", result.ActionName);
        Assert.Equal("MedicalData", result.ControllerName);
    }

    [Fact]
    public void AddResult_Post_ReturnsView_WhenModelStateIsInvalid()
    {
        
        var mockLabResultService = new Mock<ILabResultService>();
        var mockLogger = new Mock<ILogger<LabResultController>>();
        var controller = new LabResultController(mockLabResultService.Object, mockLogger.Object);

        controller.ModelState.AddModelError("Name", "Required"); 
        var resultModel = new ResultModel(); 

        
        var result = controller.AddResult(resultModel) as ViewResult;

        
        Assert.NotNull(result);
        Assert.IsType<ViewResult>(result);
        Assert.Equal(resultModel, result.Model);
    }
}