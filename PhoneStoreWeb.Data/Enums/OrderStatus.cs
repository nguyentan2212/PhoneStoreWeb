using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PhoneStoreWeb.Data.Enums
{
    public enum OrderStatus
    {
        [Display(Name = "Chưa xác nhận")]
        [Description("Chưa xác nhận")]
        Unconfirm,
        [Display(Name = "Đã xác nhận")]
        [Description("Đã xác nhận")]
        Confirm,
        [Display(Name = "Đã hủy")]
        [Description("Đã hủy")]
        Cancelled
    }
}
