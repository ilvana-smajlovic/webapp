﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackster.Core.ViewModels
{
    public class MediaPersonRoleAddVM
    {
        public int MediaID { get; set; }
        public int PersonID { get; set; }
        public int RoleID { get; set; }
        public string? Character { get; set; }
    }
}