using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApi.Model;

namespace WebApi
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
    }
}