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
            //dbContext.Product.Update(product);
            var objFromDb = dbContext.Product.FirstOrDefault(u => u.Id == product.Id);
            if(objFromDb != null)
            {
                objFromDb.Title = product.Title;
                objFromDb.Description = product.Description;
                objFromDb.ISBN = product.ISBN;
                objFromDb.Price = product.Price;
                objFromDb.Price50 = product.Price50;
                objFromDb.ListPrice = product.ListPrice;
                objFromDb.Price100 = product.Price100;
                objFromDb.CategoryId = product.CategoryId;
                objFromDb.Author = product.Author;
                if(product.ImageUrl != null)
                {
                    objFromDb.ImageUrl = product.ImageUrl;
                }
            }
        }
    }
}
