using System;
using System.ComponentModel.DataAnnotations;

namespace OcharHasfarim.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Height { get; set; } 
        public int Width { get; set; }  
        public int ShelfId { get; set; } 
        public Shelf Shelf { get; set; } 
    }

}
