using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebShoppingMall.Models
{
    public class PhotoProduct
    {
        [Key]
        public int ProductId { get; set; }

        [Display(Name = "Product Type")]
        public string Title { get; set; }

    }

}
