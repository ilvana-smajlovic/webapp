using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackster.Core
{
    public class GenreMedia
    {
        [Key]
        public int Id { get; set; }

        public virtual Genre Genre { get; set; }
        public virtual Media Media { get; set; }
    }
}