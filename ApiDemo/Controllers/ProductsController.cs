using ApiDemo.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ApiDemo.Controllers
{
    public class ProductsController : ApiController
    {

        private IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet, Route("api/v1/Products/GetAll")]
        public async Task<IHttpActionResult> GetAll()
        {
            var Products = await productService.GetAllProductsAsync();
            return Ok(Products);
        }

        [HttpGet, Route("api/v1/Products/GetByName/{name}")]
        public async Task<IHttpActionResult> GetByName(string name)
        {
            var Products = await productService.GetProductsByName(name);
            return Ok(Products);
        }

        [HttpGet, Route("api/v1/Products/Get/{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var Products = productService.GetProductById(id);
            return Ok(Products);
        }

    }

}