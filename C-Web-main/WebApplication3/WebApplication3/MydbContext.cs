using Microsoft.EntityFrameworkCore;
using System;
using WebApplication3.Models;

namespace WebApplication3
{
    public class MydbContext :DbContext
    {
        public MydbContext(DbContextOptions<MydbContext> options) : base(options)
        {

        }
        public DbSet<Car> Cars { get; set; }

    }
}