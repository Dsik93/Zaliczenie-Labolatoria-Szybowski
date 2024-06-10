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
    public class DetailsModel : PageModel
    {
        private readonly RazorAdminPanel.Data.RazorAdminPanelContext _context;

        public DetailsModel(RazorAdminPanel.Data.RazorAdminPanelContext context)
        {
            _context = context;
        }

        public Registration Registration { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registration = await _context.Registration.FirstOrDefaultAsync(m => m.Id == id);
            if (registration == null)
            {
                return NotFound();
            }
            else
            {
                Registration = registration;
            }
            return Page();
        }
    }
}
