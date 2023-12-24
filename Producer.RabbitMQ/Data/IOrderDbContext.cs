using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Producer.RabbitMQ.Data
{
	public interface IOrderDbContext
    {
        DbSet<Order> Order { get; set; }

        Task<int> SaveChangesAsync();
    }
}