using glory.BookStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace glory.BookStore.Data
{
    //dbcontext updated to IdentityDbContext #91
    public class BookStoreContext : IdentityDbContext<ApplicationUser>
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options)
        {

        }
        public DbSet<Books> Books { get; set; }
        public DbSet<Language> Language { get; set; }
        public DbSet<BookGallery> BookGallery { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //connection stringi startup a ekledik. realtime app>>> app settings.json da olur gercekte
        //    optionsBuilder.UseSqlServer("Server=GLORY;Database=BookStore;Integrated Security=True");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
