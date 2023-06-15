using System;
using System.Collections.Generic;

namespace DemoExam_Type4_Variant1.Application.Models.Entities;

public partial class Product
{
    public int Id { get; set; }

    public int ManufacturerId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Image { get; set; }

    public decimal Cost { get; set; }

    public sbyte? Discount { get; set; }

    public int Amount { get; set; }

    public virtual Manufacturer Manufacturer { get; set; } = null!;

    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

    public decimal FinalCost
    {
        get
        {
            var discountAmount = Cost * ((Discount ?? 0) / 100M);
            return Cost - discountAmount;
        }
    }
}
