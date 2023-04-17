using API.Contexts;
using API.Models;
using API.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Data;

public class EmployeeRepository : GeneralRepository<Employee, string, MyContext>, IEmployeeRepository
{
    public EmployeeRepository(MyContext context) : base(context) { }
    public async Task<string> GetFullNameByEmailAsync(string email)
    {
        var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Email == email);
        return employee is null ? string.Empty : string.Concat(employee.FirstName, " ", employee.LastName);
    }
}
