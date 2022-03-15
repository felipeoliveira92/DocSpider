using DocSpider.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocSpider.Infra
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {

        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Document> Documents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connection = "Integrated Security=SSPI;Persist Security Info=False;" +
                "User ID=sa;Initial Catalog=dbDocSpider;Data Source=DESKTOP-FELIPE";

            optionsBuilder.UseSqlServer(connection);
        }
    }
}
