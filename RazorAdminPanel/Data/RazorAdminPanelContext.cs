using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RazorAdminPanel.Model;

namespace RazorAdminPanel.Data
{
    public class RazorAdminPanelContext : DbContext
    {
        public RazorAdminPanelContext (DbContextOptions<RazorAdminPanelContext> options)
            : base(options)
        {
        }

        public DbSet<RazorAdminPanel.Model.Registration> Registration { get; set; } = default!;
    }
}
