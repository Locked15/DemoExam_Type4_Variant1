using System.Collections.Generic;
using System.Linq;

namespace DemoExam_Type4_Variant1.Application.Models.Entities;

public partial class Order
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int StatusId { get; set; }

    public int PickupPointId { get; set; }

    public int TakeCode { get; set; }

    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

    public virtual PickupPoint PickupPoint { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;

    public virtual User? User { get; set; }

    public int FullOrderDiscount
    {
        get => OrderProducts.Sum(op => op.Product.Discount ?? 0);
    }

    public decimal FullOrderCost
    {
        get => OrderProducts.Sum(op => op.Product.FinalCost);
    }

    public bool AddNewProductToOrder(Product product)
    {
        // There is explicit cast to "OrderProduct", so if there is NULL value, cast will fail.
        if (OrderProducts.FirstOrDefault(op => op.ProductId == product.Id) is OrderProduct orderProduct)
        {
            orderProduct.Count++;
            return true;
        }

        var newOrderProduct = new OrderProduct()
        {
            OrderId = Id,
            ProductId = product.Id,
            Count = 1
        };
        OrderProducts.Add(newOrderProduct);
        return false;
    }
}
