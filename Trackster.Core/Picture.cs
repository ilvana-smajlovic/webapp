using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackster.Core
{
    public class Picture
    {
        [Key]
        public int PictureId { get; set; }
        public string Name { get; set; }
        public byte[]? File { get; set; }

        public Picture()
        {}
        public Picture(string name, byte[]? file)
        {
            Name = name;
            File = file;
        }
    }
}
