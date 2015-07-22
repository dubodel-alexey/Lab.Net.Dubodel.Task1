using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace task3.Infrastructure.Authentication
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string RoleName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}