using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Books.Models;

namespace Books.ViewModels
{
    public class BookFormNewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string Title { get; set; }

        [Required]
        [MaxLength(128)]
        public string Author { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Description { get; set; }

        [Display(Name ="Category")]
        public byte categoryId { get; set; }

        public IEnumerable<Category> Categories { get; set; }
    }
}