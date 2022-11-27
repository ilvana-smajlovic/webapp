using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackster.Core
{
    public class MediaPersonRole
    {
        [Key]
        public int Id { get; set; }

        public virtual Media Media { get; set; }
        public virtual Person Person { get; set; }
        public virtual Role Role { get; set; }
        public string? Character { get; set; }
    }
}
