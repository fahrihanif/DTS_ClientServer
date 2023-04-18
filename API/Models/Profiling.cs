using System.Text.Json.Serialization;

namespace API.Models;

public partial class Profiling
{
    public string Id { get; set; } = null!;

    public int EducationId { get; set; }

    [JsonIgnore]
    public virtual Education? Education { get; set; } = null!;
    [JsonIgnore]
    public virtual Employee? IdNavigation { get; set; } = null!;
}
