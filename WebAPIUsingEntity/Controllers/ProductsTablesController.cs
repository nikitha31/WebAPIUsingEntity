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
using WebAPIUsingEntity.Models;

namespace WebAPIUsingEntity.Controllers
{
    public class ProductsTablesController : ApiController
    {
        private ProductsEntities db = new ProductsEntities();

        // GET: api/ProductsTables
        public IQueryable<ProductsTable> GetProductsTables()
        {
            return db.ProductsTables;
        }

        // GET: api/ProductsTables/5
        [ResponseType(typeof(ProductsTable))]
        public IHttpActionResult GetProductsTable(int id)
        {
            ProductsTable productsTable = db.ProductsTables.Find(id);
            if (productsTable == null)
            {
                return NotFound();
            }

            return Ok(productsTable);
        }

        // PUT: api/ProductsTables/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProductsTable(int id, ProductsTable productsTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productsTable.Pid)
            {
                return BadRequest();
            }

            db.Entry(productsTable).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsTableExists(id))
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

        // POST: api/ProductsTables
        [ResponseType(typeof(ProductsTable))]
        public IHttpActionResult PostProductsTable(ProductsTable productsTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductsTables.Add(productsTable);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ProductsTableExists(productsTable.Pid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = productsTable.Pid }, productsTable);
        }

        // DELETE: api/ProductsTables/5
        [ResponseType(typeof(ProductsTable))]
        public IHttpActionResult DeleteProductsTable(int id)
        {
            ProductsTable productsTable = db.ProductsTables.Find(id);
            if (productsTable == null)
            {
                return NotFound();
            }

            db.ProductsTables.Remove(productsTable);
            db.SaveChanges();

            return Ok(productsTable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductsTableExists(int id)
        {
            return db.ProductsTables.Count(e => e.Pid == id) > 0;
        }
    }
}