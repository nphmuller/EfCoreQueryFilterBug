using System;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new MyContext())
            {
                context.Database.EnsureCreated();

                var entities = context.Set<MyEntity>().ToListAsync().Result;
            }
        }
    }

    public class MyContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("QueryFilterBug");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            int tenantId = 123;
            modelBuilder.Entity<MyEntity>()
                .HasQueryFilter(e => e.TenantId == tenantId);
        }
    }

    public class MyEntity
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
    }
}
