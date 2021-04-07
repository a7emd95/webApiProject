using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using wepApiProject.DTO;
using wepApiProject.Models;

namespace wepApiProject.Controllers
{
    [Authorize]
    public class CategoriesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Categories
        public IQueryable<Category> GetCategories()
        {
            return db.Categories;
        }

        // GET: api/Categories/5
        [ResponseType(typeof(CategoryDto))]
        public IHttpActionResult GetCategory(int id)
        {
            Category category = db.Categories.Find(id);

            CategoryDto categoryDto = new CategoryDto();

            categoryDto.Id = category.Id;
            categoryDto.Name = category.Name;
            List<ProductDto> productslist = new List<ProductDto>();

            for (var i = 0; i < category.Products.Count; i++)
            {
                ProductDto productDto = new ProductDto();

                productDto.Image = category.Products[i].Image;
                productDto.Name = category.Products[i].Name;
                productDto.Price = category.Products[i].Price;
                productDto.Quentity = category.Products[i].Quentity;
                productDto.Discount = category.Products[i].Discount;

                productslist.Add(productDto);

            }
            categoryDto.Products = productslist;


            if (category == null)
            {
                return NotFound();
            }

            return Ok(categoryDto);
        }


        // PUT: api/Categories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCategory(int id, Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != category.Id)
            {
                return BadRequest();
            }

            db.Entry(category).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Categories
        [ResponseType(typeof(Category))]
        public IHttpActionResult PostCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Categories.Add(category);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = category.Id }, category);
        }

        // DELETE: api/Categories/5
        [ResponseType(typeof(Category))]
        public IHttpActionResult DeleteCategory(int id)
        {
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            db.Categories.Remove(category);
            db.SaveChanges();

            return Ok(category);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoryExists(int id)
        {
            return db.Categories.Count(e => e.Id == id) > 0;
        }
    }
}