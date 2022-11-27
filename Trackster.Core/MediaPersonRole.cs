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

        [ForeignKey("MediaID")]
        public Media Media { get; set; }
        public int MediaID { get; set; }

        [ForeignKey("PersonID")]
        public Person Person { get; set; }
        public int PersonID { get; set; }

        [ForeignKey("RoleID")]
        public Role Role { get; set; }
        public int RoleID { get; set; }

        public string? Character { get; set; }
    }
}
