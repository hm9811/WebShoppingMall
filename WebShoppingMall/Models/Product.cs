using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebShoppingMall.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        public string Title { get; set; }

        public string Descriptions { get; set; }

        [DataType(DataType.Currency)]
        public double Price { get; set; }
    }

    public class PhotoProduct : Product
    {
        public string PhotoId { get; set; }
    }
}
