using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestAssessmentController : CustomBaseController
{
    private readonly ITestAssessmentService _testAssessmentService;
    public TestAssessmentController(ITestAssessmentService testAssessmentService)
    {
        _testAssessmentService = testAssessmentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTestAssessmentPagingsion(int pageIndex = 0, int pageSize = 10)
    {
        try
        {
            var result = await _testAssessmentService.GetTestAssessmentPagingsionAsync(pageIndex, pageSize);
            return CustomResult(result);
        }
        catch (Exception ex)
        {
            return CustomResult(ex.Message, HttpStatusCode.BadRequest);
        }
    }

}
