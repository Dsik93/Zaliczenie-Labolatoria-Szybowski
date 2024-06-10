using WebApi.Model;

namespace WebApi.Service
{
    public interface ISubjectService
    {
        Task<IEnumerable<Subject>> GetSubjectsAsync();
        Task<Subject> GetSubjectByIdAsync(int id);
        Task AddSubjectAsync(Subject subject);
        Task UpdateSubjectAsync(Subject subject);
        Task DeleteSubjectAsync(int id);
    }
}
