﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GulfSeal.Models
{
    public class Family
    { 
        public Family()
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
          
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}