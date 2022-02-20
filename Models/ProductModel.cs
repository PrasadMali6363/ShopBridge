using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        [StringLength(25)]
        public string Description { get; set; }
        [Range(1, 10000)]
        public float Price { get; set; }
    }
}
