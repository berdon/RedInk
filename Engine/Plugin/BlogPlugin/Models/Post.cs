using System.ComponentModel.DataAnnotations;

namespace BlogPlugin.Models
{
    public class Post
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Heading { get; set; }
        [Required]
        public string Body { get; set; }
    }
}