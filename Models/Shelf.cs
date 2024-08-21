using System.ComponentModel.DataAnnotations;

namespace OcharHasfarim.Models
{
    public class Shelf
    {
        [Key]
        public int Id { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }  

        [Required(ErrorMessage = "LibraryId is required.")]
        public int LibraryId { get; set; } 
        public Library Library { get; set; } 
        //public ICollection<Book> Books { get; set; } // אוסף הספרים על המדף
    }
}
