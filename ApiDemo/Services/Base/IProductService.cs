using DataAccessLayer.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ApiDemo.Services.Base
{
    public interface IProductService
    {

        Task<IEnumerable<Product>> GetAllProductsAsync();

        Product GetProductById(int id);
         
        Task<IEnumerable<Product>> GetProductsByName(string name);
         
    }
}