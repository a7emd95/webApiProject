using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace wepApiProject.Models
{
 
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [JsonIgnore]
        public virtual List<Product> Products { get; set; }
    }
}
