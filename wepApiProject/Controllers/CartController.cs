using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Description;
using wepApiProject.DTO;
using wepApiProject.Models;

namespace wepApiProject.Controllers
{
    public class CartController : ApiController
    {

        private ApplicationDbContext db = new ApplicationDbContext();




        [ResponseType(typeof(ProductCartDto))]
        [HttpGet]

        public IHttpActionResult Get_Cart()
        {
            var user_id = User.Identity.GetUserId();
            var productCart = db.ProductCarts.Where(pc => pc.Cart_Id == user_id).ToList();


            ProductCartDto productCartDto = new ProductCartDto();
            List<ProductDto> productDtoList = new List<ProductDto>();
            foreach (var item in productCart)
            {
                var product = db.Products.Find(item.Product_Id);
                ProductDto productDto = new ProductDto();
                productDto.Image = product.Image;
                productDto.Name = product.Name;
                productDto.Price = product.Price;
                productDto.Quentity = item.Quntity;
                productDto.Discount = product.Discount;
                productDto.Description = product.Description;
                productDtoList.Add(productDto);

            }
            productCartDto.Productss = productDtoList;

            return Ok(productCartDto);
        }

        // http://localhost:13149/api/Cart?Product_id=1

        [HttpPost]
        public IHttpActionResult Post_Pro(int Product_id)
        {
            string userID = "";
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                var userIdClaim = claimsIdentity.Claims.FirstOrDefault(y => y.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                {
                    userID = userIdClaim.Value;
                }
            }


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Checkcart = db.Carts.Find(userID);
            if (Checkcart == null)
            {
                Cart cart = new Cart { AccountID = userID };
                db.Carts.Add(cart);
                db.SaveChanges();
            }

            var productInCart = db.ProductCarts.Where(p => p.Cart_Id == userID && p.Product_Id == Product_id).FirstOrDefault();
            if (productInCart != null)
            {
                productInCart.Quntity += 1;
                db.Entry(productInCart).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                    return Ok("Product Added To cart");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(Product_id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }



            }
            ProductCart pro_cart = new ProductCart { Cart_Id = userID, Product_Id = Product_id, Quntity = 1 };

            db.ProductCarts.Add(pro_cart);
            db.SaveChanges();



            //return CreatedAtRoute("DefaultApi", new { id = pro_cart.Id }, pro_cart);
            return Ok("Product Add To cart");
        }


        // Delete Productfrom Cart
        // http://localhost:13149/api/Cart?Product_id=1
        public IHttpActionResult Delete_Product(int Product_id)
        {
            var user_id = User.Identity.GetUserId();
            var product_Cart = db.ProductCarts.Where(pc => pc.Cart_Id == user_id).ToList();

            foreach (var item in product_Cart)
            {
                if (item.Product_Id == Product_id)
                {
                    ProductCart proCart = db.ProductCarts.Find(item.Id);
                    if (proCart == null)
                    {
                        return NotFound();
                    }
                    db.ProductCarts.Remove(proCart);
                    db.SaveChanges();
                }
            }
            return Ok("Product Deleted");
        }
        private bool CategoryExists(object id)
        {
            throw new NotImplementedException();
        }


    }
}
