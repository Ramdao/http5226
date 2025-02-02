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

    }
}
