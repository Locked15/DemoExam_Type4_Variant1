using System;
using System.Collections.Generic;
using System.IO;

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

    public sbyte SafeDiscount
    {
        get => Discount ?? 0;
    }

    public string SafeDiscountString
    {
        get => $"{SafeDiscount}%";
    }

    public decimal FinalCost
    {
        get
        {
            var discountAmount = Cost * (SafeDiscount / 100M);
            return Cost - discountAmount;
        }
    }

    public string SafeImagePath
    {
        get
        {
            // First '\\' to make path relational on executable-level folder (not current component folder).
            var imagePath = Path.Combine("Assets", "Products", Image ?? string.Empty);
            return $"\\{imagePath}";
        }
    }
}
