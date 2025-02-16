using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Passion_Project.Models
{
    public class Users
    {
        [Key]
        public int user_Id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string friend_list { get; set; }
        
        public string password { get; set; }
       


    }

    public class UserDto
    {
        public int user_Id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string friend_list { get; set; }
        public string password { get; set; }
    }
}
