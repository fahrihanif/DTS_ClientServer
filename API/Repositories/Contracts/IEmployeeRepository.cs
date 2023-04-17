using API.Models;

namespace API.Repositories.Contracts;

public interface IEmployeeRepository : IGeneralRepository<Employee, string>
{
    Task<string> GetFullNameByEmailAsync(string email);
}
