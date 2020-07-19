using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MusicData.Models
{
    public  class Artist
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        public string Bio { get; set; }
        public int Age { get; set; } 
        [Required]
        public int NumberOfAlbums { get; set; }
        public int NumberOfLikes { get; set; }


        
    }
}
