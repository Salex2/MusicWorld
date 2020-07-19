using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MusicData.Models
{
    public class Songs
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime DateOfCreation { get; set; }  
        public string SongFilePath { get; set; }
        



        public virtual Artist ArtistSong { get; set; }
    }
}
