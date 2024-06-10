using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorAdminPanel.Data;
using RazorAdminPanel.Model;

namespace RazorAdminPanel.Pages.User
{
    public class IndexModel : PageModel
    {
        private readonly RazorAdminPanel.Data.RazorAdminPanelContext _context;

        public IndexModel(RazorAdminPanel.Data.RazorAdminPanelContext context)
        {
            _context = context;
        }

        public IList<Registration> Registration { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Registration = await _context.Registration.ToListAsync();
        }
    }
}
