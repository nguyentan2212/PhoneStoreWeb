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
        public string ImagePath { get; set; }
        public List<Product> Products { set; get; }               
    }
}
