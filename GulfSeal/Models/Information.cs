using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GulfSeal.Models
{
    public class Information
    {
        public Information()
        {
            CreatedAt = DateTime.Now;
            LastUpdatedAt = DateTime.Now;
            InformationURL = "1";
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string InformationURL { get; set; }



        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }


        [Required]
        public int InformationTypeId { get; set; } 

        [ForeignKey("InformationTypeId")]
        public virtual InformationType InformationType { get; set; }
    }
}