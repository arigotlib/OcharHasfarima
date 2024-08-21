using System.ComponentModel.DataAnnotations;

namespace OcharHasfarim.Models
{
    public class Library
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Ganer name")]
        public string Genre { get; set; } // ז'אנר הספריה
    }
}
