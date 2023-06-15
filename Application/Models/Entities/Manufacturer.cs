using System;
using System.Collections.Generic;

namespace DemoExam_Type4_Variant1.Application.Models.Entities;

public partial class Manufacturer
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
