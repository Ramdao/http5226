﻿using Microsoft.AspNetCore.Mvc;
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

    public class UserTimelineDto
    {
        public int usertime_Id { get; set; }

        public int timeline_Id { get; set; }

        public int user_id { get; set; }

        public string timeline_name { get; set; }

        public string first_name { get; set; }

        public string last_name { get; set; }
    }
}
