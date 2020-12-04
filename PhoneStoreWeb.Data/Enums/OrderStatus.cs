using System.ComponentModel.DataAnnotations;

namespace PhoneStoreWeb.Data.Enums
{
    public enum OrderStatus
    {
        [Display(Name = "Chưa xác nhận")]
        Unconfirm,
        [Display(Name = "Đã xác nhận")]
        Confirm,
        [Display(Name = "Đã hủy")]
        Cancelled
    }
}
