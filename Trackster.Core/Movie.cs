using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackster.Core
{
    public class Movie
    {
        [Key]
        public int MovieID { get; set; }
        public virtual Media Media { get; set; }

        public int? Runtime { get; set; }
    }
}