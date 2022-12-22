using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trackster.Core
{
    public class Person
    {
        [Key]
        public int PersonId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Bio { get; set; }
        public string? Picture { get; set;}

        [ForeignKey("GenderID")]
        public Gender Gender { get; set; }
        public int GenderID { get; set; }
    }
}