using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PhoneStoreWeb.Data.Enums
{
    public enum ProductStatus
    {
        [Display(Name = "Còn hàng")]        
        InStock,
        [Display(Name = "Hết hàng")]       
        OutOfStock,
        [Display(Name = "Ngừng bán")]       
        SoldOut
    }
}
