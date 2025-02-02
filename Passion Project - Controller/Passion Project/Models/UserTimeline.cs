using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Passion_Project.Models
{
    public class UserTimeline
    {
       [Key]
       public int usertime_Id { get; set; }

        [ForeignKey("Timeline")]
        public int timeline_Id { get; set; }
        public virtual Timeline Timeline { get; set; }

        [ForeignKey("Users")]
        public int user_id { get; set; }
        public virtual Users Users { get; set; }
        

        

    }
}
