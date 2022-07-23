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
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private  ApplicationDbContext _dbContext;
        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(Category category)
        {
            
            var categoryDB = _dbContext.Categories.FirstOrDefault(x=>x.Id==category.Id);
            if(categoryDB != null)
            {
                categoryDB.Name = category.Name;
                categoryDB.DisplayOrder = category.DisplayOrder;
            }

        }
    }
}
