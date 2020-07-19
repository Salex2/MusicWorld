using MusicData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicData
{
    public interface IArtists
    {
        Artist GetArtist(int Id);
    }
}
