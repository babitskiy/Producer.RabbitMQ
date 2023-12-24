using System;
using Microsoft.EntityFrameworkCore;

namespace Producer.RabbitMQ.Data
{
    public class OrderDbContext : DbContext, IOrderDbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options)
            : base(options)
        {
            //base.Database.EnsureCreated();
        }

        public DbSet<Order> Order { get; set; }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
    }
}