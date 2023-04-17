using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Profiling
{
    public string Id { get; set; } = null!;

    public int EducationId { get; set; }

    public virtual Education Education { get; set; } = null!;

    public virtual Employee IdNavigation { get; set; } = null!;
}
