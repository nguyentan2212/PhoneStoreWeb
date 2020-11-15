﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
