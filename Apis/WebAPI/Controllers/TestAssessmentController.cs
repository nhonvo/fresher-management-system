using Application.Commons;
using Application.Interfaces;
using Application.ViewModels.TestAssessmentViewModels;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestAssessmentController : ControllerBase
{ 
    private readonly ITestAssessmentService _testAssessmentService;
    public TestAssessmentController(ITestAssessmentService testAssessmentService)
    {
        _testAssessmentService = testAssessmentService;
    }

    [HttpGet]
    public async Task<Pagination<TestAssessmentViewModel>> GetTestAssessmentPagingsion(int pageIndex = 0, int pageSize = 10)
    {
        return await _testAssessmentService.GetTestAssessmentPagingsionAsync(pageIndex, pageSize);
    }

}
