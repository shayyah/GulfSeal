using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GulfSeal.Models
{
    public class InformationTranslation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }




        public int LanguageId { get; set; }
         

        [ForeignKey("LanguageId")]
        public virtual Language Language { get; set; }

        
        public int InformationId { get; set; }

        [ForeignKey("InformationId")]
        public virtual Information Information { get; set; }

    }
}