using API.Models;

namespace API.Repositories.Contracts;

public interface IUniversityRepository : IGeneralRepository<University, int>
{
    Task<University?> GetByNameAsync(string name);
    Task<bool> IsNameExistAsync(string name);
}
