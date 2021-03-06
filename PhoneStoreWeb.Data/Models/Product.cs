﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PhoneStoreWeb.Data.Enums;

namespace PhoneStoreWeb.Data.Models
{
    public class Product
    {
        public int Id { set; get; }
        public string Name { get; set; }
        public string Description { get; set; }        
        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "Money")]
        public decimal Price { set; get; }                     
        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime CreatedDate { set; get; }
        public string OS { get; set; }
        public int RAM { get; set; }
        public int Storage { get; set; }
        public int BatteryCapacity { get; set; }              
        public ProductStatus Status { get; set; }
        public string Image { get; set; }
        public virtual Category Category { set; get; }
        public virtual List<ProductItem> ProductItems { get; set; }          
    }
}
