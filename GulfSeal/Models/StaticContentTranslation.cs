using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GulfSeal.Models
{
    public class StaticContentTranslation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 

        public string About { get; set; }
        public string Privacy { get; set; }




        public int LanguageId { get; set; }

        public int StaticContentId { get; set; }
         
        [ForeignKey("LanguageId")]
        public virtual Language Language { get; set; }
         
        [ForeignKey("StaticContentId")]
        public virtual StaticContent StaticContent { get; set; }
    }
}