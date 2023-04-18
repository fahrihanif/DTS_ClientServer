using API.Models;
using API.ViewModels;

namespace API.Repositories.Contracts;

public interface IEmployeeRepository : IGeneralRepository<Employee, string>
{
    Task<UserDataVM> GetUserDataByEmailAsync(string email);
}
