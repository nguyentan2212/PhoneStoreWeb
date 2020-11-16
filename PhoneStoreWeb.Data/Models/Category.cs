using PhoneStoreWeb.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneStoreWeb.Data.Models
{
    public class Category
    {
        public int Id { set; get; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public bool IsShowOnHome { set; get; }
        [Required]
        public CategoryStatus Status { set; get; }
        [Required]
        [Column(TypeName = "Date")]
        public DateTime Created_At { set; get; }
        public string ImagePath { get; set; }
        public List<Product> Products { set; get; }        
        public List<Blog> Blogs { set; get; }
    }
}
