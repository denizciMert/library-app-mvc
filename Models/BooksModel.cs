using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models
{
    public class BooksModel
    {
        [Key]
        public int KitapID { get; set; }
        [Required]
        public string Baslik { get; set; }
        [Required]
        public string Yazar { get; set; }
        [Required]
        [Range(1,2024)]
        public int YayinYili { get; set; }
        [Required]
        public string Tur { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string RafNumarasi { get; set; }
        [Required]
        public string Durum { get; set; }
    }
}
