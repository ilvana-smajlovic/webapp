﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackster.Core
{
    public class Media
    {
        [Key]
        public int MediaId { get; set; }
        public string Name { get; set; }
        public DateTime AirDate { get; set; }
        public string Synopsis { get; set; }
        public float Rating { get; set; }

        public virtual Picture Poster { get; set; }
        public virtual Status Status { get; set; }
    }
}