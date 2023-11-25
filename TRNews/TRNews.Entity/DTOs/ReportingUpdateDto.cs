using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRNews.Entity.DTOs
{
    public class ReportingUpdateDto
    {
        [Required]
        public int Id { get; set; } 
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public bool Active { get; set; } = true;
        [Required]
        public int UserId { get; set; }
        public DateTime Created { get; set; }

    }
}
