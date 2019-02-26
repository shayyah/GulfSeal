using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GulfSeal.Models
{
    public class Article
    {
        public Article()
        {
            CreatedAt = DateTime.Now;
            LastUpdatedAt = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }


        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }


        [Required]
        public string FileName { get; set; }
        public string Link { get; set; }
         
        public string Extinstion { get; set; }


        public virtual ICollection<Media> MediaFiles { get; set; }
        public virtual ICollection<ArticleContent> ArticleContents { get; set; }
    }
}