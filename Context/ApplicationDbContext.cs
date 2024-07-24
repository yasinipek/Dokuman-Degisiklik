using Microsoft.EntityFrameworkCore;
using WebApplication18.Models;

namespace WebApplication18.Context
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
       : base(options)
        {
        }

        public DbSet<DocumentModel> Documents { get; set; }

    }
}
