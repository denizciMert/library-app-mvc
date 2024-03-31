using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApp.Models
{
    public class RentsModel
    {
        [Key]
        public int KiralananID { get; set; }
        public int KitapID { get; set; }
        public int KullaniciID { get; set; }
        [DataType(DataType.Date)]
        public DateTime AlisTarihi { get; set; }
        [DataType(DataType.Date)]
        public DateTime? IadeTarihi { get; set; }
        [DataType(DataType.Date)]
        public DateTime? GeriGetirmeTarihi { get;set; }

        [ForeignKey("KullaniciID")]
        public virtual UsersModel? User { get; set; }
        [ForeignKey("KitapID")]
        public virtual BooksModel? Book { get; set; }
    }
}
