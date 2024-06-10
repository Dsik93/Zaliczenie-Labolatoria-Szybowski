using Microsoft.EntityFrameworkCore;
using WebApi.Model;

namespace WebApi.Service
{
    public class RegistrationService : IRegistrationService
    {
        private readonly ApplicationDbContext _context;

        public RegistrationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Registration>> GetRegistrationsAsync()
        {
            return await _context.Registrations.ToListAsync();
        }

        public async Task<Registration> GetRegistrationByIdAsync(int id)
        {
            return await _context.Registrations.FindAsync(id);
        }

        public async Task AddRegistrationAsync(Registration registration)
        {
            _context.Registrations.Add(registration);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRegistrationAsync(Registration registration)
        {
            _context.Entry(registration).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRegistrationAsync(int id)
        {
            var registration = await _context.Registrations.FindAsync(id);
            if (registration != null)
            {
                _context.Registrations.Remove(registration);
                await _context.SaveChangesAsync();
            }
        }
    }
}