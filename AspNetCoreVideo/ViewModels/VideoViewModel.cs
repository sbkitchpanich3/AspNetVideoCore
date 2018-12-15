using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreVideo.ViewModels
{
    public class VideoViewModel // Responsible for what's being displayed on the view.  Different from the model in the sense that the model only contains data, not entirely responsible for what's displayed.
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
    }
}
