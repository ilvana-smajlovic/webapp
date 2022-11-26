using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackster.Core.ViewModels
{
    public class WatchlistMediaShowVM
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public int MediaID { get; set; }
        public int StateID { get; set; }
        public int RatingID { get; set; }
    }
}