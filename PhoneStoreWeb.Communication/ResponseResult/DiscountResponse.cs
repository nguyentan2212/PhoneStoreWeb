using System;

namespace PhoneStoreWeb.Communication.ResponseResult
{
    public class DiscountResponse
    {
        public int Id { set; get; }
        public string Code { set; get; }
        public int DiscountPercent { set; get; }
        public int DiscountAmount { set; get; }
        public DateTime FromDate { set; get; }
        public DateTime ToDate { set; get; }
    }
}
