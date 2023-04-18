using System.Text.Json.Serialization;

namespace API.Models;

public partial class Account
{
    public string Nik { get; set; } = null!;

    public string Password { get; set; } = null!;

    [JsonIgnore]
    public virtual Employee? NikNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<AccountRole>? TbMAccountRoles { get; set; } = new List<AccountRole>();
}
