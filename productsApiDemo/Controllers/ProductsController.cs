using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using productsDataAccess;
using System.Net.Http;
using System.Net;

namespace productsApiDemo.Controllers
{
    public class ProductsController : ApiController
    {
        ProductEntities _entity = new ProductEntities();

        public IEnumerable<tbl_products> Get()
        {
            return _entity.tbl_products.ToList();
        }

        public HttpResponseMessage Get(int id)
        {
            var data = _entity.tbl_products.FirstOrDefault(e => e.Id == id);
            return data != null ? Request.CreateResponse(HttpStatusCode.OK, data) : Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Product with Id {id} not found.");
        }

        public HttpResponseMessage Post([FromBody] tbl_products products)
        {
            try
            {
                _entity.tbl_products.Add(products);
                _entity.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.Created, products);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var data = _entity.tbl_products.FirstOrDefault(e => e.Id == id);
                if (data != null)
                {
                    _entity.tbl_products.Remove(data);
                    _entity.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"Product with Id {id} not found.");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
