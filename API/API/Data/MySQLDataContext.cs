using API.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace API.Data
{
    public class MySQLDataContext : DbContext
    {
        public virtual DbSet<Url> URLs { get; set; }

        public MySQLDataContext(DbContextOptions<MySQLDataContext> options) : base(options) 
        { 
            Database.EnsureCreated();
            Database.Migrate();
        }
    }
}
