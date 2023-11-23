using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackster.Core
{
    public class UserFavourites
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("UserID")]
        public int UserID { get; set; }
        public RegisteredUser User { get; set; }


        [ForeignKey("MediaID")]
        public int MediaID { get; set; }
        public Media Media { get; set; }

    }
}
