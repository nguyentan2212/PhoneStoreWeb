using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneStoreWeb.Data.Models
{
    public class ProductLanguage
    {
        public int ProductId { set; get; }
        public Product Product { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public string ProductUrl { set; get; }
        public string LanguageId { set; get; }
        public Language Language { set; get; }
    }
}
