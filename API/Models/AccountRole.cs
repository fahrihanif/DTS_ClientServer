using System;
using System.Collections.Generic;

namespace API.Models;

public partial class AccountRole
{
    public int Id { get; set; }

    public string EmployeeNik { get; set; } = null!;

    public int RoleId { get; set; }

    public virtual Account EmployeeNikNavigation { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
