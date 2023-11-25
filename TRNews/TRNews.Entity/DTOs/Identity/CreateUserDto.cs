using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRNews.Entity.DTOs.Identity
{
    public class CreateUserDto
    {
        [Required]
        public string? Email { get; init; }
        [Required]
        public string? Password { get; init; } 
        public string? Phonenumber { get; init; }
        public ICollection<string> Roles { get; init; }
    }
}
