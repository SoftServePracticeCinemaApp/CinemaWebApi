using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Domain.Entities
{
    public class RatingEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int MovieId { get; set; }

        [ForeignKey("MovieId")]
        public MovieEntity Movie { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        [Range(1, 10)]
        public int Rating { get; set; }
    }
}
