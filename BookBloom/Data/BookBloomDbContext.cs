using Microsoft.EntityFrameworkCore;

namespace BookBloom.Data
{
    public class BookBloomDbContext : DbContext
    {
        public BookBloomDbContext(DbContextOptions<BookBloomDbContext> options) : base(options)
        {
                
        }
    }
}
