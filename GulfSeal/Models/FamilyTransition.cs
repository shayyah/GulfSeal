using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GulfSeal.Models
{
    public class FamilyTransition
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
         


        public int LanguageId { get; set; }

        public int FamilyId { get; set; }


        [ForeignKey("LanguageId")]
        public virtual Language Language { get; set; }


        [ForeignKey("FamilyId")]
        public virtual Family Family { get; set; }


    }
}