using System;
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

    public decimal FullOrderDiscount
    {
        get => OrderProducts.Sum(op => op.Product.FinalDiscountAmount * op.Count);
    }

    public decimal FullOrderCost
    {
        get => OrderProducts.Sum(op => op.Product.FinalCost * op.Count);
    }

    public DateTime OrderDeliveryDate
    {
        get
        {
            var currentDate = DateTime.Now;
            var isAllProductsAvailable = OrderProducts.All(op => op.Product.Amount > 3);

            return currentDate.AddDays(isAllProductsAvailable ? 3 : 6);
        }
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
            Order = this,
            OrderId = Id,
            Product = product,
            ProductId = product.Id,
            Count = 1
        };
        OrderProducts.Add(newOrderProduct);
        return false;
    }
}
