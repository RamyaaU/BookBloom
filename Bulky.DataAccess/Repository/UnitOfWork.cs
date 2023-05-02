using BookBloom.Data;
using BookBloom.DataAccess.Repository.IRepository;
using BookBloom.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBloom.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private BookBloomDbContext dbContext;

        public UnitOfWork(BookBloomDbContext _dbContext)
        {
            dbContext = _dbContext;
            Category = new CategoryRepository(dbContext);
            Product = new ProductRepository(dbContext); 
        }

        public ICategoryRepository Category { get; private set; }
        public IProductRepository Product { get; private set; }

        public void Save()
        {
            dbContext.SaveChanges();
        }
    }
}
