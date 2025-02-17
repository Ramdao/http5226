namespace Passion_Project.Models
{
    public class EntryDetails
    {
        public required EntriesDto Entry { get; set; }
        public IEnumerable<TimelineDto> Timelines { get; set; } = new List<TimelineDto>();
    }
}
