using ORDER_MANAGEMENT.Data;
using System.Collections.Generic;
using System.Web.Http;

namespace ORDER_MANAGEMENT.API.Controllers
{
    public class ProductController : ApiController
    {
        private readonly IUnitOfWork db;
        public ProductController(IUnitOfWork unitOfWork)
        {
            db = unitOfWork;
        }
        // GET: api/Product
        [HttpGet]
        [Route("api/DistributorProduct")]
        public ICollection<ProductVM> DistributorProduct(int id, string search = "")
        {
            return db.Products.GetProductWithDistributorPrice(id, search);
        }

        // GET: api/Product
        [HttpGet]
        [Route("api/OutletProduct")]
        public ICollection<ProductVM> OutletProduct(string search = "")
        {
            return db.Products.GetProductBySearch(search);
        }

        // GET: api/Product/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Product
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Product/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Product/5
        public void Delete(int id)
        {
        }
    }
}
