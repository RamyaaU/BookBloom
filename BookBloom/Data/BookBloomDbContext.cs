using BookBloom.Models;
using Microsoft.EntityFrameworkCore;

namespace BookBloom.Data
{
    public class BookBloomDbContext : DbContext
    {
        public BookBloomDbContext(DbContextOptions<BookBloomDbContext> options) : base(options)
        {
                
        }

        public DbSet<Category> Category { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Sci-fi", DisplayOrder = 2 },
                new Category { Id = 3, Name = "History", DisplayOrder = 3 }
                );
        }
    }

}
