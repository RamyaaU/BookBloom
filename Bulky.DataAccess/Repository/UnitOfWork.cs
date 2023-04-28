using BookBloom.Data;
using BookBloom.DataAccess.Repository.IRepository;
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
        }

        public ICategoryRepository Category { get; private set; }

        public void Save()
        {
            dbContext.SaveChanges();
        }
    }
}
