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

        [ForeignKey("UserRegisteredUserId")]
        public RegisteredUser User { get; set; }
        public int UserRegisteredUserId { get; set; }

        [ForeignKey("TVShowID")]
        public TVShow TVShow { get; set; }
        public int TVShowID { get; set; }

        [ForeignKey("StateID")]
        public State State { get; set; }
        public int StateID { get; set; }

        [ForeignKey("RatingID")]
        public Rating Rating { get; set; }
        public int? RatingID { get; set; }
    }
}