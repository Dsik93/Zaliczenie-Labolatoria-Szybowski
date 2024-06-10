using WebApi.Model;

namespace WebApi.Service
{
    public interface IRegistrationService
    {
        Task<IEnumerable<Registration>> GetRegistrationsAsync();
        Task<Registration> GetRegistrationByIdAsync(int id);
        Task AddRegistrationAsync(Registration registration);
        Task UpdateRegistrationAsync(Registration registration);
        Task DeleteRegistrationAsync(int id);
    }
}
