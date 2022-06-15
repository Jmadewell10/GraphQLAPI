using System.ComponentModel.DataAnnotations;

namespace GraphQLAPI.Models
{
    public class Song
    {
        [Key]
        public string? ID { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Album { get; set; }
        public IList<Person>? People { get; set; }
    }
}
