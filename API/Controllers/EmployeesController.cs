using API.Base;
using API.Models;
using API.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class EmployeesController : GeneralController<IEmployeeRepository, Employee, string>
{
    public EmployeesController(IEmployeeRepository repository) : base(repository) { }
}
