using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GulfSeal.Models
{
    public class Product
    {
        public Product()
        {
            CreatedAt = DateTime.Now;
            LastUpdatedAt = DateTime.Now;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }


        public string Thickness { get; set; }
        public string Length { get; set; }
        public string Width { get; set; }
        public string ReinforcedType { get; set; }
        public string ReinforcedDensity { get; set; }
        public string ServiceType { get; set; }

         
        public string FileName { get; set; }
        public string Link { get; set; } 
        public string Extinstion { get; set; }


        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }



        public int FamilyId { get; set; } 

        [ForeignKey("FamilyId")]
        public virtual Family Family { get; set; }
         

    }
}