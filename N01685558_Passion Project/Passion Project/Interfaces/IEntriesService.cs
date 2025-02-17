using Passion_Project.Models;

namespace Passion_Project.Interfaces
{
    public interface IEntriesService
    {
        Task<IEnumerable<EntriesDto>> List();

        Task<EntriesDto?> FindEntry(int id);

        Task<ServiceResponse> UpdateEntry(int id, EntriesDto entriesDto);

        Task<ServiceResponse> AddEntry(EntriesDto entriesDto);

        Task<ServiceResponse> DeleteEntry(int id);

        Task<IEnumerable<EntriesDto>> GetEntriesForTimeline(int timelineId);
    }
}
