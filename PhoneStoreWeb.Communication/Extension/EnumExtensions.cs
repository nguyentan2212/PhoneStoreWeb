﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStoreWeb.Communication.Extension
{
    public static class EnumExtensions
    {
        public static string ToDescriptionString<TEnum>(this TEnum @enum)
        {
            FieldInfo fieldInfo = @enum.GetType().GetField(@enum.ToString());
            var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes?[0].Description ?? @enum.ToString();
        }
    }
}
