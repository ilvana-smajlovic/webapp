using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackster.Core
{
    public class Rating
    {
        [Key]
        public int RatingID { get; set; }
        public int RatingValue { get; set; }
    }
}
