using Emart.DataAccessLayer.Data;
using Emart.DataAccessLayer.Infrastructure.IRepository;
using Emart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emart.DataAccessLayer.Infrastructure.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private  ApplicationDbContext _dbContext;
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(Product product)
        {
            var productDB = _dbContext.Products.FirstOrDefault(x => x.Id == product.Id);
            if(productDB != null)
            {
                productDB.Name = product.Name;
                productDB.Description = product.Description;
                productDB.Price = product.Price;
                if(product.ImageUrl != null)
                {
                    productDB.ImageUrl = product.ImageUrl;
                }

            }
        }
    }
}
