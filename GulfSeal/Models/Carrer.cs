using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GulfSeal.Models
{
    public class Carrer
    {
        public Carrer()
        {
            CreatedAt = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
 
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
         
        public DateTime CreatedAt { get; set; }
    }
}