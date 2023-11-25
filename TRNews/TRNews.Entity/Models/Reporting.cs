using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TRNews.Entity.Models
{
    [Table("Reportings")]
    public class Reporting
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }
        public bool Active { get; set; } = true;
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User IdentityUser { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))] 
        public Category Category { get; set; }
    }
}
