namespace Passion_Project.Models
{
    internal class UserTimelineViewModel
    {
        public IEnumerable<UserDto> Users { get; set; }
        public IEnumerable<TimelineDto> Timelines { get; set; }
        public IEnumerable<UserTimelineDto> UserTimelines { get; set; }
      
    }
}