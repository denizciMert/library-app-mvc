using Microsoft.EntityFrameworkCore;
using LibraryApp.Models;

namespace LibraryApp.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        { 

        }
        public DbSet<UsersModel> kullanicilar { get; set; }
        public DbSet<BooksModel> kitaplar { get; set; }
        public DbSet<RentsModel> kiralanan{ get; set; }
    }
}
