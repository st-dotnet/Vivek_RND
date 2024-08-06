using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebForm.Models;

namespace WebForm.Data
{
    public class WebFormDbContext : DbContext
    {
        public WebFormDbContext(DbContextOptions<WebFormDbContext> options) : base(options)
        {
        }
        public DbSet<User> users { get; set; }
        public DbSet<AddUsers> AddUsers { get; set; }
        public DbSet<UpdateUser> UpdateUser { get; set; }
        public DbSet<WebForm.Models.DeleteUser> DeleteUser { get; set; }
    }
}
