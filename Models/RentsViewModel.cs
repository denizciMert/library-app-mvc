using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApp.Models
{
    public class RentsViewModel
    {
        [Key]
        public int KiralananID { get; set; }
        public int KitapID { get; set; }
        public string? KitapAdi { get; set; }
        public int KullaniciID { get; set; }
        public string? KullaniciAdi { get; set; }
        [DataType(DataType.Date)]
        public DateTime AlisTarihi { get; set; }
        [DataType(DataType.Date)]
        public DateTime? IadeTarihi { get; set; }
        [DataType(DataType.Date)]
        public DateTime? GeriGetirmeTarihi { get; set; }
    }
}
