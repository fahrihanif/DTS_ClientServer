using API.Contexts;
using API.Models;

namespace API.Repositories.Contracts;

public class EducationRepository : GeneralRepository<Education, int, MyContext>, IEducationRepository
{
    public EducationRepository(MyContext context) : base(context) { }
}
