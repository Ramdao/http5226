namespace Passion_Project.Models;

public class UserDetails
{
    
    public required UserDto User { get; set; }

    public IEnumerable<UserTimelineDto>? UserTimeline { get; set; }

}