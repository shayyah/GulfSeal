using GulfSeal.Areas.Admin.Models.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GulfSeal.Models
{
    public class Project
    {
        public Project()
        {
            CreatedAt = DateTime.Now;
            LastUpdatedAt = DateTime.Now;
            Rank = 0;
        }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Rank { get; set; }

        [Required]
        public string Title { get; set; }
        public string ImageURL { get; set; }


        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }

        [Range(1, int.MaxValue)]
        public Enums.ProjectType? ProjectType { get; set; }

        public virtual ICollection<ProjectTranslation> ProjectTranslations { get; set; }

    }
}