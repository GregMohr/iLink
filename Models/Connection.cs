using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace exam4.Models
{
    public class Connection : BaseEntity
    {
        public int userId { get; set; }

        [ForeignKey("userId")]
        [InverseProperty("connections")]
        public User user { get; set; }

        public int connectionId { get; set; }

        [ForeignKey("connectionId")]
        public User connection { get; set; }
    }
}