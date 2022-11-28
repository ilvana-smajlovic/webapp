using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackster.Core.ViewModels
{
    public class MovieShowVM
    {
        public int MovieID { get; set; }
        public int MediaId { get; set; }
        public int? Runtime { get; set; }
    }
}
