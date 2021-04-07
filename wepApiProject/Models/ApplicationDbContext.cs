using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wepApiProject.Models
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public ApplicationDbContext() :
           base("Data Source=AHMED-TOSHIBA;Initial Catalog=WebApiProject;Integrated Security=True")
        {

        }

        public ApplicationDbContext(string name) :
        base(name)
        {

        }



        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { set; get; }
        public virtual DbSet<ProductCart> ProductCarts { get; set; }

    }
}
