using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackster.Core
{
    public class WatchlistTVShow
    {
        [Key]
        public int Id { get; set; }

        public virtual RegisteredUser User { get; set; }
        public virtual TVShow TVShow { get; set; }
        public virtual State State { get; set; }
    }
}