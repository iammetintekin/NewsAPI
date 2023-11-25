using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRNews.Entity.DTOs.Identity
{
    public class LoginUserDto
    {
        [Required]
        public string Username { get; init; }
        [Required]
        public string Password { get; init; }
    }
}
