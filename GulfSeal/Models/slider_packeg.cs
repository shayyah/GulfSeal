using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GulfSeal.Models
{
    public class slider_packeg
    {
        [Key]
        public int Id { get; set; }
        public string slider_name { get; set; }
        public string sublider_name { get; set; }
        public int lang_id { get; set; }
        public string lang_name { get; set; }
        public string image_url { get; set; }
        public string semilarity_key {get;set;}
        
    }
}