using Passion_Project.Models;

namespace Passion_Project.Interfaces
{
    public interface IUserTimeline
    {
       

        Task<ServiceResponse> LinkUserToTimeline(int userId, int timelineId);

        Task<ServiceResponse> UnlinkUserFromTimeline(int userId, int timelineId);

        Task<IEnumerable<UserTimelineDto>> GetTimelinesForUser(int userId);

        Task<IEnumerable<UserTimelineDto>> GetUsersForTimeline(int timelineId);
    }
}
