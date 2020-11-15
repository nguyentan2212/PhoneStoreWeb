namespace PhoneStoreWeb.Communication.ResponseResult
{
    public class CartItemResponse
    {
        public ProductResponse Product { get; set; }
        public int CartID { set; get; }
        public int Quantity { set; get; }
    }
}