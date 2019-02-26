using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GulfSeal.Models
{
    public class ArticleContent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }


        public int LanguageId { get; set; }
         
        public int ArticleId { get; set; }
         
         
        [ForeignKey("LanguageId")]
        public virtual Language Language { get; set; }


        [ForeignKey("ArticleId")]
        public virtual Article Article { get; set; }



    }
}