namespace PhoneStoreWeb.Communication.Orders
{
    public class OrderItem
    {
        public int ProductItemId { get; set; }
        public int WarrantyPeriod { get; set; }
        public string Name { get; set; }
        public string SerialNumber { get; set; }      
        public decimal SoldPrice { get; set; }
    }
}
