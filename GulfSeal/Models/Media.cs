using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GulfSeal.Models
{
    public class Media
    { 

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ArticleId { get; set; }

        [Required]
        public string Link { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public string Extinstion { get; set; }



        [ForeignKey("ArticleId")]
        public virtual Article Article { get; set; }


    }
}