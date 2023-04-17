using API.Contexts;
using API.Models;
using API.Repositories.Contracts;

namespace API.Repositories.Data;

public class AccountRoleRepository : GeneralRepository<AccountRole, int, MyContext>, IAccountRoleRepository
{
    private readonly IRoleRepository _roleRepository;

    public AccountRoleRepository(
        MyContext context,
        IRoleRepository roleRepository) : base(context)
    {
        _roleRepository = roleRepository;
    }

    public async Task<IEnumerable<string>> GetRolesByNikAsync(string nik)
    {
        var getAccountRoleByAccountNik = GetAllAsync().Result.Where(x => x.EmployeeNik == nik);
        var getRole = await _roleRepository.GetAllAsync();

        var getRoleByNik = from ar in getAccountRoleByAccountNik
                           join r in getRole on ar.RoleId equals r.Id
                           select r.Name;

        return getRoleByNik;
    }
}
