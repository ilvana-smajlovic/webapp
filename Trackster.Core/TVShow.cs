using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackster.Core
{
    public class TVShow
    {
        [Key]
        public int TVShowID { get; set; }
        public virtual Media Media { get; set; }

        public int? SeasonCount { get; set; }
        public int? EpisodeCount { get; set; }
        public int? EpisodeRuntime { get; set; }
        //public int TotalRuntime { get; set; } -> Mozemo ovo kasnije
    }
}
