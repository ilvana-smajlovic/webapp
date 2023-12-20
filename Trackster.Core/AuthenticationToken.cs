using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Trackster.Core
{
    public class AuthenticationToken
    {
        [Key]
        public int id { get; set; }
        public string tokenValue { get; set; }

        [ForeignKey(nameof(registeredUser))]
        public int registeredUserId { get; set; }
        public RegisteredUser registeredUser { get; set; }
        public DateTime timeOfGeneration { get; set; }
        public string? isAddress { get; set; }

        [JsonIgnore]
        public string twoFCode { get; set; }
        public bool twoFIsUnlocked{ get; set; }
    }
}
