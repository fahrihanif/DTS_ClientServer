using API.Contexts;
using API.Models;
using API.ViewModels;

namespace API.Repositories.Contracts;

public class AccountRepository : GeneralRepository<Account, string, MyContext>, IAccountRepository
{
    private readonly IUniversityRepository _universityRepository;
    private readonly IEducationRepository _educationRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IProfilingRepository _profilingRepository;
    private readonly IAccountRoleRepository _accountRoleRepository;

    public AccountRepository(
        MyContext context,
        IUniversityRepository universityRepository,
        IEducationRepository educationRepository,
        IEmployeeRepository employeeRepository,
        IProfilingRepository profilingRepository,
        IAccountRoleRepository accountRoleRepository) : base(context)
    {
        _universityRepository = universityRepository;
        _educationRepository = educationRepository;
        _employeeRepository = employeeRepository;
        _profilingRepository = profilingRepository;
        _accountRoleRepository = accountRoleRepository;
    }

    public async Task RegisterAsync(RegisterVM registerVM)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try {
            var university = _universityRepository.InsertAsync(new University {
                Name = registerVM.UniversityName
            });

            var education = await _educationRepository.InsertAsync(new Education {
                Major = registerVM.Major,
                Degree = registerVM.Degree,
                Gpa = registerVM.GPA,
                UniversityId = university.Id,
            });

            var employee = await _employeeRepository.InsertAsync(new Employee {
                Nik = registerVM.NIK,
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                BirthDate = registerVM.BirthDate,
                Email = registerVM.Email,
                PhoneNumber = registerVM.PhoneNumber,
                Gender = registerVM.Gender,
                HiringDate = DateTime.Now
            });

            await InsertAsync(new Account {
                Nik = employee!.Nik,
                Password = registerVM.Password
            });

            await _profilingRepository.InsertAsync(new Profiling {
                Id = employee.Nik,
                EducationId = education!.Id
            });

            await _accountRoleRepository.InsertAsync(new AccountRole {
                EmployeeNik = registerVM.NIK,
                RoleId = 1
            });

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