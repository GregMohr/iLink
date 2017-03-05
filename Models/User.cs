using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace exam4.Models
{
    public class User : BaseEntity
    {
        public User(){
            connections = new List<Connection>();
            invites = new List<Invite>();
        }
        [Display(Name="First Name")]
        [Required]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z''-'\s]{2,40}$", ErrorMessage = "Only letters are allowed.")]
        public string first { get; set; }

        [Display(Name="Last Name")]
        [Required]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z''-'\s]{2,40}$", ErrorMessage = "Only letters are allowed.")]
        public string last { get; set; }

        [Display(Name="Email")]
        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Display(Name="Password")]
        [Required]
        // [MinLength(8)]        
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$", ErrorMessage = "Password needs 8 characters of at least 1 letter, 1 number and 1 special character")]
        public string password { get; set; }

        [Display(Name="Profile Description")]
        [Required]
        [MinLength(8)] 
        public string description { get; set; }
        public ICollection<Connection> connections { get; set; }
        public ICollection<Invite> invites { get; set; }
    }
}