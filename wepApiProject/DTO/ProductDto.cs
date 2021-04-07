using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wepApiProject.DTO
{
    public class ProductDto
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quentity { get; set; }
        public string Description { get; set; }

        public string Image { get; set; }
        public int Discount { get; set; }
    }
}