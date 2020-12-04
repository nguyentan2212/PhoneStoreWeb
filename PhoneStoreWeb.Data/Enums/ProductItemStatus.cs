using System.ComponentModel.DataAnnotations;

namespace PhoneStoreWeb.Data.Enums
{
    public enum ProductItemStatus
    {
        [Display(Name = "Sẵn sàng")]
        Available,
        [Display(Name = "Đã bán")]
        Sold
    }
}
