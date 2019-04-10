using Microsoft.AspNetCore.Mvc;
using Shop.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Controllers.API
{    
    [Route("api/[controller]")]
    
    public class ProductsController:Controller
    {
        private IProductRepository productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            return this.Ok(this.productRepository.GetAll());
        }

    }
}
