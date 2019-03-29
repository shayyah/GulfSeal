using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GulfSeal.Models
{
    public class sec_lang
    {
        [Key]
        public int Id { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public int langId { get; set; }
        public string image_url { get; set; }
        //sim_key for editing all lang
        public string sim_key { get; set; }
        public string page { get; set; }
        //three type => text-paragraph(title-body)- image- sec(image-paragraph)
        public string type { get; set; }
        [ForeignKey("langId")]
        public virtual Language Language { get; set; }
    }
}