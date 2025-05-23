﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Tech.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        [DisplayName("Price")]
        [Range(1, 10000)]
        public double Price { get; set; }
        [Required]
        [DisplayName("Discounted Price")]
        [Range(1, 10000)]
        public double DiscountedPrice { get; set; }
        [DisplayName("Category")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }
        [DisplayName("Product Images")]
        [ValidateNever]
        public List<ProductImage> ProductImages { get; set; }
    }
}
