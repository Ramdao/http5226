using Passion_Project.Models;

namespace Passion_Project.Interfaces
{
    public interface ITimelineService
    {

        Task<IEnumerable<TimelineDto>> List();

        Task<TimelineDto?> FindTimeline(int id);

        Task<ServiceResponse> UpdateTimeline(int id, TimelineDto timelineDto);

        Task<ServiceResponse> AddTimeline(TimelineDto timelineDto);

        Task<ServiceResponse> DeleteTimeline(int id);


        Task<IEnumerable<TimelineDto>> GetTimelinesForEntry(int entryId);
    }
}
