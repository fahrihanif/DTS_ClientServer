using API.Base;
using API.Models;
using API.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
//https://domain/api/universities
[Route("api/[controller]")]
[ApiController]
public class UniversitiesController : GeneralController<IUniversityRepository, University, int>
{
    public UniversitiesController(IUniversityRepository repository) : base(repository) { }
}
