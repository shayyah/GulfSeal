using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GulfSeal.Models
{
    public class Language
    {
        public Language()
        {
            CreatedAt = DateTime.Now;
            LastUpdatedAt = DateTime.Now;
            RTL = false;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required]
        public string Name { get; set; }

        [Required]
        public string Abbreviation { get;set;} 


        [Required]
        public string FlagImageUrl { get; set; }
        public string FlagImageName { get; set; }

        public bool RTL { get; set; }

        public DateTime CreatedAt { get; set; } 
        public DateTime LastUpdatedAt { get; set; }

    }
}