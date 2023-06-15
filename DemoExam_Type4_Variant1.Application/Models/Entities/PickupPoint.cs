using System.Collections.Generic;

namespace DemoExam_Type4_Variant1.Application.Models.Entities;

public partial class PickupPoint
{
    public int Id { get; set; }

    public string City { get; set; } = null!;

    public string Street { get; set; } = null!;

    public int House { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    /// <returns>This value can be correctly used in ComboBox without converters.</returns>
    public override string ToString() => string.Join('—', City, Street, House);
}
