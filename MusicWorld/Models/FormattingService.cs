using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicWorld.Models
{
    public class FormattingService
    {
        public string AsReadableDate(DateTime date) //Custom method for date; we can inject it in our Views
        {
            return date.ToString("d");
        }

    }
}
