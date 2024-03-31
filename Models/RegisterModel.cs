using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models
{
    public class RegisterModel
    {
        [Key]
        public int KullaniciID { get; set; }
        [Required]
        public string Adi { get; set; }
        [Required]
        public string Soyadi { get; set; }
        [Required]
        [EmailAddress]
        public string Eposta { get; set; }
        [Required]
        public string KullaniciAdi { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Sifre { get; set; }
    }
}
