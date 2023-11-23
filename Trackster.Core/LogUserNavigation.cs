using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackster.Core
{
    public class LogUserNavigation
    {
        [Key]
        public int id { get; set; }

        [ForeignKey(nameof(user))]
        public int userID { get; set; }
        public RegisteredUser user { get; set; }
        public string? queryPath { get; set; }
        public string? postData { get; set; }
        public DateTime _time { get; set; }
        public string? ipAddress { get; set; }
        public string? exceptionMessage { get; set; }
        public bool isException { get; set; }
    }
}
