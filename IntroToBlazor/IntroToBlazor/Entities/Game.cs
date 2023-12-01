using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntroToBlazor.Entities
{
    public class Game
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // create unique PK
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateOnly ReleaseDate { get; set; }

        [Required]
        [Range(1, 10)]
        public int Rating { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }
    }
}
