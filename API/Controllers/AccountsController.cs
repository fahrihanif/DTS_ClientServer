using API.Base;
using API.Handlers;
using API.Models;
using API.Repositories.Contracts;
using API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AccountsController : GeneralController<IAccountRepository, Account, string>
{
    private readonly ITokenService _tokenService;
    private readonly IAccountRoleRepository _accountRoleRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public AccountsController(
        IAccountRepository repository,
        ITokenService tokenService,
        IAccountRoleRepository accountRoleRepository,
        IEmployeeRepository employeeRepository) : base(repository)
    {
        _tokenService = tokenService;
        _accountRoleRepository = accountRoleRepository;
        _employeeRepository = employeeRepository;
    }

    [AllowAnonymous]
    [HttpPost("Auth")]
    public async Task<ActionResult<Account>> LoginAsync(LoginVM loginVM)
    {
        try {
            var result = await _repository.LoginAsync(loginVM);
            if (!result) {
                return NotFound();
            }

            var userdata = await _employeeRepository.GetUserDataByEmailAsync(loginVM.Email);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, userdata.Email),
                new Claim(ClaimTypes.Name, userdata.Email),
                new Claim(ClaimTypes.NameIdentifier, userdata.FullName),
                new Claim("NIK", userdata.Nik)
            };

            var getRoles = await _accountRoleRepository.GetRolesByNikAsync(userdata.Nik);

            foreach (var item in getRoles) {
                claims.Add(new Claim(ClaimTypes.Role, item));
            }

            var accessToken = _tokenService.GenerateAccessToken(claims);
            //var refreshToken = _tokenService.GenerateRefreshToken();

            //await _repository.UpdateToken(userdata.Email, refreshToken, DateTime.Now.AddDays(1)); // Token will expired in a day

            return Ok(accessToken);
        } catch {
            return StatusCode(StatusCodes.Status500InternalServerError,
                              new {
                                  Code = StatusCodes.Status500InternalServerError,
                                  Status = "Internal Server Error",
                                  Errors = new {
                                      Message = "Invalid Salt Version"
                                  },
                              });
        }
    }

    [HttpPost("Register")]
    public async Task<ActionResult<Account>> RegisterAsync(RegisterVM registerVM)
    {
        try {
            await _repository.RegisterAsync(registerVM);
            return Ok();
        } catch {
            return BadRequest();
        }
    }
}
