using ApiDemo.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessLayer.Database;
using DataAccessLayer.Generic;
using System.Threading.Tasks;

namespace ApiDemo.Services
{
    public class ProductService : IProductService
    {
        private readonly UnitOfWork unitOfWork;

        public ProductService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await unitOfWork.Products.GetAsync(orderBy: x => x.OrderBy(y => y.Id));
        }

        public Product GetProductById(int id)
        {
            return unitOfWork.Products.GetSingle(x => x.Id == id);
        }

        public async Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            return await unitOfWork.Products.GetAsync(x => x.Name == name, y => y.OrderBy(z => z.Id));
        }
    }
}