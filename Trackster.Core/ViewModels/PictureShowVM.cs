using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackster.Core.ViewModels
{
    public class PictureShowVM
    {
        public int PictureId { get; set; }
        public string Name { get; set; }
        public byte[]? File { get; set; }
    }
}
