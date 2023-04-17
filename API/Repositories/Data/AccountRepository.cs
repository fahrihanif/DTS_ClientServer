using API.Contexts;
using API.Models;
using API.ViewModels;

namespace API.Repositories.Contracts;

public class AccountRepository : GeneralRepository<Account, string, MyContext>, IAccountRepository
{
    private readonly IUniversityRepository _universityRepository;
    private readonly IEducationRepository _educationRepository;
    private readonly IEmployeeRepository _employeeRepository;
    public AccountRepository(
        MyContext context,
        IUniversityRepository universityRepository,
        IEducationRepository educationRepository,
        IEmployeeRepository employeeRepository
        ) : base(context)
    {
        _universityRepository = universityRepository;
        _educationRepository = educationRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task RegisterAsync(RegisterVM registerVM)
    {
        using var transaction = _context.Database.BeginTransaction();
        try {

            var university = new University {
                Name = registerVM.UniversityName
            };
            if (await _universityRepository.IsNameExist(registerVM.UniversityName)) {

            } else {
                await _universityRepository.InsertAsync(university);
            }

            var education = new Education {
                Major = registerVM.Major,
                Degree = registerVM.Degree,
                Gpa = registerVM.GPA,
                UniversityId = university.Id,
            };
            await _educationRepository.InsertAsync(education);

            // Employee
            // Account
            // Profiling
            // AccountRole

            await transaction.CommitAsync();
        } catch {
            await transaction.RollbackAsync();
        }
    }

    public async Task<bool> LoginAsync(LoginVM loginVM)
    {
        var getEmployees = await _employeeRepository.GetAllAsync();
        var getAccounts = await GetAllAsync();

        var getUserData = getEmployees.Join(getAccounts,
                                            e => e.Nik,
                                            a => a.Nik,
                                            (e, a) => new LoginVM {
                                                Email = e.Email,
                                                Password = a.Password
                                            })
                                      .FirstOrDefault(ud => ud.Email == loginVM.Email);

        return getUserData is not null && loginVM.Password == getUserData.Password;
    }
}