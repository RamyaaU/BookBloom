using BookBloom.Data;
using BookBloom.DataAccess.Repository.IRepository;
using BookBloom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBloom.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private BookBloomDbContext dbContext;

        public CategoryRepository(BookBloomDbContext _dbContext) : base(_dbContext)
        {
            dbContext = _dbContext;
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }

        public void Update(Category category)
        {
            dbContext.Category.Update(category);
        }
    }
}
