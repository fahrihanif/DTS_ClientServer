using API.Contexts;
using API.Models;
using API.Repositories.Contracts;
using API.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Data;

public class EmployeeRepository : GeneralRepository<Employee, string, MyContext>, IEmployeeRepository
{
    public EmployeeRepository(MyContext context) : base(context) { }
    public async Task<UserDataVM> GetUserDataByEmailAsync(string email)
    {
        var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Email == email);
        return new UserDataVM {
            Nik = employee!.Nik,
            Email = employee.Email,
            FullName = string.Concat(employee.FirstName, " ", employee.LastName)
        };
    }
}
