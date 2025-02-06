using Passion_Project.Models;

namespace Passion_Project.Interfaces
{
    public interface IUserTimeline
    {
       

        Task<ServiceResponse> LinkUserToTimeline(int userId, int timelineId);

        Task<ServiceResponse> UnlinkUserFromTimeline(int userId, int timelineId);

        Task<ServiceResponse<List<int>>> GetTimelinesForUser(int userId);

        Task<ServiceResponse<List<int>>> GetUsersForTimeline(int timelineId);
    }
}
