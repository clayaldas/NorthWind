namespace NorthWind.Sales.Entities.Dtos.CreateOrder;

// Primary Constructors c# 12
public class CreateOrderDetailDto (int productId, decimal unitPrice, short quantity)
{
    public int ProductId => productId;
    public decimal UnitPrice => unitPrice;
    public short Quantity => quantity;
}
