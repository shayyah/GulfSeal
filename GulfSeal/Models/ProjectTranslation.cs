using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GulfSeal.Models
{
    public class ProjectTranslation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }



        public int LanguageId { get; set; }

        public int ProjectId { get; set; }


        [ForeignKey("LanguageId")]
        public virtual Language Language { get; set; }


        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }


    }
}