using API.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;
//https://domain/api/universities
[Route("api/[controller]")]
[ApiController]
public class UniversitiesController : ControllerBase
{
    private readonly IUniversityRepository _universityRepository;

    public UniversitiesController(IUniversityRepository universityRepository)
    {
        _universityRepository = universityRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var results = await _universityRepository.GetAllAsync();
        if (results == null) {
            return NotFound(new {
                code = StatusCodes.Status404NotFound,
                status = HttpStatusCode.NotFound.ToString(),
                data = new {
                    message = "Data Not Found!"
                }
            });
        }

        return Ok(new {
            code = StatusCodes.Status200OK,
            status = HttpStatusCode.OK.ToString(),
            data = new {
                message = "Insert Success"
            }
        });
    }
}
