using Passion_Project.Models;

namespace Passion_Project.Interfaces
{
    public interface IUserTimeline
    {
        Task<IEnumerable<UserTimelineDto>> List();

        Task<ServiceResponse> LinkUserToTimeline(int userId, int timelineId);

        Task<ServiceResponse> UnlinkUserFromTimeline(int userId, int timelineId);
    }
}
