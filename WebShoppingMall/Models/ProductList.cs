using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebShoppingMall.Models
{
    public class ProductList
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public double Price { get; set; }

        public string Image { get; set; }

        [Display(Name = "Color Options")]
        public string Color { get; set; }

        [Required]
        [Display(Name = "Available")]
        public bool IsAvailable { get; set; }

        [Required]
        [Display(Name = "Product Type")]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public PhotoProduct ProductTypes { get; set; }

        [Required]
        [Display(Name = "Tag")]
        public int TagId { get; set; }

        [ForeignKey("TagId")]
        public TagProduct ProductTag { get; set; }
    }
}
