using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wepApiProject.Models;

namespace wepApiProject.Models
{
    [Table("Cart")]
    public class Cart
    {
        [Key]
        [ForeignKey("Account")]
        public string AccountID { get; set; }

        [JsonIgnore]
        public virtual IdentityUser Account { get; set; }
        [JsonIgnore]
        public virtual List<ProductCart> ProductsCart { get; set; }

    }
}
