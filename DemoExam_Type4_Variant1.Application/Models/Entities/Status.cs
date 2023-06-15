using System;
using System.Collections.Generic;

namespace DemoExam_Type4_Variant1.Application.Models.Entities;

public partial class Status
{
    public const int DefaultStatusId = 1;

    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
