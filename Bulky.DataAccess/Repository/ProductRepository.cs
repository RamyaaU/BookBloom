using BookBloom.Data;
using BookBloom.DataAccess.Repository.IRepository;
using BookBloom.Models;
using BookBloom.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBloom.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private BookBloomDbContext dbContext;

        public ProductRepository(BookBloomDbContext _dbContext) : base(_dbContext)
        {
            dbContext = _dbContext;
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }

        public void Update(Product product)
        {
            dbContext.Product.Update(product);
        }
    }
}
