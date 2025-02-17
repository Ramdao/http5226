namespace Passion_Project.Models
{
    public class DashboardViewModel
    {
        public UserDto User { get; set; }
        public IEnumerable<UserTimelineDto> UserTimelines { get; set; }
        public IEnumerable<EntriesDto> RecentEntries { get; set; }
    }
}
