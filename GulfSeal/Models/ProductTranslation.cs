using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GulfSeal.Models
{
    public class ProductTranslation
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }


        public string Thickness { get; set; }
        public string Length { get; set; }
        public string Width { get; set; }
        public string ReinforcedType { get; set; }
        public string ReinforcedDensity { get; set; }
        public string ServiceType { get; set; }



        public int LanguageId { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("LanguageId")]
        public virtual Language Language { get; set; }


        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }


    }
}