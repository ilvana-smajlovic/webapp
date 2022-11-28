using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackster.Core.ViewModels
{
    public class TVShowAddVM
    {  
        public int MediaID { get; set; }
        public int? SeasonCount { get; set; }
        public int? EpisodeCount { get; set; }
        public int? EpisodeRuntime { get; set; }
    }
}
