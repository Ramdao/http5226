using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Passion_Project.Models
{
    public class Entries
    {

        [Key]

        public int entries_Id { get; set; }

        public string entries_name { get; set; }

        public string description { get; set; }

        public string location { get; set; }

        public string images { get; set; }

        [ForeignKey("Timeline")]
        public int timeline_Id { get; set; }

        public virtual Timeline Timeline { get; set; }
    }

    public class EntriesDto
    {
        public int entries_Id { get; set; }

        public string entries_name { get; set; }

        public string description { get; set; }

        public string location { get; set; }

        public string images { get; set; }

        public int timeline_Id { get; set; }

        public TimelineDto? Timeline { get; set; }
       
    }
}
