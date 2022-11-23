using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trackster.Core
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public byte[] Picture { get; set; }
        public DateTime Birthday { get; set; }
        public string Bio { get; set; }

        public virtual Gender Gender { get; set; }
    }
}