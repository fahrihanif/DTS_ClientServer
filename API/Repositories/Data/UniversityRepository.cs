﻿using API.Contexts;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Contracts;

public class UniversityRepository : GeneralRepository<University, int, MyContext>, IUniversityRepository
{
    public UniversityRepository(MyContext context) : base(context) { }
    public async Task<bool> IsNameExist(string name)
    {
        var entity = await _context.Set<University>().FirstOrDefaultAsync(x => x.Name == name);
        return entity != null;
    }
}