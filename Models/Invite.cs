using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace exam4.Models
{
    public class Invite : BaseEntity
    {
        public int userId { get; set; }

        [ForeignKey("userId")]
        [InverseProperty("invites")]
        public User user { get; set; }
        public int invitedId { get; set; }

        [ForeignKey("invitedId")]
        public User invited { get; set; }
    }
}