using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Account
{
    public string Nik { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual Employee NikNavigation { get; set; } = null!;

    public virtual ICollection<AccountRole> TbMAccountRoles { get; set; } = new List<AccountRole>();
}
