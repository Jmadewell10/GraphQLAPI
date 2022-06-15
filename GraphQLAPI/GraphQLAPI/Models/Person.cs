using System.ComponentModel.DataAnnotations;

namespace GraphQLAPI.Models
{
    public class Person
    {
        [Key]
        public string? ID { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Email { get; set; }
        public IList<Song>? Songs { get; set; }
        public IList<Person>? KnownPeople { get; set; }

    }
}
