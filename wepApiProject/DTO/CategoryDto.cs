using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wepApiProject.DTO
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductDto> Products { get; set; }
    }
}