using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreVideo.Models;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreVideo.Entities
{
    public class Video // This class resides in entities because it will be used to define a table in the database.
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(80)]
        public string Title { get; set; }
        [Display(Name ="Film Genre")]
        public Genres Genre { get; set; }
    }
}
