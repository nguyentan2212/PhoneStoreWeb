using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneStoreWeb.Data.Models
{
    public class CategoryLanguage
    {
        public int CategoryId { set; get; }
        public Category Category { set; get; }
        public Language Language { set; get; }
        public string LanguageId { set; get; }
        public string Name { set; get; }
    }
}
