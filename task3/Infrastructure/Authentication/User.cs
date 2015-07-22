using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace task3.Infrastructure.Authentication
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public String Username { get; set; }

        [Required]
        public String Email { get; set; }

        [Required]
        public String Password { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
    }
}