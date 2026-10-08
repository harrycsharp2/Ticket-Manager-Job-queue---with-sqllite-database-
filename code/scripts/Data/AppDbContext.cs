using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using Jobqueue_TicketSystem.scripts.Models;

namespace Jobqueue_TicketSystem.scripts.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Job> job { get; set; }
        public DbSet<Manager> manager { get; set; }
        public DbSet <Worker> worker { get; set; }
        public DbSet <Message> message { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=app.db");
        }
    }
}
