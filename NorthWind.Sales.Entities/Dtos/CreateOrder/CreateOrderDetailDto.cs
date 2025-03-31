namespace NorthWind.Sales.Entities.Dtos.CreateOrder;

////  Constructores primarios (C# 12)
public class CreateOrderDetailDto(int productId,
    decimal unitPrice, short quantity)
{
    public int ProductId => productId;
    public decimal UnitPrice => unitPrice;
    public short Quantity => quantity;
}


//  C# 11-10-9-8
//public class CreateOrderDetailDto
//{
//    public int ProductId { get; }
//    public decimal UnitPrice {  get; }
//    public short Quantity { get; }

//    public CreateOrderDetailDto(int productId, decimal unitPrice, 
//        short quantity)
//    {
//        ProductId = productId;
//        UnitPrice = unitPrice;
//        Quantity = quantity;
//    }
//}

////  C# 7
//public class CreateOrderDetailDto
//{ 
//    private readonly int _productId;
//    private readonly decimal _unitPrince;
//    private readonly short _quantity;

//    public int ProductId { get { return _productId; } }
//    public decimal UnitPrice { get { return _unitPrince; } }
//    public short Quantity { get { return _quantity; } }

//    public CreateOrderDetailDto(int productId, decimal unitPrice, 
//        short quantity)
//    {
//        _productId = productId;
//        _unitPrince = unitPrice;
//        _quantity = quantity;
//    }
//}