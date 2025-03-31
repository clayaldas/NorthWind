namespace NorthWind.Sales.Entities.Dtos.CreateOrder;

public class CreateOrderDto (string customerID, string shipAddress, 
    string shipCity, string shipCountry, string shipPostalCode, 
    IEnumerable<CreateOrderDetailDto> orderDetails)
{
    public string CustomerID=> customerID;
    public string ShipAddress => shipAddress;
    public string ShipCity => shipCity;
    public string ShipCountry => shipCountry;
    public string ShipPostalCode => shipPostalCode;
    public IEnumerable<CreateOrderDetailDto> OrderDetails => orderDetails;
}
