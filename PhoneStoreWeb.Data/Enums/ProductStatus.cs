using System.ComponentModel;

namespace PhoneStoreWeb.Data.Enums
{
    public enum ProductStatus
    {
        [Description("Còn hàng")]
        InStock,
        [Description("Hết hàng")]
        OutOfStock,
        [Description("Ngừng bán")]
        SoldOut
    }
}
