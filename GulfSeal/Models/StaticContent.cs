using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GulfSeal.Models
{
    public class StaticContent
    { 

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
         

        public string About { get; set; }
        public string Privacy { get; set; }


        public virtual ICollection<StaticContentTranslation> StaticContentTranslations { get; set; }

    }
}