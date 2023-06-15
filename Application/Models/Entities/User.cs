using System.Collections.Generic;

namespace DemoExam_Type4_Variant1.Application.Models.Entities;

public partial class User
{
    public int Id { get; set; }

    public int UserRoleId { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string? Patronymic { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Role UserRole { get; set; } = null!;

    public string FullName
    {
        get
        {
            var fullName = $"{Surname} {Name[0]}.";
            if (Patronymic != null)
                fullName += $" {Patronymic[0]}.";

            return fullName;
        }
    }
}
