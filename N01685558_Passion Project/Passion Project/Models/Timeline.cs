using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Passion_Project.Models
{
    public class Timeline
    {

        [Key]
        public int timeline_Id { get; set; }

        public string timeline_name { get; set; }

        public DateTime date { get; set; }

        public string description { get; set; }

        public virtual ICollection<Entries> entries { get; set; } = new List<Entries>();


    }

    public class TimelineDto
    {
        public int timeline_Id { get; set; }

        public string timeline_name { get; set; }

        public DateTime date { get; set; }

        public string description { get; set; }

       

    }
}
