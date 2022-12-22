using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackster.Core.ViewModels
{
    public class MediaShowVM
    {
        public int MediaId { get; set; }
        public string Name { get; set; }
        public DateTime AirDate { get; set; }
        public string Synopsis { get; set; }
        public float Rating { get; set; }
        public string Picture { get; set; }
        public int Status { get; set; }
    }
}
