using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhoneStoreWeb.Data.Models
{
    public class Language
    {
        public string Id { set; get; }
        [Required]
        public string Name { set; get; }
        [Required]
        public bool IsDefault { set; get; }
        public List<ProductLanguage> ProductLanguages { set; get; }
        public List<CategoryLanguage> CategoryLanguages { set; get; }

    }
}
