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

        [ForeignKey("GenreID")]
        public Genre Genre { get; set; }
        public int GenreID { get; set; }

        [ForeignKey("MediaID")]
        public Media Media { get; set; }
        public int MediaID { get; set; }
    }
}