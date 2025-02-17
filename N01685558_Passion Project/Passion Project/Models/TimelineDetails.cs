using Passion_Project.Data.Migrations;

namespace Passion_Project.Models
{
    public class TimelineDetails
    {

        public required TimelineDto Timeline { get; set; }

        public IEnumerable<EntriesDto> Entries { get; set; }

        public IEnumerable<UserTimelineDto>? UserTimeline { get; set; }

     
    }
}
