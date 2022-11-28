using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackster.Core.ViewModels
{
    public class MovieAddVM
    {
        public int MediaId { get; set; }
        public int? Runtime { get; set; }
    }
}
